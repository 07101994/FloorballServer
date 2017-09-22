using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IStatisticRepository : IDisposable
    {

        #region READ

        IEnumerable<Statistic> GetStatisticsByLeague(int leagueId);

        IEnumerable<Statistic> GetAllStatistic();

        #endregion

    }
}
