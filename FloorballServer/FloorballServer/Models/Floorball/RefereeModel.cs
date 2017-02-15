using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FloorballServer.Models.Floorball
{
    [KnownType(typeof(RefereeModel))]
    public class RefereeModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public short Number { get; set; }

        public short Penalty { get; set; }
    }
}