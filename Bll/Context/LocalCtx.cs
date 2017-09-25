using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::Ninject;

namespace Bll.Context
{
    public class LocalCtx : FloorballBaseCtx
    {
        public LocalCtx() : base("name=FloorballLocal")
        {
            Database.SetInitializer(Kernel.Get<IDatabaseInitializer<LocalCtx>>());
        }
    }
}
