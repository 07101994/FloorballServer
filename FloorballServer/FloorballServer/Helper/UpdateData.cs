using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.UpdateFolder
{
    public class UpdateData
    {
        public UpdateType Type { get; set; }

        public object Entity { get; set; }

        public bool isAdding { get; set; }

    }
}
