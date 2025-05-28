using MusicCenterModels;
using MusicCenterWebService;
using MusicCenterWebService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using UserModel = MusicCenterModels.User;

namespace MusicCenterWPF.Windows
{
    /// <summary>
    /// Interaction logic for Requests.xaml
    /// </summary>
    public partial class Requests : Window
    {
        private RepositoryUOW repositoryUOW = new RepositoryUOW();
        public Requests()
        {
            DbContext.GetInstance().OpenConnection();
            List<Request> requestsSent = repositoryUOW.GetRequestRepository().GetSentByUserID(SessionManager.UserID);
            List<Request> requestsRecieved = repositoryUOW.GetRequestRepository().GetRecievedByUserID(SessionManager.UserID);
            DbContext.GetInstance().CloseConnection();
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                if (requestsSent != null)
                {
                    HandleRequestsSent(requestsSent);
                }
                if (requestsRecieved != null)
                {
                    HandleRequestsRecieved(requestsRecieved);
                }
            };
        }
        private void HandleRequestsSent(List<Request> requestsSent)
        {
            foreach (Request request in requestsSent)
            {
                Style? style = this.FindResource("StyledLabel") as Style;
                StackPanel stackPanel = new StackPanel();

                Label title = new Label
                {
                    Content = request.Title,
                    Style = style,
                    FontSize = 18
                };

                Label description = new Label
                {
                    Content = request.Description,
                    Style = style,
                    FontSize = 18,
                };

                DbContext.GetInstance().OpenConnection();
                Label sentBy = new Label
                {
                    Content = "Sent to: " + repositoryUOW.GetUserRepository().GetRecieverByRequestID(request.Id).Name,
                    Style = style,
                    FontSize = 15,
                };
                DbContext.GetInstance().CloseConnection();

                stackPanel.Children.Insert(0, title);
                stackPanel.Children.Insert(1, description);
                stackPanel.Children.Insert(2, sentBy);

                RequestsSent.Children.Insert(0, stackPanel);
            }
        }
        private void HandleRequestsRecieved(List<Request> requestsRecieved)
        {
            foreach (Request request in requestsRecieved)
            {
                Style? style = this.FindResource("StyledLabel") as Style;
                Style? buttonStyle = this.FindResource("StyledButton") as Style; ;

                StackPanel stackPanel = new StackPanel();

                DbContext.GetInstance().OpenConnection();
                string senderName = repositoryUOW.GetUserRepository().GetSenderByRequestID(request.Id).Name;
                DbContext.GetInstance().CloseConnection();

                Label title = new Label
                {
                    Content = request.Title,
                    Style = style,
                    FontSize = 18
                };

                Label description = new Label
                {
                    Content = request.Description,
                    Style = style,
                    FontSize = 16
                };

                Label sentBy = new Label
                {
                    Content = "Received From: " + senderName,
                    Style = style,
                    FontSize = 15
                };

                stackPanel.Children.Insert(0, title);
                stackPanel.Children.Insert(1, description);
                stackPanel.Children.Insert(2, sentBy);

                if (request.IsSeen == null || !(bool)request.IsSeen)
                {
                    Button accept = new Button
                    {
                        Content = "Approve",
                        Style = buttonStyle,
                        FontSize = 15,
                    };
                    accept.Width = 0.8 * accept.Width;
                    accept.Height = 0.8 * accept.Height;

                    accept.Click += (sender, args) =>
                    {
                        request.IsSeen = true;
                        request.IsApproved = true;
                        DbContext.GetInstance().OpenConnection();
                        sentBy.Content = repositoryUOW.GetRequestRepository().Update(request);
                        DbContext.GetInstance().CloseConnection();
                        this.Visibility = Visibility.Hidden;
                        new Requests().Show();
                    };

                    Button deny = new Button
                    {
                        Content = "Deny",
                        Style = buttonStyle,
                        FontSize = 15,
                        Width = accept.Width,
                        Height = accept.Height
                    };

                    deny.Click += (sender, args) =>
                    {
                        request.IsSeen = true;
                        request.IsApproved = false;
                        DbContext.GetInstance().OpenConnection();
                        sentBy.Content = repositoryUOW.GetRequestRepository().Update(request);
                        DbContext.GetInstance().CloseConnection();
                        this.Visibility = Visibility.Hidden;
                        new Requests().Show();
                    };

                    stackPanel.Children.Insert(3, accept);
                    stackPanel.Children.Insert(4, deny);
                }
                else
                {
                    Label isAccepted = new Label
                    {
                        Content = (request.IsApproved ?? false) ? "Request was approved" : "Request was denied",
                        Style = style,
                        FontSize = 15
                    };

                    stackPanel.Children.Insert(3, isAccepted);
                }


                RequestsReceived.Children.Insert(0, stackPanel);
            }
        }
    }
}
