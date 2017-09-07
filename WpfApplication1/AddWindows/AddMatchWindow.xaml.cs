using Bll;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApplication1.AddWindows
{
    /// <summary>
    /// Interaction logic for AddMatchWindow.xaml
    /// </summary>
    public partial class AddMatchWindow : Window
    {
        public ObservableCollection<int> RoundsModel { get; set; }
        public ObservableCollection<ComboboxModel> HomeModel { get; set; }
        public ObservableCollection<ComboboxModel> AwayModel { get; set; }

        public DateTime SelectedDate { get; set; }

        public AddMatchWindow()
        {
            InitializeComponent();

            league.DataContext = this;
            round.DataContext = this;
            home.DataContext = this;
            away.DataContext = this;
            date.DataContext = this;
            stadium.DataContext = this;

            league.ItemsSource = ModelHelper.FillLeaguesComboBox();
            league.SelectedIndex = 0;
            stadium.ItemsSource = ModelHelper.FillStadiumsComboBox();
            stadium.SelectedIndex = 0;

            SelectedDate = DatabaseManager.GetLeagueById(Convert.ToInt32(league.SelectedValue)).Year.AddMonths(8);

        }

        private void SaveCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = league.SelectedValue != null && stadium.SelectedValue != null && home.SelectedValue != null && away.SelectedValue != null && round.SelectedValue != null;
        }

        private void SaveCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {

            MessageBox.Show("A mérkőzés sikeresen létrejött!", "Sikeres mentés", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

       
        private void league_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RoundsModel = ModelHelper.FillRoundsComboBox(Convert.ToInt32(league.SelectedValue));
            round.ItemsSource = RoundsModel;
            round.SelectedIndex = 0;

            HomeModel = ModelHelper.FillTeamsComboBox(Convert.ToInt32(league.SelectedValue));
            home.ItemsSource = HomeModel;
            home.SelectedIndex = 0;
            AwayModel = ModelHelper.FillTeamsComboBox(Convert.ToInt32(league.SelectedValue));
            away.ItemsSource = AwayModel;
            away.SelectedIndex = 0;

        }

        private void round_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
