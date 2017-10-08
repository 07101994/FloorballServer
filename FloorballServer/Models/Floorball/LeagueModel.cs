using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FloorballServer.Models.Floorball
{
    [KnownType(typeof(LeagueModel))]
    public class LeagueModel
    {

        public int  Id { get; set; }

        public string Name { get; set; }

        public DateTime Year { get; set; }

        public LeagueTypeEnum Type { get; set; }

        public ClassEnum Class { get; set; }

        public short Rounds { get; set; }

        public CountriesEnum Country { get; set; }

        public GenderEnum Gender { get; set; }

    }
}