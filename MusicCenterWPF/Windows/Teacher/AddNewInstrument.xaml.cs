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

namespace MusicCenterWPF.Windows.Teacher
{
    /// <summary>
    /// Interaction logic for AddNewInstrument.xaml
    /// </summary>
    public partial class AddNewInstrument : Window
    {
        private string instrumentIDselected = "";
        public AddNewInstrument()
        {
            DbContext.GetInstance().OpenConnection();
            RepositoryUOW repository = new RepositoryUOW();
            List<Instrument> instruments = repository.GetInstrumentRepository().GetAll();
            List<Instrument> final = new List<Instrument>();
            HashSet<string> toRemove = repository.GetInstrumentRepository()
            .GetByTeacherID(SessionManager.UserID)
            .Select(instr => instr.Id)
            .ToHashSet();
            foreach (Instrument instr in instruments)
            {
                if (!toRemove.Contains(instr.Id))
                {
                    final.Add(instr);
                }
            }
            DbContext.GetInstance().CloseConnection();
            InitializeComponent();
            this.Loaded += (s, e) => LoadInstruments(final);
        }
        private void LoadInstruments(List<Instrument> instruments)
        {
            foreach (var instrument in instruments)
            {
                ComboBoxItem item = new ComboBoxItem { 
                    Content = instrument.Name,
                    Tag = instrument.Id
                };
            }
        }

        private void instrumentInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)instrumentInput.SelectedItem;
            instrumentIDselected = (string)item.Tag;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            WebClient<Instrument> webClient = new WebClient<Instrument> {
                port = 5004,
                Host = "localhost",
                Path = "api/Teacher/AddInstrument"
            };
            webClient.AddParams("teacherID", SessionManager.UserID);
            webClient.AddParams("instrumentID", instrumentIDselected);
            bool success = await webClient.PostAsync(new Instrument());
            MessageBox.Show(success ? "Instrument added." : "Failed to add instrument.",
                success ? "Success" : "Error",
                MessageBoxButton.OK,
                success ? MessageBoxImage.Information : MessageBoxImage.Error);
        }
    }
}
