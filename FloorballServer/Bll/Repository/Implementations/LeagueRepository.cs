using Bll.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Implementations
{
    public class LeagueRepository : Repository, ILeagueRepository
    {

        public int AddLeague(League league)
        {
            
            ctx.Leagues.Add(league);
            ctx.SaveChanges();

            AddUpdate(new Update
            {
                name = UpdateEnum.League.ToUpdateString(),
                isAdding = true,
                date = DateTime.Now,
                data1 = league.Id
            });

            return league.Id;
        }

        public IEnumerable<League> GetAllLeague()
        {
            return ctx.Leagues;
        }

        public League GetLeagueById(int id)
        {
            return ctx.Leagues.Where(l => l.Id == id).FirstOrDefault();
        }

        public IEnumerable<int> GetAllYear()
        {
            return ctx.Leagues.Select(l => l.Year.Year).Distinct().OrderBy(t => t);
        }

        public IEnumerable<League> GetLeaguesByYear(DateTime year)
        {
            return ctx.Leagues.Where(l => l.Year == year);
        }

        public int GetNumberOfRoundsInLeague(int id)
        {
            return ctx.Leagues.Find(id).Rounds;
        }

    }
}
