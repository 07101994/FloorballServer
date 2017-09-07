using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class ComboboxModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName
        {
            get { return Id != -1 ? Name + " (" + Id + ")" : Name; }
        }


    }
}
