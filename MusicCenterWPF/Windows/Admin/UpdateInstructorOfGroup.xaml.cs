using MusicCenterModels;
using MusicCenterWebService;
using MusicCenterWebService.Repositories;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebApiClient;

namespace MusicCenterWPF.Windows.Admin
{
    /// <summary>
    /// Interaction logic for UpdateInstructorOfGroup.xaml
    /// </summary>
    public partial class UpdateInstructorOfGroup : Window
    {
        private string selectedGroupId = "";
        private string selectedInstructorId = "";

        private RepositoryUOW repositoryUOW = new RepositoryUOW();
        public UpdateInstructorOfGroup()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                LoadGroups();
                LoadInstructors();
            };
        }
        private async void LoadGroups()
        {
            WebClient<List<Group>> webClient = new WebClient<List<Group>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetGroups";
            List<Group> groups = await webClient.GetAsync();
            GroupInput.Items.Clear();
            foreach (var group in groups)
            {
                var item = new ComboBoxItem
                {
                    Content = group.Name,
                    Tag = group.Id
                };
                GroupInput.Items.Add(item);
            }
        }

        private async void LoadInstructors()
        {
            WebClient<List<Instructor>> webClient = new WebClient<List<Instructor>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetInstructors";
            List<Instructor> instructors = await webClient.GetAsync();
            InstructorInput.Items.Clear();
            foreach (var instructor in instructors)
            {
                var item = new ComboBoxItem
                {
                    Content = instructor.Name,
                    Tag = instructor.Id
                };
                InstructorInput.Items.Add(item);
            }
        }

        private void GroupInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = GroupInput.SelectedItem as ComboBoxItem;
            if (item != null)
                selectedGroupId = item.Tag as string;
        }

        private void InstructorInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = InstructorInput.SelectedItem as ComboBoxItem;
            if (item != null)
                selectedInstructorId = item.Tag as string;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedGroupId) || string.IsNullOrWhiteSpace(selectedInstructorId))
            {
                MessageBox.Show("Please select both a group and an instructor.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            WebClient<Group> webClient = new WebClient<Group>
            {
                port = 5004,
                Host = "localhost",
                Path = "api/Admin/AssignInstructorToGroup"
            };

            webClient.AddParams("groupID", selectedGroupId);
            webClient.AddParams("instructorID", selectedInstructorId);

            bool success = await webClient.PostAsync(new Group());

            MessageBox.Show(success ? "Instructor updated for group." : "Failed to update instructor.",
                success ? "Success" : "Error",
                MessageBoxButton.OK,
                success ? MessageBoxImage.Information : MessageBoxImage.Error);
        }
    }
}
