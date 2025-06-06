using MusicCenterModels;
using MusicCenterWPF.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for ScheduleEvent.xaml
    /// </summary>
    public partial class ScheduleEvent : Window
    {
        Group? currentGroup = null;
        public ScheduleEvent()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                LoadGroups();
                LoadStudents();
            };
        }
        private async void LoadGroups()
        {
            WebClient<List<Group>> webClient = new WebClient<List<Group>>
            {
                port = 5004,
                Host = "localhost",
                Path = "api/Admin/GetGroups"
            };
            List<Group> groups = await webClient.GetAsync();
            foreach (var group in groups)
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Content = group.Name,
                    Tag = group.Id
                };
                groupComboBox.Items.Add(item);
            }
        }
        private async void LoadStudents()
        {
            WebClient<List<MusicCenterModels.Registree>> webClient = new WebClient<List<MusicCenterModels.Registree>>
            {
                port = 5004,
                Host = "localhost",
                Path = "api/Admin/GetRegistrees"
            };
            List<MusicCenterModels.Registree> registrees = await webClient.GetAsync();
            foreach (var registree in registrees)
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Content = registree.Name,
                    Tag = registree.Id
                };
                studnetComboBox.Items.Add(item);
            }
        }
        private void actionChoiceBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (actionChoiceBox.SelectedItem != null) {
                if ((actionChoiceBox.SelectedItem as ComboBoxItem).Content.Equals("Lesson"))
                {
                    LessonGrid.Visibility = Visibility.Visible;
                    ComboBoxHelper.SetPlaceholder(studnetComboBox, "Choose Student");
                    ComboBoxHelper.SetPlaceholder(teacherComboBox, "Choose Teacher");
                    
                    MeetingGrid.Visibility = Visibility.Hidden;
                    ComboBoxHelper.SetPlaceholder(groupComboBox, "");
                    currentGroup = null;
                    roomTextBox.Text = null;
                }
                else if ((actionChoiceBox.SelectedItem as ComboBoxItem).Content.Equals("Meeting"))
                {
                    LessonGrid.Visibility = Visibility.Hidden;
                    ComboBoxHelper.SetPlaceholder(studnetComboBox, "");
                    ComboBoxHelper.SetPlaceholder(teacherComboBox, "");

                    MeetingGrid.Visibility = Visibility.Visible;
                    ComboBoxHelper.SetPlaceholder(groupComboBox, "Choose Group");
                }
            }
        }
        private async void groupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = groupComboBox.SelectedItem as ComboBoxItem;
            if (item == null)
            {
                return;
            }
            WebClient<Group> webClient = new WebClient<Group> { 
                port = 5004,
                Host = "localhost",
                Path = "api/Admin/GetGroupById"
            };
            webClient.AddParams("groupID", item.Tag as string);
            currentGroup = await webClient.GetAsync();
            roomTextBox.Text = currentGroup == null ? "" : currentGroup.Room;
        }
        private async void studentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (studnetComboBox.SelectedItem == null)
            {
                return;
            }
            WebClient<List<MusicCenterModels.Teacher>> webClient = new WebClient<List<MusicCenterModels.Teacher>>
            {
                port = 5004,
                Host = "localhost",
                Path = "api/Admin/GetTeachersByRegistree"
            };
            webClient.AddParams("registreeID", (studnetComboBox.SelectedItem as ComboBoxItem).Tag as string);
            List<MusicCenterModels.Teacher> teachers = await webClient.GetAsync();
            LoadTeachers(teachers);
        }
        private void LoadTeachers(List<MusicCenterModels.Teacher> teachers)
        {
            teacherComboBox.Items.Clear();
            foreach (var teacher in teachers)
            {
                ComboBoxItem teacherComboBoxItem = new ComboBoxItem
                {
                    Content = teacher.Name,
                    Tag = teacher.Id
                };
                teacherComboBox.Items.Add(teacherComboBoxItem);
            }
        }

        private async void submitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime dateTime = (DateTime)datePicker.Value;
                DateTime withoutSeconds = new DateTime(
                    dateTime.Year, dateTime.Month, dateTime.Day,
                    dateTime.Hour, dateTime.Minute, 0
                );
                string date = withoutSeconds.ToString("s");
                string room = roomTextBox.Text;
                if (((actionChoiceBox.SelectedItem as ComboBoxItem).Content as string).Equals("Lesson"))
                {
                    string studnetID = (studnetComboBox.SelectedItem as ComboBoxItem).Tag as string;
                    string teacherID = (teacherComboBox.SelectedItem as ComboBoxItem).Tag as string;
                    Lesson lesson = new Lesson {
                        Date = date,
                        Room = room,
                        Student = new MusicCenterModels.Registree { Id = studnetID },
                        Teacher = new MusicCenterModels.Teacher { Id = teacherID }
                    };
                    WebClient<Lesson> lessonWebClient = new WebClient<Lesson> {
                        port = 5004,
                        Host = "localhost",
                        Path = "api/Admin/CreateLesson"
                    };
                    lessonWebClient.AddParams("lesson",JsonSerializer.Serialize(lesson));
                    bool success = await lessonWebClient.PostAsync(lesson);
                    MessageBox.Show(success? "Lesson posted.": "Failed to post lesson.",
                        success? "Success":"Error",
                        MessageBoxButton.OK,
                        success? MessageBoxImage.Information : MessageBoxImage.Error);
                }
                else if (((actionChoiceBox.SelectedItem as ComboBoxItem).Content as string).Equals("Meeting"))
                {
                    string groupID = (groupComboBox.SelectedItem as ComboBoxItem).Tag as string;
                    Meeting meeting = new Meeting
                    {
                        Date = date,
                        Room = room,
                        Group = new Group { Id = groupID }
                    };
                    WebClient<Meeting> meetingWebClient = new WebClient<Meeting>
                    {
                        port = 5004,
                        Host = "localhost",
                        Path = "api/Admin/CreateMeeting"
                    };
                    meetingWebClient.AddParams("meeting", JsonSerializer.Serialize(meeting));
                    bool success = await meetingWebClient.PostAsync(meeting);
                    MessageBox.Show(success ? "Meeting posted." : "Failed to post meeting.",
                        success ? "Success" : "Error",
                        MessageBoxButton.OK,
                        success ? MessageBoxImage.Information : MessageBoxImage.Error);
                }
            }
            catch {
                MessageBox.Show("Please fill all fields","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
