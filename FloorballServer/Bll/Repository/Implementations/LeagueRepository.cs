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

        public int UpdateLeague(League league)
        {
            var updated = ctx.Leagues.Find(league.Id);

            updated.ClassName = league.ClassName;
            updated.Country = league.Country;
            updated.Name = league.Name;
            updated.Rounds = league.Rounds;
            updated.Sex = league.Sex;
            updated.Type = league.Type;
            updated.Year = league.Year;

            ctx.Leagues.Attach(updated);
            ctx.Entry(updated).State = System.Data.Entity.EntityState.Modified;

            ctx.SaveChanges();

            return updated.Id;
        }
    }
}
