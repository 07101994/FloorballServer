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
            throw new NotImplementedException();
        }

        public void AddRefereeToMatch(int refereeId, int matchId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Referee> GetAllReferee()
        {
            throw new NotImplementedException();
        }

        public Referee GetrefereeById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Referee> GetRefereesByMatch(int matchId)
        {
            throw new NotImplementedException();
        }

        public void RemoveRefereeFromMatch(int refereeId, int matchId)
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
