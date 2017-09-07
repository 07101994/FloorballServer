using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FloorballServer.Models.Floorball
{
    [KnownType(typeof(PlayerModel))]
    public class PlayerModel
    {
        public int RegNum { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public short Number { get; set; }

        public DateTime BirthDate { get; set; }


    }
}