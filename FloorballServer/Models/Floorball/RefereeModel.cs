﻿using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FloorballServer.Models.Floorball
{
    public class RefereeModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public short Number { get; set; }

        public short Penalty { get; set; }

        public CountriesEnum Country { get; set; }
    }
}