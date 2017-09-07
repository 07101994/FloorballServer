using Bll;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Model;

namespace WpfApplication1
{
    public class ModelHelper
    {

        public static ObservableCollection<ComboboxModel> FillLeaguesComboBox()
        {
            ObservableCollection<ComboboxModel> comboboxModel = new ObservableCollection<ComboboxModel>();
            List<League> list = DatabaseManager.GetAllLeague();
            foreach (var l in list)
            {
                ComboboxModel model = new ComboboxModel();
                model.Name = l.Name + " - " + l.Year.ToString("yyyy");
                model.Id = l.Id;
                comboboxModel.Add(model);
            }

            return comboboxModel;
        }

        public static ObservableCollection<ComboboxModel> FillStadiumsComboBox()
        {
            ObservableCollection<ComboboxModel> comboboxModel = new ObservableCollection<ComboboxModel>();
            List<Stadium> list = DatabaseManager.GetAllStadium();
            foreach (var l in list)
            {
                ComboboxModel model = new ComboboxModel();
                model.Name = l.Name;
                model.Id = l.Id;
                comboboxModel.Add(model);
            }

            return comboboxModel;
        }

        public static ObservableCollection<ComboboxModel> FillTeamsComboBox(int leagueId)
        {
            ObservableCollection<ComboboxModel> comboboxModel = new ObservableCollection<ComboboxModel>();
            List<Team> list = DatabaseManager.GetTeamsByLeague(leagueId);
            foreach (var l in list)
            {
                ComboboxModel model = new ComboboxModel();
                model.Name = l.Name;
                model.Id = l.Id;
                comboboxModel.Add(model);
            }

            return comboboxModel;
        }

        public static ObservableCollection<int> FillRoundsComboBox(int leagueId)
        {
            ObservableCollection<int> comboboxModel = new ObservableCollection<int>();
            int round = DatabaseManager.GetNumberOfRoundsInLeague(leagueId);
            for (int i = 1; i <= round; i++)
            {
                comboboxModel.Add(i);
            }

            return comboboxModel;
        }

        public static ObservableCollection<EventModel> FillEventsList(int matchId)
        {
            ObservableCollection<EventModel> eventModels = new ObservableCollection<EventModel>();

            List<Event> events = DatabaseManager.GetEventsByMatch(matchId);

            foreach (var e in events)
            {

                if (e.Type != "A")
                {
                    EventModel model = new EventModel();

                    if (e.Type == "G")
                    {
                        model.EventText = "Gól: " + e.Player.Name;
                        model.EventText += " Assziszt: " + events.Find(ev => ev.Time == e.Time && ev.Type == "A").Player.Name;
                     
                        model.EventText += e.EventMessage.Code != 605 ? " - " + e.EventMessage.Message + " gól" : "";

                    }
                    else
                    {
                        if (e.Type == "P2")
                        {
                            model.EventText = "2 perc: " + e.Player.Name + " kiállították.";
                            model.EventText += " Megnevezés: " + e.EventMessage.Message;
                        }
                        else
                        {
                            if (e.Type == "P5")
                            {
                                model.EventText = "5 perc: " + e.Player.Name + " kiállították.";
                                model.EventText += " Megnevezés: " + e.EventMessage.Message;
                            }
                            else
                            {
                                if (e.Type == "P10")
                                {
                                    model.EventText = "10 perc: " + e.Player.Name + " kiállították.";
                                    model.EventText += " Megnevezés: " + e.EventMessage.Message;
                                }
                                else
                                {
                                    if (e.Type == "PV")
                                    {
                                        model.EventText = "Végleges: " + e.Player.Name + " kiállították.";
                                        model.EventText += " Megnevezés: " + e.EventMessage.Message;
                                    }
                                    else
                                    {
                                        if (e.Type == "I")
                                        {
                                            //model.EventText = "Időkérérés: " +  + " kiállították.";
                                            //model.EventText += " Megnevezés: " + e.EventMessage.Message;
                                        }
                                        else
                                        {
                                            if (e.Type == "B")
                                            {
                                                model.EventText = "Büntető rontás: " + e.Player.Name + " büntetőt rontott.";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    model.Time = e.Time.ToString(@"mm\:ss");
                    model.Id = e.Id;

                    eventModels.Add(model);
                }
                
            }

            return eventModels;
        }

        public static ObservableCollection<ComboboxModel> FillPlayerCombobox(Match m)
        {
            ObservableCollection<ComboboxModel> comboboxModel = new ObservableCollection<ComboboxModel>();
            List<Player> players = m.Players.ToList();
            foreach (var player in players)
            {
                ComboboxModel model = new ComboboxModel();
                model.Name = player.Name;
                model.Id = player.Number;

                comboboxModel.Add(model);
            }

            return comboboxModel;
        }

        public static ObservableCollection<ComboboxModel> FillEventMessageCombobox(string eventType)
        {
            ObservableCollection<ComboboxModel> comboboxModel = new ObservableCollection<ComboboxModel>();
            char category = ' ';

            if (eventType == "P2")
            {
                category = '2';
            } else
            {
                if (eventType == "P5")
                {
                    category = '5';
                }
                else
                {
                    if (eventType == "P10")
                    {
                        category = '1';
                    }
                    else
                    {
                        if (eventType == "V")
                        {
                            category = '3';
                        }
                        else
                        {
                            if (eventType == "G")
                            {
                                category = '6';
                            }
                            else
                            {
                                if (eventType == "B" || eventType == "I")
                                {
                                    category = '4';
                                }
                            }
                        }
                    }
                }
            }

            List<EventMessage> eventMessages = DatabaseManager.GetEventMessagesByCategory(category);
            foreach (var e in eventMessages)
            {
                ComboboxModel model = new ComboboxModel();
                model.Name = e.Message + " (" + e.Code + ")";
                model.Id = e.Id;

                comboboxModel.Add(model);
            }

            return comboboxModel;
        }

        public static ObservableCollection<ComboboxModel> FillHomeAndAwayTeamCombobox(Match m)
        {
            ObservableCollection<ComboboxModel> comboboxModel = new ObservableCollection<ComboboxModel>();
            List<Team> teams = new List<Team>();
            teams.Add(m.HomeTeam);
            teams.Add(m.AwayTeam);
            foreach (var team in teams)
            {
                ComboboxModel model = new ComboboxModel();
                model.Name = team.Name;
                model.Id = team.Id;

                comboboxModel.Add(model);
            }

            return comboboxModel;
        }

        public static ObservableCollection<ComboboxModel> FillComboboxFromStringList(List<string> list)
        {
            ObservableCollection<ComboboxModel> comboboxModel = new ObservableCollection<ComboboxModel>();

            foreach (var l in list)
            {
                ComboboxModel model = new ComboboxModel();
                model.Name = l;
                model.Id = -1;

                comboboxModel.Add(model);
            }

            return comboboxModel;
        }

        public static ObservableCollection<ComboboxModel> FillComboboxTeamMembersPlayedInMatch(Team t, Match m)
        {
            ObservableCollection<ComboboxModel> comboboxModel = new ObservableCollection<ComboboxModel>();

            foreach (var p in t.Players)
            {
                if (m.Players.Contains(p))
                {
                    ComboboxModel model = new ComboboxModel();
                    model.Name = p.Name + " (" + p.Number + ")";
                    model.Id = p.RegNum;

                    comboboxModel.Add(model);
                }
                
            }

            return comboboxModel;
        }

        //public static EventModel CreateEventModel(int eventId1, int eventId2)
        //{

        //    Event e1 = DatabaseManager.GetEvent(eventId1);

        //    EventModel model = new EventModel();
        //    model.Id = e1.Id;
        //    model.Time = e1.Time.ToString(@"mm\:ss");

        //    if (e1.Type == "G")
        //    {

        //        Event e2 = DatabaseManager.GetEvent(eventId2);

            
        //        model.EventText = "Gól: " + e1.Player.Name;
        //        model.EventText += " Assziszt: " + e2.Player.Name;
        //        model.EventText += e1.EventMessage.Code != 605 ? " - " + e1.EventMessage.Message + " gól" : "";

        //    }


        //    return model;
        //}

    }
}
