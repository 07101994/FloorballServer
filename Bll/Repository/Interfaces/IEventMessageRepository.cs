using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IEventMessageRepository : IRepository
    {

        #region READ

        IEnumerable<EventMessage> GetAllEventMessage();

        EventMessage GetEventMessageById(int id);

        IEnumerable<EventMessage> GetEventMessagesByCategory(char startNumber);

        #endregion

        #region CREATE

        int AddEventMessage(EventMessage eventMessage);

        #endregion

        #region UPDATE

        int UpdateEventmessage(EventMessage eventMessage);

        #endregion

    }
}
