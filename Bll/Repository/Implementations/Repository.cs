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
        public virtual FloorballEntities Ctx { get; set; }

        public IEnumerable<Update> GetUpdatesAfterDate(DateTime date)
        {
            return Ctx.Updates.Where(u => DateTime.Compare(u.date, date) > 0);
        }

        public int AddUpdate(Update update)
        {
            Ctx.Updates.Add(update);
            Ctx.SaveChanges();

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
                    Ctx.Dispose();
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
