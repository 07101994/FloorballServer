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

            TeamModel model = new TeamModel
            {
                Id = t.Id,
                LeagueId = t.LeagueId,
                Match = t.MatchNumber,
                Name = t.Name,
                Points = t.Points,
                Scored = t.Scored,
                Standing = t.Standing,
                StadiumId = t.StadiumId,
                TeamId = t.TeamId,
                Year = t.Year,
                Coach = t.Coach,
                Sex = t.Gender,
                Country = t.Country,
                ImageName = t.ImageName
            };

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
            return new MatchModel
            {
                Id = m.Id,
                Date = m.Date,
                AwayTeamId = m.AwayTeamId,
                ScoreA = m.ScoreA,
                ScoreH = m.ScoreH,
                HomeTeamId = m.HomeTeamId,
                LeagueId = m.LeagueId,
                Round = m.Round,
                StadiumId = m.StadiumId,
                State = m.State,
                Time = m.Time
            };
        }

        public static LeagueModel CreateLeagueModel(League l)
        {
            return new LeagueModel
            {
                Id = l.Id,
                Country = l.Country,
                Year = l.Year,
                Name = l.Name,
                Type = l.Type,
                Class = l.Class,
                Gender = l.Gender,
                Rounds = l.Rounds
            };
        }

        public static PlayerModel CreatePlayerModel(Player p)
        {
            return new PlayerModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                BirthDate = p.BirthDate,
                Gender = p.Gender,
                Number = p.Number
            };
        }

        public static RefereeModel CreateRefereeModel(Referee r)
        {
            return new RefereeModel
            {
                Id = r.Id,
                Name = r.Name,
                Number = r.Number,
                Penalty = r.Penalty
            };
        }

        public static StadiumModel CreateStadiumModel(Stadium s)
        {
            return new StadiumModel
            {
                Id = s.Id,
                Name = s.Name,
                Country = s.Country,
                City = s.City,
                PostCode = s.PostCode,
                Address = s.Address
            };
        }

        public static StatisticModel CreateStatisticsModel(Statistic s)
        {
            return new StatisticModel
            {
                Id = s.Id,
                Type = s.Type,
                PlayerId = s.PlayerId,
                Number = s.Number,
                TeamId = s.TeamId
            };
        }

        public static EventModel CreateEventModel(Event e)
        {
            return new EventModel
            {
                Id = e.Id,
                EventMessageId = e.EventMessageId,
                MatchId = e.MatchId,
                PlayerId = e.PlayerId,
                TeamId = e.TeamId,
                Time = e.Time,
                Type = e.Type
            };
        }

        public static EventMessageModel CreateEventMessageModel(EventMessage e)
        {
            return new EventMessageModel
            {
                Id = e.Id,
                Code = e.Code,
                Message = e.Message
            };
        }

    }
}