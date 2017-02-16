using Bll.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Implementations
{
    public class RefereeRepository : Repository, IRefereeRepository
    {

        public int AddReferee(Referee referee)
        {
            ctx.Referees.Add(referee);
            ctx.SaveChanges();

            AddUpdate(new Update
            {
                name= UpdateEnum.Referee.ToUpdateString(),
                date = DateTime.Now,
                isAdding = true,
                data1 = referee.Id
            });

            return referee.Id;
        }

        public void AddRefereeToMatch(int refereeId, int matchId)
        {
            var referee = ctx.Referees.Find(refereeId);
            var match = ctx.Matches.Find(matchId);

            match.Referees.Add(referee);
            ctx.SaveChanges();

            AddUpdate(new Update
            {
                name = UpdateEnum.RefereeMatch.ToUpdateString(),
                date = DateTime.Now,
                isAdding = true,
                data1 = refereeId,
                data2 = matchId
            });
        }

        public IEnumerable<Referee> GetAllReferee()
        {
                return ctx.Referees;
        }

        public Referee GetRefereeById(int id)
        {
            return ctx.Referees.Find(id);
        }

        public Dictionary<int, List<int>> GetAllRefereeAndMatchId()
        {
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            ctx.Matches.Include("Referees").Where( m => m.Referees.Any()).ToList().ForEach(m => dict.Add(m.Id, m.Referees.Select(r => r.Id).ToList()));
            return dict;
        }

        public IEnumerable<Referee> GetRefereesByMatch(int matchId)
        {
                return ctx.Matches.Include("Referees").Where(m => m.Id == matchId).First().Referees;
        }

        public void RemoveRefereeFromMatch(int refereeId, int matchId)
        {
            var referee = ctx.Referees.Find(refereeId);
            var match = ctx.Matches.Find(matchId);

            if (!(referee.Matches.Contains(match)))
                throw new Exception("Referee cannot be removed from match!");


            match.Referees.Remove(referee);

            ctx.Matches.Attach(match);
            ctx.Entry(match).Property(e => e.Referees).IsModified = true;

            ctx.SaveChanges();

            AddUpdate(new Update
            {
                name = UpdateEnum.RefereeMatch.ToUpdateString(),
                date = DateTime.Now,
                isAdding = false,
                data1 = refereeId,
                data2 = matchId
            });
        }

        public int UpdateReferee(Referee referee)
        {
            var updated = ctx.Referees.Find(referee.Id);

            updated.Name = referee.Name;

            ctx.Referees.Attach(updated);
            ctx.Entry(updated).State = System.Data.Entity.EntityState.Modified;

            ctx.SaveChanges();

            return referee.Id;
        }
    }
}
