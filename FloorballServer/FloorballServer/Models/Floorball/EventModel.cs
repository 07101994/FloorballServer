using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloorballServer.Models.Floorball
{
    public class EventModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Time { get; set; }

        public int MatchId { get; set; }

        public int PlayerId { get; set; }

        public int EventMessageId { get; set; }

        public int TeamId { get; set; }

    }
}