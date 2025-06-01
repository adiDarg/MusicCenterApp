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
    /// Interaction logic for CreateGroup.xaml
    /// </summary>
    public partial class CreateGroup : Window
    {
        private string selectedInstructorId = "";
        public CreateGroup()
        {

            InitializeComponent();
            this.Loaded += (s,e) => LoadInstructors();
        }
        private async void LoadInstructors()
        {
            WebClient<List<Instructor>> webClient = new WebClient<List<Instructor>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetInstructors";
            List<Instructor> instructors = await webClient.GetAsync();
            InstructorSelection.Items.Clear();
            foreach (var instructor in instructors)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = instructor.Name;
                item.Tag = instructor.Id;
                InstructorSelection.Items.Add(item);
            }
            InstructorSelection.SelectionChanged += InstructorSelection_SelectionChanged;
        }

        private void InstructorSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ComboBoxItem)InstructorSelection.SelectedItem;
            if (item != null)
                selectedInstructorId = (string)item.Tag;
        }
        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string groupName = groupNameInput.Text;
            string room = RoomInput.Text;

            if (string.IsNullOrWhiteSpace(groupName) || string.IsNullOrWhiteSpace(room) || string.IsNullOrWhiteSpace(selectedInstructorId))
            {
                MessageBox.Show("Please fill all fields.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Group newGroup = new Group
            {
                Id = Guid.NewGuid().ToString(),
                Name = groupName,
                Room = room,
                Instructor = new Instructor()
            };
            newGroup.Instructor.Id = selectedInstructorId;

            WebClient<Group> webClient = new WebClient<Group>
            {
                port = 5004,
                Host = "localhost",
                Path = "api/Admin/CreateGroup"
            };

            bool success = await webClient.PostAsync(newGroup);
            if (success)
            {
                MessageBox.Show("Group created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to create group.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
