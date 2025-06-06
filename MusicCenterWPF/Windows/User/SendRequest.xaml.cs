using MusicCenterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebApiClient;

namespace MusicCenterWPF.Windows.User
{
    /// <summary>
    /// Interaction logic for SendRequest.xaml
    /// </summary>
    public partial class SendRequest : Window
    {
        public SendRequest()
        {
            InitializeComponent();
            this.Loaded += (s, e) => LoadUsers();
        }
        private async void LoadUsers()
        {
            WebClient<List<MusicCenterModels.User>> webClient = new WebClient<List<MusicCenterModels.User>>
            {
                port = 5004,
                Host = "localhost",
                Path = "api/User/GetUsers"
            };
            List<MusicCenterModels.User> users = await webClient.GetAsync();
            foreach (var user in users) {
                ComboBoxItem item = new ComboBoxItem { 
                    Content = user.Name,
                    Tag = user.Id
                };
                recieverInput.Items.Add(item);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string recieverID = (recieverInput.SelectedItem as ComboBoxItem).Tag as string;
            string title = titleInput.Text;
            string description = descriptionInput.Text;
            string requestType = (typeInput.SelectedItem as ComboBoxItem).Content as string;
            if (string.IsNullOrEmpty(recieverID) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(requestType))
            {
                MessageBox.Show("Please select a reciever.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Request request = new Request { 
                Title = title,
                Description = description,
                Reciever = new MusicCenterModels.User { Id = recieverID },
                Sender = new MusicCenterModels.User { Id = SessionManager.UserID },
                IsApproved = false,
                IsSeen = false,
                RequestType = requestType
            };
            WebClient<Request> client = new WebClient<Request>();
            client.port = 5004;
            client.Host = "localhost";
            client.Path = "api/User/SendRequest";
            client.AddParams("request", JsonSerializer.Serialize(request));
            bool success = await client.PostAsync(request);
            MessageBox.Show(success? "Request sent.":"Request failed to send.",
                success? "Success":"Error",
                MessageBoxButton.OK,
                success? MessageBoxImage.Information: MessageBoxImage.Error);
        }
    }
}
