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

namespace MusicCenterWPF.Windows.Admin
{
    /// <summary>
    /// Interaction logic for DisplayMembers.xaml
    /// </summary>
    public partial class DisplayMembers : Window
    {
        public DisplayMembers()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                LoadProfiles("Users");
            };
        }
        private async void LoadProfiles(string type)
        {
            WebClient<List<UserModel>> webClient = new WebClient<List<UserModel>>
            {
                port = 5004,
                Host = "localhost",
                Path = $"api/Admin/Get{type}"
            };
            List<UserModel> users = await webClient.GetAsync();
            int row = 0;
            int column = 0;
            profileCardsGrid.RowDefinitions.Add(new RowDefinition());
            foreach (var user in users)
            {
                ProfileCard card = new ProfileCard();
                card.Profile = user;

                Border border = new Border
                {
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

        private void typeChoiceBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            profileCardsGrid.RowDefinitions.Clear();
            profileCardsGrid.Children.Clear();
            try
            {
                LoadProfiles((typeChoiceBox.SelectedItem as ComboBoxItem).Content as string);
            }
            catch (Exception ex) { 
                MessageBox.Show("an issue occured: " + ex,"Error",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
