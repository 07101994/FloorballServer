using DAL;
using DAL.Model;
using FloorballServer.Models.Floorball;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloorballServer.Helper
{
    public class ModelHelper
    {

        public static TeamModel CreateTeamModel(Team t, bool withImage)
        {
            
            TeamModel model = new TeamModel();
            model.Id = t.Id;
            model.LeagueId = t.LeagueId;
            model.Match = t.Match;
            model.Name = t.Name;
            model.Points = t.Points;
            model.Scored = t.Scored;
            model.Standing = t.Standing;
            model.StadiumId = t.StadiumId;
            model.TeamId = t.TeamId;
            model.Year = t.Year;
            model.Coach = t.Coach;
            model.Sex = t.Sex;
            model.Country = t.Country.ToEnum<CountriesEnum>();
            model.ImageName = t.ImageName;
            if (withImage)
            {
                try
                {
                    model.Image = ImageManager.GetImage(t.ImageName);
                }
                catch (Exception)
                {
                }
            }

            return model;
        }

        public static MatchModel CreateMatchModel(Match m)
        {
            MatchModel model = new MatchModel();
            model.AwayTeamId = m.AwayTeamId;
            model.HomeTeamId = m.HomeTeamId;
            model.Date = m.Date;
            model.GoalsA = m.GoalsA;
            model.GoalsH = m.GoalsH;
            model.Id = m.Id;
            model.LeagueId = m.LeagueId;
            model.Round = m.Round;
            model.StadiumId = m.StadiumId;
            model.State = m.State;
            model.Time = m.Time;

            //model.Players = new List<PlayerModel>();
            //foreach (var player in m.Players)
            //{
            //    model.Players.Add(CreatePlayerModel(player));
            //}

            return model;
        }

        public static LeagueModel CreateLeagueModel(League l)
        {
            LeagueModel model = new LeagueModel();
            model.Id = l.Id;
            model.Name = l.Name;
            model.type = l.Type;
            model.Year = l.Year;
            model.Rounds = l.Rounds;
            model.ClassName = l.ClassName;
            model.Country = l.Country.ToEnum<CountriesEnum>();
            model.Sex = l.Sex;

            return model;
        }

        public static PlayerModel CreatePlayerModel(Player p)
        {
            PlayerModel model = new PlayerModel();
            model.BirthDate = p.Date;
            model.FirstName = p.FirstName;
            model.SecondName = p.SecondName;
            model.Number = p.Number;
            model.RegNum = p.RegNum;

            return model;
        }

        public static RefereeModel CreateRefereeModel(Referee r)
        {
            RefereeModel model = new RefereeModel();
            model.Id = r.Id;
            model.Name = r.Name;
            model.Number = r.Number;
            model.Penalty = r.Penalty;

            return model;
        }

        public static StadiumModel CreateStadiumModel(Stadium s)
        {
            StadiumModel model = new StadiumModel();
            model.Address = s.Address;
            model.Id = s.Id;
            model.Name = s.Name;

            return model;
        }

        public static StatisticModel CreateStatisticsModel(Statistic s)
        {
            StatisticModel model = new StatisticModel();
            model.Id = s.Id;
            model.Name = s.Name;
            model.Number = s.Number;
            model.PlayerRegNum = s.PlayerRegNum;
            model.TeamId = s.TeamId;

            return model;
        }

        public static EventModel CreateEventModel(Event e)
        {
            EventModel model = new EventModel();
            model.EventMessageId = e.EventMessageId;
            model.Id = e.Id;
            model.MatchId = e.MatchId;
            model.PlayerId = e.PlayerRegNum;
            model.Time = e.Time;//.ToString(@"h\h\:m\m\:s\s", System.Globalization.CultureInfo.InvariantCulture);
            model.Type = e.Type;
            model.TeamId = e.TeamId;

            return model; 
        }

        public static EventMessageModel CreateEventMessageModel(EventMessage e)
        {
            EventMessageModel model = new EventMessageModel();
            model.Code = e.Code;
            model.Id = e.Id;
            model.Message = e.Message;

            return model;
        }

    }
}