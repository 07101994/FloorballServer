using Bll.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Implementations
{
    public class EventMessageRepository : Repository, IEventMessageRepository
    {

        public IEnumerable<EventMessage> GetAllEventMessage()
        {
            return ctx.EventMessages;
        }

        public EventMessage GetEventMessageById(int id)
        {
            return ctx.EventMessages.Find(id);
        }

        public IEnumerable<EventMessage> GetEventMessagesByCategory(char categoryStartNumber)
        {
            return ctx.EventMessages.Where(e => e.Code.ToString()[0] == categoryStartNumber);
        }
    }
}
