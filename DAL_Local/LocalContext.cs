using Bll.Seed;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Local
{
    public class LocalContext : FloorballBaseCtx
    {
        public LocalContext() : base("name=FloorballLocal")
        {
            Seeder = new LocalSeeder();
        }
    }
}
