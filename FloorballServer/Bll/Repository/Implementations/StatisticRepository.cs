using Bll.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Implementations
{
    class StatisticRepository : Repository, IStatisticRepository
    {

        public IEnumerable<Statistic> GetAllStatistic()
        {
            return ctx.Statistics;
        }

        public IEnumerable<Statistic> GetStatisticsByLeague(int leagueId)
        {
            return ctx.Statistics.Where(s => s.Team.LeagueId == leagueId);
        }
    }
}
