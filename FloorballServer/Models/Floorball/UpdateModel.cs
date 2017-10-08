using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WebApi.Helper;

namespace FloorballServer.Models.Floorball
{
    public class UpdateModel
    {
        public List<UpdateData> Updates { get; set; }

        public DateTime UpdateTime { get; set; }

    }
}