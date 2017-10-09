using DAL.Model;
using DAL.Repository.Interfaces;
using DAL.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Implementations
{
    public class MatchRepository : FlorballRepository , IMatchRepository
    {

        public int AddMatch(Match match)
        {
            Ctx.Matches.Add(match);
            Ctx.SaveChanges();

            AddUpdate( new Update
            {
                Name = UpdateEnum.Match,
                Date = DateTime.Now,
                Updatetype = UpdateType.Create,
                Data1 = match.Id
            });

            return match.Id;
        }

        public IEnumerable<Match> GetActualMatches()
        {
            DateTime threshold = DateTime.Now.AddDays(3);

            return Ctx.Matches.Where(m => m.Date < threshold && m.Date >= DateTime.Now);
        }

        public IEnumerable<Match> GetAllMatch()
        {
            return Ctx.Matches;
        }

        public Match GetMatchById(int id)
        {
            return Ctx.Matches.Include("HomeTeam").Include("AwayTeam").Include("Players").Include("League").Include("HomeTeam.Players").Include("AwayTeam.Players").Where(m => m.Id == id).FirstOrDefault();
        }

        public IEnumerable<Match> GetMatchesByLeague(int id)
        {
            return Ctx.Leagues.Include("Matches").Include("Matches.HomeTeam").Include("Matches.AwayTeam").Where(l => l.Id == id).First().Matches;
        }

        public IEnumerable<Match> GetMatchesByReferee(int refereeId)
        {
            return Ctx.Referees.Include("Matches").Include("Matches.Players").Where(r => r.Id == refereeId).First().Matches;
        }

        public void UpdateMatch(Match match)
        {
            Match updated = Ctx.Matches.Find(match.Id);

            updated.Date = match.Date;
            updated.Time = match.Time;
            updated.Round = match.Round;
            updated.StadiumId = match.StadiumId;
            updated.ScoreA = match.ScoreA;
            updated.ScoreH = match.ScoreH;
            updated.State = match.State;

            Ctx.Matches.Attach(updated);
            var entry = Ctx.Entry(updated);
            entry.State = System.Data.Entity.EntityState.Modified;

            Ctx.SaveChanges();
        }

    }
}
