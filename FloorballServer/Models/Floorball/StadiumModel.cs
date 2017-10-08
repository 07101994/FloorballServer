using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FloorballServer.Models.Floorball
{
    public class StadiumModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string PostCode { get; set; }

    }
}