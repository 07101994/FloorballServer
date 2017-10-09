using DAL.Model;
using DAL.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Demo
{
    public class DemoContext : FloorballBaseCtx
    {
        public DemoContext() : base("name=FloorballDemo")
        {
            Seeder = new DemoSeeder();
        }
    }
}
