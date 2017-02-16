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
        public int AddEventMessage(EventMessage eventMessage)
        {
            ctx.EventMessages.Add(eventMessage);
            ctx.SaveChanges();

            return eventMessage.Id;
        }

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

        public int UpdateEventmessage(EventMessage eventMessage)
        {
            var updated = ctx.EventMessages.Find(eventMessage.Id);

            updated.Code = eventMessage.Code;
            updated.Message = eventMessage.Message;

            ctx.EventMessages.Attach(updated);
            ctx.Entry(updated).State = System.Data.Entity.EntityState.Modified;

            ctx.SaveChanges();

            return updated.Id;
        }
    }
}
