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
            Ctx.EventMessages.Add(eventMessage);
            Ctx.SaveChanges();

            return eventMessage.Id;
        }

        public IEnumerable<EventMessage> GetAllEventMessage()
        {
            return Ctx.EventMessages;
        }

        public EventMessage GetEventMessageById(int id)
        {
            return Ctx.EventMessages.Find(id);
        }

        public IEnumerable<EventMessage> GetEventMessagesByCategory(char categoryStartNumber)
        {
            return Ctx.EventMessages.Where(e => e.Code.ToString()[0] == categoryStartNumber);
        }

        public int UpdateEventmessage(EventMessage eventMessage)
        {
            var updated = Ctx.EventMessages.Find(eventMessage.Id);

            updated.Code = eventMessage.Code;
            updated.Message = eventMessage.Message;

            Ctx.EventMessages.Attach(updated);
            Ctx.Entry(updated).State = System.Data.Entity.EntityState.Modified;

            Ctx.SaveChanges();

            return updated.Id;
        }
    }
}
