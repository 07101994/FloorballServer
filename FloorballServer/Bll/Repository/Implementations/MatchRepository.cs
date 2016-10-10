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
                isAdding = true,
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

        public void UpdateMatch(int matchId, DateTime date, TimeSpan time, short round, int stadiumId, short goalsh, short goalsa, string state)
        {
            Match match = ctx.Matches.Where(m => m.Id == matchId).First();

            match.Date = date;
            match.Time = time;
            match.Round = round;
            match.StadiumId = stadiumId;
            match.GoalsA = goalsa;
            match.GoalsH = goalsh;
            match.State = state;

            ctx.Matches.Attach(match);
            var entry = ctx.Entry(match);
            entry.State = System.Data.Entity.EntityState.Modified;

            ctx.SaveChanges();
        }

    }
}
