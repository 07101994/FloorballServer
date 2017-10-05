using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Context
{
    public class RemoteCtx : FloorballBaseCtx
    {
        public RemoteCtx() : base("name=FloorballRemote")
        {
        }
    }
}
