using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IStadiumRepository : IRepository
    {

        #region READ

        IEnumerable<Stadium> GetAllStadium();

        Stadium GetStadiumById(int id);

        #endregion


        #region CREATE
        int AddStadium(Stadium stadium);

        #endregion

        #region UPDATE

        int UpdateStadium(Stadium stadium);

        #endregion

    }
}
