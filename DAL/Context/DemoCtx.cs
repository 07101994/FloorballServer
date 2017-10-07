using DAL.Model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class DemoCtx : FloorballBaseCtx
    {
        public DemoCtx() : base("name=FloorballDemo")
        {
            Database.SetInitializer(Kernel.Get<IDatabaseInitializer<DemoCtx>>());
        }
    }
}
