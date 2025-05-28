using System.Windows;
using System.Windows.Controls;
using MusicCenterWebService;
using MusicCenterWebService.Repositories;
using WebApiClient;
using UserClass = MusicCenterModels.User;
namespace MusicCenterWPF.Windows
{
    /// <summary>
    /// Interaction logic for EnterValidationKey.xaml
    /// </summary>
    public partial class EnterValidationKey : Window
    {
        private RepositoryUOW repositoryUOW = new RepositoryUOW();
        private UserClass user;
        public EnterValidationKey()
        {
            DbContext db = DbContext.GetInstance();
            db.OpenConnection();
            user = repositoryUOW.GetUserRepository().GetById(SessionManager.UserID);
            db.CloseConnection();
            InitializeComponent();
        }

        private async void Submit_ClickAsync(object sender, RoutedEventArgs e)
        {
            string key = inputTextBox.Text;
            WebClient<UserClass> webClient = new WebClient<UserClass>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/User/EnterValidationKey";
            webClient.AddParams("userID", user.Id);
            webClient.AddParams("key", key);
            bool result = await webClient.PostAsync(user);
            if (result) {
                SessionManager.Type = "Registree";
            }
            else
            {
                errorLabel.Content = "Error in validation key entry(maybe the key entered was wrong?)";
            }
        }
    }
}
