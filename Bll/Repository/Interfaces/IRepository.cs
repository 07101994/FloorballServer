using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Interfaces
{
    public interface IRepository : IDisposable
    {
        #region READ
        IEnumerable<Update> GetUpdatesAfterDate(DateTime date);

        #endregion

        #region CREATE

        int AddUpdate(Update update);

        #endregion


    }
}
