using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Seed
{
    public interface ISeeder
    {
        void Seed(FloorballBaseCtx ctx);
    }
}
