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
using RegistreeModel = MusicCenterModels.Registree;
using TeacherModel = MusicCenterModels.Teacher;

namespace MusicCenterWPF.Windows.Admin
{
    /// <summary>
    /// Interaction logic for AddStudentTeacherPair.xaml
    /// </summary>
    public partial class AddStudentTeacherPair : Window
    {
        public AddStudentTeacherPair()
        {
            InitializeComponent();
            this.Loaded += (s, e) => { 
                LoadRegistrees();
                LoadTeachers();
            };
        }
        private async void LoadRegistrees() {
            WebClient<List<RegistreeModel>> webClient = new WebClient<List<RegistreeModel>> { 
                port = 5004,
                Host = "localhost",
                Path = "api/Admin/GetRegistrees"
            };
            List<RegistreeModel> registrees = await webClient.GetAsync();
            foreach (var registree in registrees) {
                ComboBoxItem item = new ComboBoxItem {
                    Content = registree.Name,
                    Tag = registree.Id
                };
                registreeInput.Items.Add(item);
            }
        }
        private async void LoadTeachers() {
            WebClient<List<TeacherModel>> webClient = new WebClient<List<TeacherModel>>
            {
                port = 5004,
                Host = "localhost",
                Path = "api/Admin/GetTeachers"
            };
            List<TeacherModel> teachers = await webClient.GetAsync();
            foreach (var teacher in teachers)
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Content = teacher.Name,
                    Tag = teacher.Id
                };
                teacherInput.Items.Add(item);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem teacherItem = teacherInput.SelectedItem as ComboBoxItem;
            ComboBoxItem registreeItem = registreeInput.SelectedItem as ComboBoxItem;
            if (registreeItem == null || teacherItem == null) { 
                MessageBox.Show("Please select a student and a teacher","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            WebClient<TeacherModel> webClient = new WebClient<TeacherModel> {
                port = 5004,
                Host = "localhost",
                Path = "api/Admin/AddTeacherStudentPair"
            };
            webClient.AddParams("teacherID", teacherItem.Tag as string);
            webClient.AddParams("registreeID",registreeItem.Tag as string);
            bool success = await webClient.PostAsync(new TeacherModel());
            MessageBox.Show(success? "Pair added or already existed.":"Failed to register student for teacher",
                success? "Success":"Error",
                MessageBoxButton.OK, 
                success? MessageBoxImage.Information : MessageBoxImage.Error);
        }
    }
}
