using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FloorballServer.Models.Floorball
{
    public class StatisticModel
    {

        public int Id { get; set; }

        public StatType Type { get; set; }

        public short Number { get; set; }

        public int PlayerId { get; set; }

        public int TeamId { get; set; }

    }
}