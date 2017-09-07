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
    /// Interaction logic for AddPlayerWindow.xaml
    /// </summary>
    public partial class AddPlayerWindow : Window
    {
        public string PlayerName { get; set; }
        public string Id { get; set; }
        public string Number { get; set; }
        public DateTime BirthDate { get; set; }


        public AddPlayerWindow()
        {
            InitializeComponent();

            nameText.DataContext = this;
            id.DataContext = this;
            number.DataContext = this;
            birthdate.DataContext = this;

            BirthDate = DateTime.Now.AddYears(-20);
        }

        private void SaveCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = BirthDate != null && PlayerName != null && PlayerName != "" && Id != null && Id != "" && Number != null && Number != "";
        }

        private void SaveCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {

            DatabaseManager.AddPlayer(PlayerName, Convert.ToInt32(Id), Convert.ToInt16(Number), BirthDate);

            MessageBox.Show("A játékos sikeresen létrejött!", "Sikeres mentés", MessageBoxButton.OK, MessageBoxImage.Information);
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
