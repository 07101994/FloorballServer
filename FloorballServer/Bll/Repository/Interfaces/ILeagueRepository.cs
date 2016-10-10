using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Interfaces
{
    public interface ILeagueRepository : IDisposable
    {

        #region READ

        League GetLeagueById(int id);

        IEnumerable<League> GetLeaguesByYear(DateTime year);

        IEnumerable<League> GetAllLeague();

        #endregion

        #region CREATE

        int AddLeague(League league);

        #endregion


    }
}
