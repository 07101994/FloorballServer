using Bll.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Implementations
{
    public class RefereeRepository : IRefereeRepository
    {

        [Inject]
        public FloorballEntities ctx { get; set; }

        public int AddReferee(Referee referee)
        {
            ctx.Referees.Add(referee);
            ctx.SaveChanges();

            AddUpdate(ctx, UpdateEnum.Referee.ToUpdateString(), DateTime.Now, true, referee.Id);

            return referee.Id;
        }

        public void AddRefereeToMatch(int refereeId, int matchId)
        {
            var referee = ctx.Referees.Find(refereeId);
            var match = ctx.Matches.Find(matchId);

            match.Referees.Add(referee);
            ctx.SaveChanges();

            AddUpdate(ctx, UpdateEnum.RefereeMatch.ToUpdateString(), DateTime.Now, true, refereeId, matchId);
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
            ctx.Matches.Include("Referees").ToList().ForEach(m => dict.Add(m.Id, m.Referees.Select(r => r.Id).ToList()));
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

            AddUpdate(ctx, UpdateEnum.RefereeMatch.ToUpdateString(), DateTime.Now, false, refereeId, matchId);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RefereeRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

       
        #endregion
    }
}
