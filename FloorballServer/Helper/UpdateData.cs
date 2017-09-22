using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Helper
{
    public class UpdateData
    {
        public UpdateEnum Type { get; set; }

        public object Entity { get; set; }

        public string UpdateType { get; set; }

    }
}
