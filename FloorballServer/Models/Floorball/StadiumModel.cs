using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FloorballServer.Models.Floorball
{
    [KnownType(typeof(StadiumModel))]
    public class StadiumModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

    }
}