using MusicCenterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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

namespace MusicCenterWPF.Windows.Admin
{
    /// <summary>
    /// Interaction logic for PromoteAndDemote.xaml
    /// </summary>
    public partial class PromoteAndDemote : Window
    {
        private string selectedUserID = "";
        public PromoteAndDemote()
        {
            InitializeComponent();
        }

        private void userChoiceBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userChoiceBox.Items.Count == 0)
            {
                return;
            }
            try
            {
                selectedUserID = (userChoiceBox.SelectedItem as ComboBoxItem).Tag as string;
            }
            catch {
                MessageBox.Show("select action first","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        private void LoadUsers(List<MusicCenterModels.User> users)
        {
            userChoiceBox.Items.Clear();
            foreach (var user in users) {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = user.Name;
                comboBoxItem.Tag = user.Id;
                userChoiceBox.Items.Add(comboBoxItem);
            }
        }

        private async void actionChoiceBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WebClient<List<MusicCenterModels.User>> webClient = new WebClient<List<MusicCenterModels.User>>
            {
                port = 5004,
                Host = "localhost",
                Path = "api/Admin/Get" + (actionChoiceBox.SelectedItem as ComboBoxItem).Tag as string
            };
            var result = await webClient.GetAsync();
            LoadUsers(result);
        }

        private async void submitButton_Click(object sender, RoutedEventArgs e)
        {
            bool success = false;
            switch ((actionChoiceBox.SelectedItem as ComboBoxItem).Content as string) {
                case "Promote To Teacher":
                    WebClient<MusicCenterModels.Teacher> webClientPT = new WebClient<MusicCenterModels.Teacher> 
                    { 
                        port = 5004,
                        Host = "localhost",
                        Path = "api/Admin/MakeUserTeacher"
                    };
                    webClientPT.AddParams("userID", (userChoiceBox.SelectedItem as ComboBoxItem).Tag as string);
                    success = await webClientPT.PostAsync(new MusicCenterModels.Teacher());
                    break;
                
                case "Promote To Instructor":
                    WebClient<Instructor> webClientPI = new WebClient<Instructor>
                    {
                        port = 5004,
                        Host = "localhost",
                        Path = "api/Admin/MakeUserInstructor"
                    };
                    webClientPI.AddParams("userID", (userChoiceBox.SelectedItem as ComboBoxItem).Tag as string);
                    success = await webClientPI.PostAsync(new Instructor());
                    break;

                case "Demote Instructor":
                    WebClient<Instructor> webClientDI = new WebClient<Instructor>
                    {
                        port = 5004,
                        Host = "localhost",
                        Path = "api/Admin/DemoteInstructor"
                    };
                    webClientDI.AddParams("userID", (userChoiceBox.SelectedItem as ComboBoxItem).Tag as string);
                    success = await webClientDI.PostAsync(new Instructor());
                    break;

                case "Demote Teacher":
                    WebClient<MusicCenterModels.Teacher> webClientDT = new WebClient<MusicCenterModels.Teacher>
                    {
                        port = 5004,
                        Host = "localhost",
                        Path = "api/Admin/DemoteTeacher"
                    };
                    webClientDT.AddParams("userID", (userChoiceBox.SelectedItem as ComboBoxItem).Tag as string);
                    success = await webClientDT.PostAsync(new MusicCenterModels.Teacher());
                    break;
            }
            MessageBox.Show(success ? "Action was successfully performed." : "Action failed",
                success ? "Success" : "Error",
                MessageBoxButton.OK,
                success ? MessageBoxImage.Information : MessageBoxImage.Error);
        }
    }
}
