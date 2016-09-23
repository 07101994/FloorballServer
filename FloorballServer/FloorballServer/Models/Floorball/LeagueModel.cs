using Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloorballServer.Models.Floorball
{
    public class LeagueModel
    {

        public int  Id { get; set; }

        public string Name { get; set; }

        public DateTime Year { get; set; }

        public string type { get; set; }

        public string ClassName { get; set; }

        public int Rounds { get; set; }

        public CountriesEnum Country { get; set; }

    }
}