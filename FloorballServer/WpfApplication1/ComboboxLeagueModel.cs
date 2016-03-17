using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class ComboboxLeagueModel
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public String Year { get; set; }

        public String DisplayName
        {
            get { return Name + " (" + Year + ")"; }
        }


    }
}
