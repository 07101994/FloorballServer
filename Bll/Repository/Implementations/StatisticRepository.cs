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
            return Ctx.Statistics;
        }

        public IEnumerable<Statistic> GetStatisticsByLeague(int leagueId)
        {
            return Ctx.Statistics.Where(s => s.Team.LeagueId == leagueId);
        }
    }
}
