using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FloorballServer.Models.Floorball
{
    [KnownType(typeof(MatchModel))]
    public class MatchModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public short Round { get; set; }

        public StateEnum State { get; set; }

        public short ScoreH { get; set; }

        public short ScoreA { get; set; }

        public TimeSpan Time { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int LeagueId { get; set; }

        public int StadiumId { get; set; }

    }
}