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

namespace MusicCenterWPF.Windows.Shared
{
    /// <summary>
    /// Interaction logic for Messages.xaml
    /// </summary>
    public partial class Messages : Window
    {
        public Messages()
        {
            InitializeComponent();
            this.Loaded += (s, e) => LoadMessages();
        }
        private async void LoadMessages() {
            WebClient<List<Message>> webClient = new WebClient<List<Message>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/User/ShowMessages";
            webClient.AddParams("userID",SessionManager.UserID);
            List<Message> messages = await webClient.GetAsync();
            foreach (Message message in messages) {
                StackPanel stackPanel = new StackPanel();
                Style style = this.FindResource("StyledLabel") as Style;
                Label title = new Label { 
                    Content = message.Title,
                    Style = style
                };
                Label description = new Label { 
                    Content = message.Description,
                    Style = style
                };
                stackPanel.Children.Add(title);
                stackPanel.Children.Add(description);
                messagesPanel.Children.Add(stackPanel);
            } 
        }
    }
}
