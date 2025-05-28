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
            }
            webClient.AddParams("userID", userID);
            return await webClient.GetAsync();
        }
        private void DisplaySchedule(ScheduleViewModel schedule)
        {
            List<McEvent> list = new();
            if (schedule.Meetings != null)
                list.AddRange(schedule.Meetings);
            if (schedule.Lessons != null)
                list.AddRange(schedule.Lessons);
            list.Sort();
        }
    }
}
