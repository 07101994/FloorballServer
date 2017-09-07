using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FloorballServer.Models.Floorball
{
    [KnownType(typeof(EventMessageModel))]
    public class EventMessageModel
    {

        public int Id { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }


    }
}