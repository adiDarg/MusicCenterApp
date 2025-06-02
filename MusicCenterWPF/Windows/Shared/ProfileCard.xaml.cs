using MusicCenterModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicCenterWPF.Windows.Shared
{
    /// <summary>
    /// Interaction logic for ProfileCard.xaml
    /// </summary>
    public partial class ProfileCard : UserControl
    {
        public ProfileCard()
        {
            InitializeComponent();
            this.Loaded += (s, e) => {
                LoadCard();
            };
        }
        private void LoadCard()
        {
            LoadImage();
            //Load Username
            usernameLabel.Content = Profile.Name;
            //Load Email
            emailLabel.Content = Profile.Email;
            //Load PhoneNumber
            phoneNumberLabel.Content = Profile.PhoneNumber;
            //Load Address
            addressLabel.Content = Profile.Address;
        }
        private void LoadImage()
        {
            string fileName = Profile.Image;
            string imageUrl = $"http://localhost:5004/api/User/GetImage/{fileName}";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imageUrl, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            image.ImageSource = bitmap;
        }
        public MusicCenterModels.User Profile
        {
            get => (MusicCenterModels.User)GetValue(ProfileProperty);
            set => SetValue(ProfileProperty, value);
        }

        public static readonly DependencyProperty ProfileProperty =
            DependencyProperty.Register(nameof(Profile), typeof(MusicCenterModels.User), typeof(ProfileCard), new PropertyMetadata(null));
    }
}
