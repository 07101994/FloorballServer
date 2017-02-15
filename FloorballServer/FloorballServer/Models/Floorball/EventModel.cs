using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Script.Serialization;

namespace FloorballServer.Models.Floorball
{
    [KnownType(typeof(EventModel))]
    public class EventModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public TimeSpan Time { get; set; }

        //public string Time { get; set; }

        public int MatchId { get; set; }

        public int PlayerId { get; set; }

        public int EventMessageId { get; set; }

        public int TeamId { get; set; }

    }
}