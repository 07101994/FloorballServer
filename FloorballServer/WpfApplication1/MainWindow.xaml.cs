using Bll;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Dictionary<int,bool> Ids { get; set; }
        public static bool allowAddingId = true;
        public int selectedRadioButtonName;
        public int selectedId;

        ObservableCollection<ListItemModel> players;

        public MainWindow()
        {
            InitializeComponent();

            Ids = new Dictionary<int, bool>();

            buttonSave.IsEnabled = false;
            buttonBack.IsEnabled = false;

            comboBoxLeague.DataContext = this;

            List<ComboboxLeagueModel> comboboxLeagueModel = new List<ComboboxLeagueModel>(); 
            List<League> leagues = DatabaseManager.GetLeagues();
            foreach (var l in leagues)
            {
                ComboboxLeagueModel model = new ComboboxLeagueModel();
                model.Name = l.Name;
                model.Id = l.Id;
                model.Year = l.Year.ToString("yyyy");
                comboboxLeagueModel.Add(model);
            }
            comboBoxLeague.ItemsSource = comboboxLeagueModel;
            comboBoxLeague.SelectedIndex = 0;

            comboBoxTable.ItemsSource = new List<String>(new String[] {"Player-Team","Player-Match" });
            comboBoxTable.SelectedIndex = 0;


        }

        private void comboBoxLeague_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxTable.SelectedValue == null)
                return;

            loadListBoxes(comboBoxLeague.SelectedValue ,comboBoxTable.SelectedValue);            

        }

        private void comboBoxTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxLeague.SelectedValue == null)
                return;

            loadListBoxes(comboBoxLeague.SelectedValue, comboBoxTable.SelectedValue);
        }

        private void loadListBoxes(object year, object table)
        {

            if (year == null || table == null)
                return;

            List<ListItemModel> modelLeft = new List<ListItemModel>();
            players = new ObservableCollection<ListItemModel>();

            List<Player> playersList = DatabaseManager.GetPlayers();
            foreach (var player in playersList)
            {
                var model = new ListItemModel();
                model.Name = player.Name;
                model.Id = player.RegNum;

                players.Add(model);
            }

            ICollectionView view = CollectionViewSource.GetDefaultView(players);
            new TextSearchFilter(view, filter);


            if (table.ToString() == "Player-Match")
            {

                List<Match> matches = DatabaseManager.GetMatchesByLeague(Convert.ToInt32(comboBoxLeague.SelectedValue));
                foreach (var match in matches)
                {
                    var model = new ListItemModel();
                    model.Name = match.HomeTeam.Name + " - " + match.AwayTeam.Name;
                    model.Id = match.Id;

                    modelLeft.Add(model);
                }

                leftList.ItemsSource = modelLeft;
                rightList.ItemsSource = players;


            }
            else
            {
                if (table.ToString() == "Player-Team")
                {
                    
                    //List<Team> teams = DatabaseManager.GetTeamsByYear(DateTime.ParseExact(year.ToString(), "yyyy", System.Globalization.CultureInfo.InvariantCulture));
                    List<Team> teams = DatabaseManager.GetTeamsByLeague(Convert.ToInt32(comboBoxLeague.SelectedValue));
                    foreach (var team in teams)
                    {
                        var model = new ListItemModel();
                        model.Name = team.Name;
                        model.Id = team.Id;

                        modelLeft.Add(model);
                    }

                    rightList.ItemsSource = players;
                    leftList.ItemsSource = modelLeft;
                }

            }


        }

        private void UpdatePlayers(int id)
        {
            if (comboBoxTable.SelectedValue.ToString() == "Player-Match")
            {
                var match = DatabaseManager.GetMatchById(id);

                while (players.Count != 0)
                {
                    players.RemoveAt(0);
                }

                List<Player> playersList = match.HomeTeam.Players.ToList();
                playersList = playersList.Concat(match.AwayTeam.Players.ToList()).ToList();
                foreach (var player in playersList)
                {
                    var model = new ListItemModel(match.Players.Contains(player) ? true : false);
                    model.Name = player.Name;
                    model.Id = player.RegNum;

                    players.Add(model);
                }

            }
            else
            {
                if (comboBoxTable.SelectedValue.ToString() == "Player-Team")
                {
                    allowAddingId = false;
                    List<Player> playersInTeam = DatabaseManager.GetPlayersByTeam(id);

                    foreach (var p in players)
                    {
                        if (playersInTeam.Select(p1 => p1.RegNum).Contains(p.Id))
                        {
                            p.IsChecked = true;
                        }
                        else
                        {
                            p.IsChecked = false;
                        }
                    }
                    allowAddingId = true;
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

            FrameworkElement element = sender as FrameworkElement;
            int id = Convert.ToInt32(element.Tag);
            selectedId = id;

            UpdatePlayers(id);

        }

        private void MaintainRadioButtons()
        {
            foreach (WrapPanel panel in Helper.FindVisualChildren<WrapPanel>(leftList))
            {
                var box = panel.Children.OfType<RadioButton>().First();
                if (box.IsChecked.Value)
                {
                    box.IsEnabled = true;
                }
                else
                {
                    if (Ids.Count == 0)
                    {
                        box.IsEnabled = true;
                    }
                    else
                    {
                        box.IsEnabled = false;
                    }

                }

            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MaintainRadioButtons();

            if (Ids.Count == 0)
            {
                buttonSave.IsEnabled = false;
                buttonBack.IsEnabled = false;
            }
            else
            {
                buttonSave.IsEnabled = true;
                buttonBack.IsEnabled = true;
            }


        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxTable.SelectedValue.ToString() == "Player-Match")
            {

            }
            else
            {
                if (comboBoxTable.SelectedValue.ToString() == "Player-Team")
                {
                    foreach (var id in Ids)
                    {
                        if (id.Value)
                        {
                            DatabaseManager.AddPlayerToTeam(id.Key,selectedId);
                        }
                        else
                        {
                            DatabaseManager.RemovePlayerFromTeam(id.Key,selectedId);
                        }
                    }
                }
            }
            Ids = new Dictionary<int, bool>();
            buttonSave.IsEnabled = false;
            buttonBack.IsEnabled = false;
            MaintainRadioButtons();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            Ids = new Dictionary<int, bool>();

            UpdatePlayers(selectedId);

            buttonSave.IsEnabled = false;
            buttonBack.IsEnabled = false;
        }
    }
}
