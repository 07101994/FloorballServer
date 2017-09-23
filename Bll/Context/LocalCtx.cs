using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Context
{
    public class LocalCtx : FloorballBaseCtx
    {
        public LocalCtx() : base("name=FloorballLocal")
        {
        }
    }
}
