using Bll.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Implementations
{
    public class MatchRepository : Repository , IMatchRepository
    {

        public int AddMatch(Match match)
        {
            ctx.Matches.Add(match);
            ctx.SaveChanges();

            AddUpdate( new Update
            {
                name = UpdateEnum.Match.ToUpdateString(),
                date = DateTime.Now,
                updatetype = UpdateType.Create.ToString(),
                data1 = match.Id
            });

            return match.Id;
        }

        public IEnumerable<Match> GetActualMatches()
        {
            DateTime threshold = DateTime.Now.AddDays(3);

            return ctx.Matches.Where(m => m.Date < threshold && m.Date >= DateTime.Now);
        }

        public IEnumerable<Match> GetAllMatch()
        {
            return ctx.Matches;
        }

        public Match GetMatchById(int id)
        {
            return ctx.Matches.Include("HomeTeam").Include("AwayTeam").Include("Players").Include("League").Include("HomeTeam.Players").Include("AwayTeam.Players").Where(m => m.Id == id).FirstOrDefault();
        }

        public IEnumerable<Match> GetMatchesByLeague(int id)
        {
            return ctx.Leagues.Include("Matches").Include("Matches.HomeTeam").Include("Matches.AwayTeam").Where(l => l.Id == id).First().Matches;
        }

        public IEnumerable<Match> GetMatchesByReferee(int refereeId)
        {
            return ctx.Referees.Include("Matches").Include("Matches.Players").Where(r => r.Id == refereeId).First().Matches;
        }

        public void UpdateMatch(Match match)
        {
            Match updated = ctx.Matches.Find(match.Id);

            updated.Date = match.Date;
            updated.Time = match.Time;
            updated.Round = match.Round;
            updated.StadiumId = match.StadiumId;
            updated.GoalsA = match.GoalsA;
            updated.GoalsH = match.GoalsH;
            updated.State = match.State;

            ctx.Matches.Attach(updated);
            var entry = ctx.Entry(updated);
            entry.State = System.Data.Entity.EntityState.Modified;

            ctx.SaveChanges();
        }

    }
}
