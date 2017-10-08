using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FloorballServer.Models.Floorball
{
    public class PlayerModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public short Number { get; set; }

        public DateTime BirthDate { get; set; }

        public GenderEnum Gender { get; set; }

    }
}