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
        private RepositoryUOW repositoryUOW = new RepositoryUOW();
        private DbContext db = DbContext.GetInstance();
        string newImageFileName = "";
        public UserProfile()
        {
            db.OpenConnection();
            user = repositoryUOW.GetUserRepository().GetById(SessionManager.UserID);
            db.CloseConnection();
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                usernameBox.Text = user.Name;
                passwordBox.Text = user.Password;
                usertypeBox.Content = SessionManager.Type;
                emailBox.Text = user.Email;
                addressBox.Text = user.Address;
                phoneNumberBox.Text = user.PhoneNumber;
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", user.Image);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(path);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bitmap.EndInit();
                bitmap.Freeze();

                image.ImageSource = bitmap;
            };
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            user.Name = usernameBox.Text;
            user.Password = passwordBox.Text;
            user.Email = emailBox.Text;
            user.Address = addressBox.Text;
            user.PhoneNumber = phoneNumberBox.Text;
            if (newImageFileName.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(newImageFileName);
                var uploadsDirectory = "C:\\Users\\Owner\\Desktop\\ביה''ס\\מדמ''ח\\MusicCenter\\Utility\\Images\\";
                var filePath = System.IO.Path.Combine(uploadsDirectory, fileName);

                try
                {
                    using (FileStream sourceStream = File.Open(newImageFileName, FileMode.Open))
                    using (FileStream destinationStream = File.Create(filePath))
                    {
                        if (sourceStream.Length <= 0) {
                            throw new Exception("Empty/Corrupted image provided");
                        }
                        sourceStream.CopyTo(destinationStream);
                    }
                    using (FileStream sourceStream = File.Open(newImageFileName, FileMode.Open))
                    using (FileStream destinationStream = File.Create(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", fileName)))
                    {
                        if (sourceStream.Length <= 0)
                        {
                            throw new Exception("Empty/Corrupted image provided");
                        }
                        sourceStream.CopyTo(destinationStream);
                    }
                    this.user.Image = fileName;
                }
                catch (Exception ex)
                {
                    errorLabel.Content = $"Error copying file: {ex.Message}";
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
                while (!repositoryUOW.GetUserRepository().GetById(user.Id).Equals(user))
                {
                    Thread.Sleep(250);
                }
                DbContext.GetInstance().CloseConnection();
            }
            Visibility = Visibility.Hidden;
            UserProfile profileWindow = new UserProfile();
            profileWindow.messageLabel.Content = success ? "Profile Updated" : "Update Failed";
            profileWindow.Show();
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
