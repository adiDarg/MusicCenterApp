using MusicCenterModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
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

namespace MusicCenterWPF.Windows.User
{
    /// <summary>
    /// Interaction logic for Schedule.xaml
    /// </summary>
    public partial class Schedule : Window
    {
        public Schedule()
        {
            InitializeComponent();
            this.Loaded += async (s, e) =>
            {
                ScheduleViewModel scheduleViewModel = await ImportSchedule(SessionManager.UserID);
                DisplaySchedule(scheduleViewModel);
            };
        }
        private async Task<ScheduleViewModel> ImportSchedule(string userID)
        {
            WebClient<ScheduleViewModel> webClient = new();
            webClient.Host = "localhost";
            webClient.port = 5004;
            if (SessionManager.Type.Equals("Instructor"))
            {
                webClient.Path = $"api/Teacher/GetSchedule";
            }
            else
            {
                webClient.Path = $"api/{SessionManager.Type}/GetSchedule";
                string debug = $"api/{SessionManager.Type}/GetSchedule";
            }
            webClient.AddParams($"{SessionManager.Type.ToLower()}ID", userID);
            return await webClient.GetAsync();
        }
        private void DisplaySchedule(ScheduleViewModel schedule)
        {
            List<McEvent> list = new();
            if (schedule.Meetings != null)
                list.AddRange(schedule.Meetings);
            if (schedule.Lessons != null)
                list.AddRange(schedule.Lessons);
            list.Sort((a, b) =>
            {
                return DateTime.Parse(a.Date).CompareTo(DateTime.Parse(b.Date));
            });
            Style ? style = this.FindResource("StyledLabel") as Style;
            int column = 0;
            int grid = 0;
            eventGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            foreach (McEvent mcEvent in list)
            {
                StackPanel stackPanel = new StackPanel();
                DateTime date = new DateTime();
                string[] formats = {
                    "yyyy-MM-ddTHH:mm:ss",
                    "yyyy-MM-dTHH:mm:ss",
                    "yyyy-M-ddTHH:mm:ss",
                    "yyyy-M-dTHH:mm:ss",
                    "yyyy-M-dTHH:mm"
                };

                DateTime.TryParseExact(
                    mcEvent.Date,
                    formats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out date
                );
                Label title = new Label
                {
                    Style = style,
                    Content =  date.ToString("g"),
                    FontSize = 18
                };
                Label room = new Label
                {
                    Style = style,
                    Content = mcEvent.Room,
                    FontSize = 16
                };
                stackPanel.Children.Add(title);
                stackPanel.Children.Add(room);

                if (mcEvent is Lesson)
                {
                    Lesson lesson = (Lesson)mcEvent;
                    Label student = new Label
                    {
                        Style = style,
                        Content = "Student: " + lesson.Student.Name,
                        FontSize = 14
                    };
                    Label teacher = new Label
                    {
                        Style = style,
                        Content = "Teacher: " + lesson.Teacher.Name,
                        FontSize = 14
                    };
                    stackPanel.Children.Add(student);
                    stackPanel.Children.Add(teacher);
                }
                else if (mcEvent is Meeting)
                {
                    Meeting meeting = (Meeting)mcEvent;
                    Label group = new Label
                    {
                        Style = style,
                        Content = "Group: " + meeting.Group.Name,
                        FontSize = 14
                    };
                    stackPanel.Children.Add(group);
                }
                Border border = new Border()
                {
                    Child = stackPanel,
                    Style = this.FindResource("CellBorderStyle") as Style
                };
                Grid.SetColumn(border, column);
                Grid.SetRow(border, grid);
                eventGrid.Children.Add(border);
                if (column == 3)
                {
                    grid++;
                    eventGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                }
                column = (column + 1) % 4;
            }
        }
    }
}
