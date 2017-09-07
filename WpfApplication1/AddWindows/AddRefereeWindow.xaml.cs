using Bll;
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

namespace WpfApplication1.AddWindows
{
    /// <summary>
    /// Interaction logic for AddRefereeWindow.xaml
    /// </summary>
    public partial class AddRefereeWindow : Window
    {
        public string RefereeName { get; set; }

        public AddRefereeWindow()
        {
            InitializeComponent();

            nameText.DataContext = this;
        }

        private void SaveCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = RefereeName != null && RefereeName != "";
        }

        private void SaveCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            DatabaseManager.AddReferee(RefereeName);

            MessageBox.Show("A bíró sikeresen létrejött!", "Sikeres mentés", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
