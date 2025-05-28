using Microsoft.AspNetCore.Http;
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
using Utility;
using WebApiClient;

namespace MusicCenterWPF.Windows
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private async void signInButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameInput.Text;
            string password = passwordInput.Text;
            if (username == null || username.Length == 0 || password == null || password.Length == 0)
            {
                errorLabel.Content = "Must fill all fields";
                return;
            }
            WebClient<string> client = new WebClient<string>();
            client.port = 5004;
            client.Path = "api/Guest/Login";
            client.AddParams("username", username);
            client.AddParams("password", password);
            string? userID = await client.GetAsync();
            if (userID != null)
            {
                SessionManager.UserID = userID;
                SessionManager.Type = UserTypeGetter.GetUserType(userID);
                this.Visibility = Visibility.Hidden;
                new UserProfile().Show();
            }
            errorLabel.Content = "Failed to log in. Try checking username or password";
        }
    }
}
