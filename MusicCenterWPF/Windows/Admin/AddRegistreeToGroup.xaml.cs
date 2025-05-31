using Microsoft.AspNetCore.Authorization.Infrastructure;
using MusicCenterModels;
using MusicCenterWebService;
using MusicCenterWebService.Repositories;
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

namespace MusicCenterWPF.Windows.Admin
{
    /// <summary>
    /// Interaction logic for AddRegistreeToGroup.xaml
    /// </summary>
    public partial class AddRegistreeToGroup : Window
    {
        private List<Group> groups = new List<Group>();
        private List<Registree> registrees = new List<Registree>();
        private HashSet<string> registreesInGroup = new HashSet<string>();
        private string selectedGroupId = "";
        private string selectedRegistreeId = "";
        private RepositoryUOW repositoryUOW = new RepositoryUOW();
        public AddRegistreeToGroup()
        {
            DbContext.GetInstance().OpenConnection();
            groups = repositoryUOW.GetGroupRepository().GetAll();
            registrees = repositoryUOW.GetRegistreeRepository().GetAll();
            DbContext.GetInstance().CloseConnection();
            InitializeComponent();
            this.Loaded += (s, e) =>
            {

            };
        }
        private void LoadGroups()
        {
            groupChoice.Items.Clear();
            foreach (var group in groups)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = group.Name;
                item.Tag = group.Id;
                groupChoice.Items.Add(group);
            }
        }
        private void LoadRegistrees()
        {
            foreach (var registree in registrees)
            {
                if (registreesInGroup.Contains(registree.Id))
                {
                    continue;
                }
                var item = new ComboBoxItem();
                item.Content = registree.Name;
                item.Tag = registree.Id;
                registreeChoice.Items.Add(item);
            }
        }

        private void groupChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ComboBoxItem)(groupChoice.SelectedItem);
            selectedGroupId = (string)item.Tag;

            registreesInGroup.Clear();
            DbContext.GetInstance().OpenConnection();
            Group group = repositoryUOW.GetGroupRepository().GetById(selectedGroupId);
            foreach (var registree in repositoryUOW.GetRegistreeRepository().GetAllByGroupId(selectedGroupId))
            {
                registreesInGroup.Add(registree.Id);
            }
            DbContext.GetInstance().CloseConnection();
            if (registreesInGroup.Contains(selectedRegistreeId))
            {
                selectedRegistreeId = "";
                registreeChoice.SelectedIndex = 0;
            }
        }

        private void registreeChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)registreeChoice.SelectedItem;
            selectedRegistreeId = (string)item.Tag;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            WebClient<Group> webClient = new WebClient<Group>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/AddRegistreeToGroup";
            webClient.AddParams("groupID", selectedGroupId);
            webClient.AddParams("registreeID", selectedRegistreeId);
            bool success = await webClient.PostAsync(groups.First());
            if (success)
            {
                MessageBox.Show("Registree added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to add registree.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
