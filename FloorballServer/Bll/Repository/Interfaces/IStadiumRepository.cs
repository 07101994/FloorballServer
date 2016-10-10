using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Interfaces
{
    public interface IStadiumRepository : IDisposable
    {

        #region READ

        IEnumerable<Stadium> GetAllStadium();

        Stadium GetStadiumById(int id);

        #endregion


        #region CREATE
        int AddStadium(Stadium stadium);

        #endregion

    }
}
