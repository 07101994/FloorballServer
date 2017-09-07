using Bll;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddLeagueWindow : Window
    {
        public string LeagueName { get; set; }

        public string Year { get; set; }
        public string Type { get; set; }
        public string Class1 { get; set; }

        public string Rounds { get; set; }

        public AddLeagueWindow()
        {
            InitializeComponent();

            years.DataContext = this;
            types.DataContext = this;
            classes.DataContext = this;
            nameText.DataContext = this;
            rounds.DataContext = this;
            
            years.ItemsSource = new string[] { "2015", "2014", "2013"};
            types.ItemsSource = new string[] { "Bajnokság","Kupa","Rájátszás"};
            classes.ItemsSource = new string[] { "Nincs","Férfi OB1","Férfi OB2", "Férfi OB3", "Női OB1", "Női OB2"};

        }

        private void SaveCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = LeagueName != null && Year != null && Type != null && Class1 != null && LeagueName != "" && Rounds != "";
        }

        private void SaveCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            DatabaseManager.AddLeague(LeagueName,DateTime.ParseExact(Year,"yyyy", CultureInfo.InvariantCulture),Type,Class1,Convert.ToInt32(Rounds));

            MessageBox.Show("A bajnokság sikeresen létrejött!", "Siekers mentés", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PreviewNumber(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void DisablePasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!char.IsDigit(text, text.Length - 1))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
