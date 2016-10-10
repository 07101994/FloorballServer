using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Interfaces
{
    public interface IEventMessageRepository
    {

        #region READ

        IEnumerable<EventMessage> GetAllEventMessage();

        EventMessage GetEventMessageById(int id);

        IEnumerable<EventMessage> GetEventMessagesByCategory(char startNumber);

        #endregion
    }
}
