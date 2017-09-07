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
using System.Windows.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for AddStadium.xaml
    /// </summary>
    public partial class AddStadiumWindow : Window
    {
        public string StadiumName { get; set; }

        public string Address { get; set; }


        public AddStadiumWindow()
        {
            InitializeComponent();

            nameText.DataContext = this;
            address.DataContext = this;
        }

        private void SaveCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Address != "" && StadiumName != "" && Address != null && StadiumName != null;
        }

        private void SaveCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            DatabaseManager.AddStadium(StadiumName, Address);

            MessageBox.Show("A stadion sikeresen létrejött!", "Sikeres mentés", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            //Application.Current.Dispatcher.Invoke(new Action(() => CommandManager.InvalidateRequerySuggested()));
            //CommandManager.InvalidateRequerySuggested();
            //RaiseInvalidateRequerySuggested();
            //Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { CommandManager.InvalidateRequerySuggested(); }));
        }

    }
}
