using Bll.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Implementations
{
    public class MatchRepository : IMatchRepository
    {

        [Inject]
        public FloorballEntities ctx { get; set; }

        public int AddMatch(Match match)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Match> GetActualMatches()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Match> GetAllMatch()
        {
            throw new NotImplementedException();
        }

        public Match GetMatchById(int id)
        {
            throw new NotImplementedException();
        }

        public Match GetMatchesByLeague(int leagueId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Match> GetMatchesByReferee(int refereeId)
        {
            throw new NotImplementedException();
        }

        public Match GetMatchesByYear(DateTime year)
        {
            throw new NotImplementedException();
        }

        public void UpdateMatch(int matchId, DateTime date, TimeSpan time, short round, int stadiumId, short goalsh, short goalsa, string state)
        {
            throw new NotImplementedException();
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
        // ~MatchRepository() {
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
