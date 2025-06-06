﻿using MusicCenterModels;
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
using RegistreeModel = MusicCenterModels.Registree;

namespace MusicCenterWPF.Windows.Admin
{
    /// <summary>
    /// Interaction logic for RemoveRegistreeFromGroup.xaml
    /// </summary>
    public partial class RemoveRegistreeFromGroup : Window
    {
        private List<RegistreeModel> registreesInGroup = new List<RegistreeModel>();
        private string selectedGroupId = "";
        private string selectedRegistreeId = "";
        private RepositoryUOW repositoryUOW = new RepositoryUOW();
        public RemoveRegistreeFromGroup()
        {
            InitializeComponent();
            InitializeComponent();
            this.Loaded += (s, e) => LoadGroups();
        }
        private async void LoadGroups()
        {
            WebClient<List<Group>> webClient = new WebClient<List<Group>>();
            webClient.port = 5004;
            webClient.Host = "localhost";
            webClient.Path = "api/Admin/GetGroups";
            List<Group> groups = await webClient.GetAsync();
            groupInput.Items.Clear();
            foreach (var group in groups)
            {
                var item = new ComboBoxItem
                {
                    Content = group.Name,
                    Tag = group.Id
                };
                groupInput.Items.Add(item);
            }
        }
        private void groupInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = groupInput.SelectedItem as ComboBoxItem;
            if (item == null) return;

            selectedGroupId = item.Tag.ToString();
            LoadRegistreesForGroup(selectedGroupId);
        }
        private void LoadRegistreesForGroup(string groupId)
        {
            registreeInput.Items.Clear();
            DbContext.GetInstance().OpenConnection();
            registreesInGroup = repositoryUOW.GetRegistreeRepository().GetAllByGroupId(groupId);
            DbContext.GetInstance().CloseConnection();

            foreach (var registree in registreesInGroup)
            {
                var item = new ComboBoxItem
                {
                    Content = registree.Name,
                    Tag = registree.Id
                };
                registreeInput.Items.Add(item);
            }
        }
        private void registreeInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = registreeInput.SelectedItem as ComboBoxItem;
            if (item != null)
                selectedRegistreeId = item.Tag.ToString();
        }
        private async void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedGroupId) || string.IsNullOrWhiteSpace(selectedRegistreeId))
            {
                MessageBox.Show("Please select both a group and a registree.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            WebClient<Group> webClient = new WebClient<Group>
            {
                port = 5004,
                Host = "localhost",
                Path = "api/Admin/RemoveStudentFromGroup"
            };

            webClient.AddParams("groupID", selectedGroupId);
            webClient.AddParams("userID", selectedRegistreeId);

            bool success = await webClient.PostAsync(new Group());

            MessageBox.Show(success ? "Registree removed from group." : "Failed to remove registree.",
                success ? "Success" : "Error",
                MessageBoxButton.OK,
                success ? MessageBoxImage.Information : MessageBoxImage.Error);
        }
    }
}
