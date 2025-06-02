using MusicCenterWebService;
using MusicCenterWebService.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
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
using UserModel = MusicCenterModels.User;
using WebApiClient;
using System.Text.Json.Nodes;
using System.Text.Json;
using Microsoft.Win32;

namespace MusicCenterWPF.Windows
{
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        private UserModel user = null;
        string newImageFileName = "";
        public UserProfile()
        {
            InitializeComponent();
            this.Loaded += async (s, e) =>
            {
                if (await LoadUser())
                {
                    LoadComponents();
                };
            };
        }
        private async Task<bool> LoadUser()
        {
            WebClient<UserModel> webClient = new WebClient<UserModel>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/User/GetUserById";
            webClient.AddParams("userID", SessionManager.UserID);
            user = await webClient.GetAsync();
            return user != null;
        }
        private void LoadComponents()
        {
            usernameBox.Text = user.Name;
            passwordBox.Text = user.Password;
            usertypeBox.Content = SessionManager.Type;
            emailBox.Text = user.Email;
            addressBox.Text = user.Address;
            phoneNumberBox.Text = user.PhoneNumber;
            string fileName = user.Image;
            string imageUrl = $"http://localhost:5004/api/User/GetImage/{fileName}";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imageUrl, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            //bitmap.Freeze();

            image.ImageSource = bitmap;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            user.Name = usernameBox.Text;
            DbContext.GetInstance().OpenConnection();
            if (new RepositoryUOW().GetUserRepository().GetByUsername(user.Name) != null) {
                MessageBox.Show("Username Taken", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DbContext.GetInstance().CloseConnection();
                return;
            }
            DbContext.GetInstance().CloseConnection();
            user.Password = passwordBox.Text;
            user.Email = emailBox.Text;
            user.Address = addressBox.Text;
            user.PhoneNumber = phoneNumberBox.Text;
            if (newImageFileName.Length > 0)
            {

                try
                {
                    using (FileStream sourceStream = File.Open(newImageFileName, FileMode.Open))
                    {
                        this.user.Image = await CopyImage(sourceStream, newImageFileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error copying file: {ex.Message}",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            WebClient<UserModel> webClient = new WebClient<UserModel>();
            webClient.Host = "localhost";
            webClient.port = 5004;
            webClient.Path = "api/User/UpdateProfile";
            webClient.AddParams("user",JsonSerializer.Serialize(user));
            bool success = await webClient.PostAsync(user);
            if (success)
            {
                DbContext.GetInstance().OpenConnection();
                RepositoryUOW repositoryUOW = new RepositoryUOW();
                while (!repositoryUOW.GetUserRepository().GetById(user.Id).Equals(user))
                {
                    Thread.Sleep(250);
                }
                DbContext.GetInstance().CloseConnection();
            }
            Visibility = Visibility.Hidden;
            UserProfile profileWindow = new UserProfile();
            MessageBox.Show(success ? "Profile Updated" : "Update Failed",
                success? "Success":"Error",
                MessageBoxButton.OK,
                success? MessageBoxImage.Information:MessageBoxImage.Error);
            profileWindow.Show();
        }
        private async Task<string> CopyImage(FileStream sourceStream,string fileName)
        {
            if (sourceStream.Length <= 0)
            {
                throw new Exception("Empty/Corrupted image provided");
            }
            WebClient<string> webClient = new WebClient<string>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/User/UploadImage";
            return await webClient.PostAsync<string>(sourceStream, fileName);
        }
        private void imageInput_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Image|*.jpg|Jpeg Image|*.jpeg|PNG Image|*.png";
            openFileDialog.Title = "Choose Image";
            openFileDialog.ShowDialog();
            newImageFileName = openFileDialog.FileName;
        }
    }
}
