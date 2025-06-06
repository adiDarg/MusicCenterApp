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
            string password = passwordInput.Password;
            if (username == null || username.Length == 0 || password == null || password.Length == 0)
            {
                MessageBox.Show("Please fill all fields.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                return;
            }
            MessageBox.Show("An error occured during login. Please verify that username and password are correct.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
