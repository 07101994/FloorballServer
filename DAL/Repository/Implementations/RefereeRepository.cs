using DAL.Model;
using DAL.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Implementations
{
    public class RefereeRepository : FlorballRepository, IRefereeRepository
    {

        public int AddReferee(Referee referee)
        {
            Ctx.Referees.Add(referee);
            Ctx.SaveChanges();

            AddUpdate(new Update
            {
                name= UpdateEnum.Referee.ToUpdateString(),
                date = DateTime.Now,
                updatetype = UpdateType.Create.ToString(),
                data1 = referee.Id
            });

            return referee.Id;
        }

        public void AddRefereeToMatch(int refereeId, int matchId)
        {
            var referee = Ctx.Referees.Find(refereeId);
            var match = Ctx.Matches.Find(matchId);

            match.Referees.Add(referee);
            Ctx.SaveChanges();

            AddUpdate(new Update
            {
                name = UpdateEnum.RefereeMatch.ToUpdateString(),
                date = DateTime.Now,
                updatetype = UpdateType.Create.ToString(),
                data1 = refereeId,
                data2 = matchId
            });
        }

        public IEnumerable<Referee> GetAllReferee()
        {
                return Ctx.Referees;
        }

        public Referee GetRefereeById(int id)
        {
            return Ctx.Referees.Find(id);
        }

        public Dictionary<int, List<int>> GetAllRefereeAndMatchId()
        {
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            Ctx.Matches.Include("Referees").Where( m => m.Referees.Any()).ToList().ForEach(m => dict.Add(m.Id, m.Referees.Select(r => r.Id).ToList()));
            return dict;
        }

        public IEnumerable<Referee> GetRefereesByMatch(int matchId)
        {
                return Ctx.Matches.Include("Referees").Where(m => m.Id == matchId).First().Referees;
        }

        public void RemoveRefereeFromMatch(int refereeId, int matchId)
        {
            var referee = Ctx.Referees.Find(refereeId);
            var match = Ctx.Matches.Find(matchId);

            if (!(referee.Matches.Contains(match)))
                throw new Exception("Referee cannot be removed from match!");


            match.Referees.Remove(referee);

            Ctx.Matches.Attach(match);
            Ctx.Entry(match).Property(e => e.Referees).IsModified = true;

            Ctx.SaveChanges();

            AddUpdate(new Update
            {
                name = UpdateEnum.RefereeMatch.ToUpdateString(),
                date = DateTime.Now,
                updatetype = UpdateType.Delete.ToString(),
                data1 = refereeId,
                data2 = matchId
            });
        }

        public int UpdateReferee(Referee referee)
        {
            var updated = Ctx.Referees.Find(referee.Id);

            updated.Name = referee.Name;

            Ctx.Referees.Attach(updated);
            Ctx.Entry(updated).State = System.Data.Entity.EntityState.Modified;

            Ctx.SaveChanges();

            return referee.Id;
        }
    }
}
