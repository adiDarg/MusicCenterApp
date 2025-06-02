using MusicCenterWPF.Windows.Shared;
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
using UserModel = MusicCenterModels.User;

namespace MusicCenterWPF.Windows.Registree
{
    /// <summary>
    /// Interaction logic for ViewTeachers.xaml
    /// </summary>
    public partial class ViewTeachers : Window
    {
        public ViewTeachers()
        {
            InitializeComponent();
            this.Loaded += async (s, e) => {
                WebClient<List<UserModel>> webClient = new WebClient<List<UserModel>>{
                    port = 5004,
                    Host = "localhost",
                    Path = "api/Registree/ViewAllTeachers"
                };
                List<UserModel> users = await webClient.GetAsync();
                LoadProfiles(users); 
            };
        }
        private void LoadProfiles(List<UserModel> users)
        {
            int row = 0;
            int column = 0;
            profileCardsGrid.RowDefinitions.Add(new RowDefinition());
            foreach (var user in users)
            {
                ProfileCard card = new ProfileCard();
                card.Profile = user;

                Border border = new Border { 
                    Style = this.FindResource("CellBorderStyle") as Style,
                    Child = card
                };
                Grid.SetRow(border, row);
                Grid.SetColumn(border, column);
                profileCardsGrid.Children.Add(border);

                column++;
                if (column == 4)
                {
                    row++;
                    column = 0;
                    profileCardsGrid.RowDefinitions.Add(new RowDefinition());
                }
            }
        }
    }
}
