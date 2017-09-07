using Bll;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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
using WpfApplication1.Model;

namespace WpfApplication1
{

    /// <summary>
    /// Interaction logic for EditMatchWindow.xaml
    /// </summary>
    public partial class EditMatchWindow : Window, INotifyPropertyChanged
    {
        Match m;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool DeleteCanExcecute { get; set; }


        public ObservableCollection<EventModel> EventList { get; set; }

        public string ActualTime { get; set; }

        public string NewTime { get; set; }

        public ObservableCollection<string> EventTypes { get; set; }

        public ObservableCollection<ComboboxModel> FirstCombobox { get; set; }

        public ObservableCollection<ComboboxModel> SecondCombobox { get; set; }

        public ObservableCollection<ComboboxModel> ThirdCombobox { get; set; }


        public bool MatchWillBe { get; set; }

        public bool MatchInProgress { get; set; }

        public bool MatchFinished { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public ObservableCollection<ComboboxModel> Teams { get; set; }


        public EditMatchWindow(Match m)
        {
            InitializeComponent();

            this.m = m;

            matchFinished.DataContext = this;
            matchInProgress.DataContext = this;
            matchWillBe.DataContext = this;
            events.DataContext = this;
            actualTime.DataContext = this;
            newTime.DataContext = this;
            eventTypes.DataContext = this;
            teamCombobox.DataContext = this;
            firstCombobox.DataContext = this;
            secondCombobox.DataContext = this;
            thirdCombobox.DataContext = this;
            homeGoals.DataContext = this;
            awayGoals.DataContext = this;

            switch (m.State)
            {
                case "lejátszott":
                    MatchFinished = true;
                    break;
                case "élő":
                    MatchInProgress = true;
                    break;
                case "lejelentett":
                    MatchWillBe = true;
                    break;
                default:
                    break;
            }

            leagueName.Content = m.League.Name;
            round.Content = m.Round + ". forduló";
            homeTeamName.Content = m.HomeTeam.Name;
            awayTeamName.Content = m.AwayTeam.Name;
            HomeGoals = m.GoalsH;
            AwayGoals = m.GoalsA;

            ActualTime = m.Time.TotalHours == 1 ?  "60:00" : m.Time.ToString(@"mm\:ss");
            NewTime = ActualTime != "60:00" ? ActualTime : "59:59";

            EventList = ModelHelper.FillEventsList(m.Id);
           
            EventTypes = new ObservableCollection<string>() {"Gól","Kiállítás","Időkérés","Büntető"};
            Teams = ModelHelper.FillHomeAndAwayTeamCombobox(m);

            eventTypes.SelectedIndex = 0;
            teamCombobox.SelectedIndex = 0;

            DeleteCanExcecute = true;
           
        }

        private void eventTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Team selectedTeam;
            if (Convert.ToInt32(teamCombobox.SelectedValue) == m.HomeTeamId)
                selectedTeam = m.HomeTeam;
            else
                selectedTeam = m.AwayTeam;

            if (eventTypes.SelectedIndex == 0)
            {

                FirstCombobox = ModelHelper.FillComboboxTeamMembersPlayedInMatch(selectedTeam, m);
                OnPropertyChanged("FirstCombobox");
                SecondCombobox = ModelHelper.FillComboboxTeamMembersPlayedInMatch(selectedTeam, m);
                OnPropertyChanged("SecondCombobox");
                ThirdCombobox = ModelHelper.FillEventMessageCombobox("G");
                OnPropertyChanged("ThirdCombobox");

                firstComboboxLabel.Content = "Gól";
                secondComboboxLabel.Content = "Assziszt";
                thirdComboboxLabel.Content = "Típus";

                thirdCombobox.SelectedIndex = 0;

                secondCombobox.IsEnabled = true;
                thirdCombobox.IsEnabled = true;
            }
            else
            {
                if (eventTypes.SelectedIndex == 1) 
                {
                    FirstCombobox = ModelHelper.FillComboboxTeamMembersPlayedInMatch(selectedTeam, m);
                    OnPropertyChanged("FirstCombobox");
                    SecondCombobox = ModelHelper.FillComboboxFromStringList(new List<string>(new string[] { "2 perc", "5 perc", "2+10 perc", "Végleges" }));
                    OnPropertyChanged("SecondCombobox");
                    ThirdCombobox = null;
                    OnPropertyChanged("ThirdCombobox");

                    firstComboboxLabel.Content = "Kiállított";
                    secondComboboxLabel.Content = "Időtartalom";
                    thirdComboboxLabel.Content = "Típus";


                    secondCombobox.IsEnabled = true;
                    thirdCombobox.IsEnabled = true;
                }
                else
                {
                    if (eventTypes.SelectedIndex == 2)
                    {

                        FirstCombobox = null;
                        OnPropertyChanged("FirstCombobox");
                        SecondCombobox = null;
                        OnPropertyChanged("SecondCombobox");
                        ThirdCombobox = null;
                        OnPropertyChanged("ThirdCombobox");


                        firstComboboxLabel.Content = "";
                        secondComboboxLabel.Content = "";
                        thirdComboboxLabel.Content = "";

                        firstCombobox.IsEnabled = false;
                        secondCombobox.IsEnabled = false;
                        thirdCombobox.IsEnabled = false;
                    }
                    else
                    {
                        if (eventTypes.SelectedIndex == 3)
                        {

                            FirstCombobox = ModelHelper.FillComboboxTeamMembersPlayedInMatch(selectedTeam, m);
                            OnPropertyChanged("FirstCombobox");
                            SecondCombobox = null;
                            OnPropertyChanged("SecondCombobox");
                            ThirdCombobox = null;
                            OnPropertyChanged("ThirdCombobox");

                            firstComboboxLabel.Content = "Játékos";
                            secondComboboxLabel.Content = "";
                            thirdComboboxLabel.Content = "";

                            firstCombobox.IsEnabled = true;
                            secondCombobox.IsEnabled = false;
                            thirdCombobox.IsEnabled = false;
                        }
                    }
                }
            }
        }

        private void secondCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (eventTypes.SelectedIndex == 1)
            {
                if (secondCombobox.SelectedIndex == 0)
                {
                    ThirdCombobox = ModelHelper.FillEventMessageCombobox("P2");
                    OnPropertyChanged("ThirdCombobox");

                }
                else
                {
                    if (secondCombobox.SelectedIndex == 1)
                    {
                        ThirdCombobox = ModelHelper.FillEventMessageCombobox("P5");
                        OnPropertyChanged("ThirdCombobox");
                        
                    }
                    else
                    {
                        if (secondCombobox.SelectedIndex == 2)
                        {

                            ThirdCombobox = ModelHelper.FillEventMessageCombobox("P10");
                            OnPropertyChanged("ThirdCombobox");

                        }
                        else
                        {

                            ThirdCombobox = ModelHelper.FillEventMessageCombobox("V");
                            OnPropertyChanged("ThirdCombobox");

                        }
                    }
                }
            }
        }

        private void AddCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = eventTypes.SelectedValue != null && firstCombobox.SelectedValue != null &&
                           (secondCombobox.IsEnabled ? secondCombobox.SelectedValue != null : true) &&
                           (thirdCombobox.IsEnabled ? thirdCombobox.SelectedValue != null : true);
        }

        private void AddCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            TimeSpan time = TimeSpan.ParseExact(NewTime, "mm\\:ss", CultureInfo.InvariantCulture);

            if (eventTypes.SelectedIndex == 0)
            {
                int e1 = DatabaseManager.AddEvent(m.Id, "G", time, Convert.ToInt32(firstCombobox.SelectedValue), Convert.ToInt32(thirdCombobox.SelectedValue), Convert.ToInt32(teamCombobox.SelectedValue));
                int e2 = DatabaseManager.AddEvent(m.Id, "A", time, Convert.ToInt32(secondCombobox.SelectedValue), Convert.ToInt32(thirdCombobox.SelectedValue), Convert.ToInt32(teamCombobox.SelectedValue));

            }
            else
            {
                if (eventTypes.SelectedIndex == 1)
                {
                    int index = secondCombobox.SelectedIndex;
                    string type;
                    if (index == 0)
                    {
                        type = "P2";
                    } else
                    {
                        if (index == 1)
                        {
                            type = "P5";
                        }
                        else
                        {
                            if (index == 2)
                            {
                                type = "P10";
                            }
                            else
                            {
                                type = "PV";
                            }
                        }
                    }
                    
                    int e1 = DatabaseManager.AddEvent(m.Id,type,time, Convert.ToInt32(firstCombobox.SelectedValue), Convert.ToInt32(thirdCombobox.SelectedValue), Convert.ToInt32(teamCombobox.SelectedValue));
                }
                else
                {
                    if (eventTypes.SelectedIndex == 2)
                    {
                        int e1 = DatabaseManager.AddEvent(m.Id, "I", time, 46, -1  , Convert.ToInt32(teamCombobox.SelectedValue));
                    }
                    else
                    {
                        int e1 = DatabaseManager.AddEvent(m.Id, "B", time, Convert.ToInt32(firstCombobox.SelectedValue), 47, -1);
                    }
                }
            }

            EventList = ModelHelper.FillEventsList(m.Id);
            OnPropertyChanged("EventList");
        }

        private void teamCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Team selectedTeam;
            if (Convert.ToInt32(teamCombobox.SelectedValue) == m.HomeTeamId)
                selectedTeam = m.HomeTeam;
            else
                selectedTeam = m.AwayTeam;

            if (eventTypes.SelectedIndex == 0)
            {

                FirstCombobox = ModelHelper.FillComboboxTeamMembersPlayedInMatch(selectedTeam, m);
                OnPropertyChanged("FirstCombobox");
                SecondCombobox = ModelHelper.FillComboboxTeamMembersPlayedInMatch(selectedTeam, m);
                OnPropertyChanged("SecondCombobox");

                firstComboboxLabel.Content = "Gól";
                secondComboboxLabel.Content = "Assziszt";
                thirdComboboxLabel.Content = "Típus";


                secondCombobox.IsEnabled = true;
                thirdCombobox.IsEnabled = true;

                firstCombobox.SelectedIndex = 0;
                secondCombobox.SelectedIndex = 0;
            }
            else
            {
                if (eventTypes.SelectedIndex == 1)
                {
                    FirstCombobox = ModelHelper.FillComboboxTeamMembersPlayedInMatch(selectedTeam, m);
                    OnPropertyChanged("FirstCombobox");
                    SecondCombobox = ModelHelper.FillComboboxFromStringList(new List<string>(new string[] { "2 perc", "5 perc", "2+10 perc", "Végleges" }));
                    OnPropertyChanged("SecondCombobox");

                    firstComboboxLabel.Content = "Kiállított";
                    secondComboboxLabel.Content = "Időtartalom";
                    thirdComboboxLabel.Content = "Típus";


                    secondCombobox.IsEnabled = true;
                    thirdCombobox.IsEnabled = true;
                }
                else
                {
                    if (eventTypes.SelectedIndex == 2)
                    {

                        FirstCombobox = ModelHelper.FillHomeAndAwayTeamCombobox(m);
                        OnPropertyChanged("FirstCombobox");
                        SecondCombobox = null;
                        OnPropertyChanged("SecondCombobox");
                        ThirdCombobox = null;
                        OnPropertyChanged("ThirdCombobox");


                        firstComboboxLabel.Content = "Csapat";
                        secondComboboxLabel.Content = "";
                        thirdComboboxLabel.Content = "";

                        secondCombobox.IsEnabled = false;
                        thirdCombobox.IsEnabled = false;
                    }
                    else
                    {
                        if (eventTypes.SelectedIndex == 3)
                        {
                            FirstCombobox = ModelHelper.FillHomeAndAwayTeamCombobox(m);
                            OnPropertyChanged("FirstCombobox");
                            SecondCombobox = null;
                            OnPropertyChanged("SecondCombobox");
                            ThirdCombobox = null;
                            OnPropertyChanged("ThirdCombobox");

                            firstComboboxLabel.Content = "Csapat";
                            secondComboboxLabel.Content = "";
                            thirdComboboxLabel.Content = "";

                            secondCombobox.IsEnabled = false;
                            thirdCombobox.IsEnabled = false;
                        }
                    }
                }
            }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            int eventId = Convert.ToInt32(button.Tag);

            DatabaseManager.RemoveEvent(eventId);

            EventList = ModelHelper.FillEventsList(m.Id);

        }

        private void DeleteCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = DeleteCanExcecute;
        }

        private void DeleteCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            DeleteCanExcecute = false;

            Button button = (Button) e.OriginalSource;

            int eventId = Convert.ToInt32(button.Tag);

            DatabaseManager.RemoveEvent(eventId);

            EventList = ModelHelper.FillEventsList(m.Id);
            OnPropertyChanged("EventList");

            DeleteCanExcecute = true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
