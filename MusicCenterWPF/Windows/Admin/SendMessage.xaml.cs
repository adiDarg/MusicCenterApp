using MusicCenterModels;
using MusicCenterWPF.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebApiClient;
using UserModel = MusicCenterModels.User;

namespace MusicCenterWPF.Windows.Admin
{
    /// <summary>
    /// Interaction logic for SendMessage.xaml
    /// </summary>
    public partial class SendMessage : Window
    {
        private List<UserModel> users;
        private Dictionary<string,UserModel> selectedUsers = new Dictionary<string, UserModel>();
        public SendMessage()
        {
            InitializeComponent();
            this.Loaded += async (s, e) => {
                WebClient<List<UserModel>> webClient = new WebClient<List<UserModel>> {
                    port = 5004,
                    Host = "localhost",
                    Path = "api/Admin/GetUsers"
                };
                users = await webClient.GetAsync();
                LoadUsers();
            };
        }
        private void LoadUsers()
        {
            recieversComboBox.Items.Clear();
            foreach (var user in users)
            {
                if (!selectedUsers.ContainsKey(user.Id))
                {
                    ComboBoxItem comboBoxItem = new ComboBoxItem
                    {
                        Content = user.Name,
                        Tag = user.Id
                    };
                    recieversComboBox.Items.Add(comboBoxItem);
                }
            }
        }
        private void LoadSelected()
        {
            recieversPanel.Children.Clear();
            foreach (var user in selectedUsers.Values)
            {
                StackPanel item = new StackPanel();
                Label usernameLabel = new Label {
                    Style = this.FindResource("StyledLabel") as Style,
                    Content = user.Name
                };
                item.Children.Add(usernameLabel);
                
                Button removeButton = new Button {
                    Style = this.FindResource("StyledButton") as Style,
                    Content = "Remove"
                };
                removeButton.Click += (s, e) => { 
                    selectedUsers.Remove(user.Id);
                    LoadUsers();
                    LoadSelected();
                };
                item.Children.Add(removeButton);
                
                Border border = new Border {
                    Style = this.FindResource("CellBorderStyle") as Style
                };
                border.Child = item;
                recieversPanel.Children.Add(border);
            }
        }

        private void recieversComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (recieversComboBox.Items.Count == 0)
            {
                return;
            }
            ComboBoxItem? item = (recieversComboBox.SelectedItem as ComboBoxItem);
            if (item == null) {
                return;
            }
            string? userID = item.Tag as string;
            if (userID == null)
            {
                return;
            }
            UserModel selectedUser = new UserModel();
            foreach (var user in users)
            {
                if (user.Id == userID)
                {
                    selectedUser = user;
                }
            }
            selectedUsers.Add(userID, selectedUser);
            LoadUsers();
            LoadSelected();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUsers.Count == 0)
            {
                MessageBox.Show("Please add recievers","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            string title = titleInput.Text;
            string description = descriptionInput.Text;
            if (title.Length == 0 || description.Length == 0)
            {
                MessageBox.Show("Please fill all fields","Error",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Message message = new Message {
                Title = title,
                Description = description,
            };
            WebClient<Message> webClient = new WebClient<Message> {
                port = 5004,
                Host = "localhost",
                Path = "api/Admin/SendMessage"
            };
            bool success = await webClient.PostAsync(message);

            if (success)
            {
                string? messageID = await GetMessageID(message);
                int count = 0;
                while (messageID == null && count < 50){
                    Thread.Sleep(200);
                    messageID = await GetMessageID(message);
                    count++;
                };
                if (messageID == null)
                {
                    MessageBox.Show("Failed to load message to send to recivers.", "Error"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                foreach (string id in selectedUsers.Keys)
                {
                    success = await SendMessageToReciever(id, messageID);
                    if (!success)
                    {
                        MessageBox.Show("Failed to send to " + selectedUsers.GetValueOrDefault(id), 
                            "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

            MessageBox.Show(success? "Message sent.":"Message failed to send.",
                success? "Success":"Error",
                MessageBoxButton.OK,
                success? MessageBoxImage.Information : MessageBoxImage.Error);
        }
        private async Task<bool> SendMessageToReciever(string recieverID, string messageID)
        {
            WebClient<Message> webClient = new WebClient<Message> {
                port = 5004,
                Host = "localhost",
                Path = "api/Admin/AddReceiver"
            };
            webClient.AddParams("messageID", messageID);
            webClient.AddParams("receiverID", recieverID);
            return await webClient.PostAsync(new Message());
        }
        public async Task<string?> GetMessageID(Message message)
        {
            WebClient<Message> webClient = new WebClient<Message> {
                port = 5004,
                Host = "localhost",
                Path = "api/Admin/GetMessageByTitleAndDescription"
            };
            webClient.AddParams("title", message.Title);
            webClient.AddParams("description", message.Description);
            Message message1 = await webClient.GetAsync();
            if (message1 != null) {
                return message1.Id;
            }
            return null;
        }
    }
}
