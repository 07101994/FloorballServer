using Bll;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for AddTeamWindow.xaml
    /// </summary>
    public partial class AddTeamWindow : Window
    {
        public string Year { get; set; }
        public string TeamName { get; set; }
        public string Coach { get; set; }

        public AddTeamWindow()
        {
            InitializeComponent();

            nameText.DataContext = this;
            years.DataContext = this;
            coach.DataContext = this;
            stadium.DataContext = this;
            league.DataContext = this;

            league.ItemsSource = ModelHelper.FillLeaguesComboBox();
            stadium.ItemsSource = ModelHelper.FillStadiumsComboBox();
            years.ItemsSource = new string[] { "2015", "2014", "2013" };

        }

        private void SaveCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Year != null && TeamName != null && TeamName != "" && Coach != null && Coach != "" && stadium.SelectedValue != null && league.SelectedValue != null;
        }

        private void SaveCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            DatabaseManager.AddTeam(TeamName, DateTime.ParseExact(Year, "yyyy", CultureInfo.InvariantCulture), Coach, Convert.ToInt32(stadium.SelectedValue), Convert.ToInt32(league.SelectedValue));

            MessageBox.Show("A csapat sikeresen létrejött!", "Sikeres mentés", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
