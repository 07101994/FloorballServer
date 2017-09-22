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
            
            Ctx.Leagues.Add(league);
            Ctx.SaveChanges();

            AddUpdate(new Update
            {
                name = UpdateEnum.League.ToUpdateString(),
                updatetype = UpdateType.Create.ToString(),
                date = DateTime.Now,
                data1 = league.Id
            });

            return league.Id;
        }

        public IEnumerable<League> GetAllLeague()
        {
            return Ctx.Leagues;
        }

        public League GetLeagueById(int id)
        {
            return Ctx.Leagues.Where(l => l.Id == id).FirstOrDefault();
        }

        public League GetLeagueByEvent(int eventId)
        {
            return Ctx.Events.Include("Match.League").First(e => e.Id == eventId).Match.League;
        }

        public IEnumerable<int> GetAllYear()
        {
            return Ctx.Leagues.Select(l => l.Year.Year).Distinct().OrderBy(t => t);
        }

        public IEnumerable<League> GetLeaguesByYear(DateTime year)
        {
            return Ctx.Leagues.Where(l => l.Year == year);
        }

        public int GetNumberOfRoundsInLeague(int id)
        {
            return Ctx.Leagues.Find(id).Rounds;
        }

        public int UpdateLeague(League league)
        {
            var updated = Ctx.Leagues.Find(league.Id);

            updated.ClassName = league.ClassName;
            updated.Country = league.Country;
            updated.Name = league.Name;
            updated.Rounds = league.Rounds;
            updated.Sex = league.Sex;
            updated.Type = league.Type;
            updated.Year = league.Year;

            Ctx.Leagues.Attach(updated);
            Ctx.Entry(updated).State = System.Data.Entity.EntityState.Modified;

            Ctx.SaveChanges();

            return updated.Id;
        }
       
    }
}
