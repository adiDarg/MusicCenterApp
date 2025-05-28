using Microsoft.AspNetCore.Http;
using Microsoft.Win32;
using MusicCenterModels;
using MusicCenterWebService;
using MusicCenterWebService.Repositories;
using MusicCenterWPF.Windows;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
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
using Utility;
using WebApiClient;
using static System.Net.Mime.MediaTypeNames;

namespace MusicCenterWPF
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        private string image_fileName;
        public SignUp()
        {
            InitializeComponent();
        }

        private void fileOpener_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Image|*.jpg|Jpeg Image|*.jpeg|PNG Image|*.png";
            openFileDialog.Title = "Choose Image";
            openFileDialog.ShowDialog();
            image_fileName = openFileDialog.FileName;
        }

        //Fix Image not saving properly
        private async void SignUpButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            string username = usernameInput.Text;
            string password = passwordInput.Text;
            if (username.Equals("") || password.Equals(""))
            {
                errorLabel.Content = "Must enter username and password";
                return;
            }
            string email = emailInput.Text;
            string address = addressInput.Text;
            string phoneNumber = phoneNumberInput.Text;
            
            User user = new User();
            user.Name = username;
            user.Password = password;
            user.Email = email;
            user.Address = address;
            user.PhoneNumber = phoneNumber;

            if (image_fileName != null && image_fileName.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image_fileName);
                var uploadsDirectory = "C:\\Users\\Owner\\Desktop\\ביה''ס\\מדמ''ח\\MusicCenter\\Utility\\Images\\";
                var filePath = Path.Combine(uploadsDirectory, fileName);

                try
                {
                    using (FileStream sourceStream = File.Open(image_fileName, FileMode.Open))
                    using (FileStream destinationStream = File.Create(filePath))
                    {
                        if (sourceStream.Length <= 0)
                        {
                            throw new Exception("Empty/Corrupted image provided");
                        }
                        sourceStream.CopyTo(destinationStream);
                    }
                    user.Image = fileName;
                }
                catch (Exception ex)
                {
                    errorLabel.Content = $"Error copying file: {ex.Message}";
                    await Task.Delay(1000);
                    Thread.Sleep(1000);
                    user.Image = "placeholder.jpg";
                }
            }
            else
            {
                user.Image = "placeholder.jpg";
            }
            WebClient<User> client = new WebClient<User>();
            client.port = 5004;
            client.Path = "api/Guest/AddUser";
            string userJson = JsonSerializer.Serialize(user);
            client.AddParams("user", userJson);
            bool success = await client.PostAsync(user);
            if (success)
            {
                errorLabel.Content = "Loading...";
                await Task.Delay(100);

                DbContext.GetInstance().OpenConnection();
                while (new RepositoryUOW().GetUserRepository().GetByUsername(username) == null)
                {
                    Thread.Sleep(1000);
                }
                SessionManager.UserID = new RepositoryUOW().GetUserRepository().GetByUsername(username).Id;
                DbContext.GetInstance().CloseConnection();
                EmailUtils.SendValidationKeyEmail(user.Email, user.ValidationKey);
                SessionManager.Type = "User";
                Visibility = Visibility.Hidden;
                new UserProfile().Show();
            }
            else
            {
                errorLabel.Content = "Sign up failed";
            }
        }
    }
}
