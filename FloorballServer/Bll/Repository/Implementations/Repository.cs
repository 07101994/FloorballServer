using Bll.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Implementations
{
    public class Repository : IRepository
    {

        [Inject]
        public FloorballEntities ctx { get; set; }

        public IEnumerable<Update> GetUpdatesAfterDate(DateTime date)
        {
            return ctx.Updates.Where(u => DateTime.Compare(u.date, date) > 0);
        }

        public int AddUpdate(Update update)
        {
            ctx.Updates.Add(update);
            ctx.SaveChanges();

            return update.id;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
        #endregion
    }
}
