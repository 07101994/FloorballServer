using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FloorballServer.Models.Floorball
{
    [KnownType(typeof(StatisticModel))]
    public class StatisticModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public short Number { get; set; }

        public int PlayerRegNum { get; set; }

        public int TeamId { get; set; }

    }
}