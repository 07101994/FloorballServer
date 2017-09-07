using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Interfaces
{
    public interface IEventRepository : IDisposable
    {

        #region READ

        Event GetEventById(int id);

        IEnumerable<Event> GetEventsByMatch(int matchId);

        IEnumerable<Event> GetAllEvent();

        CountriesEnum GetCountryByEvent(int id);

        #endregion

        #region CREATE

        int AddEvent(Event ev);

        #endregion

        #region DELETE

        void RemoveEvent(int id);

        #endregion

        #region UPDATE

        int UpdateEvent(Event e);

        #endregion

    }
}
