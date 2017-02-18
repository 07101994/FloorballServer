using Bll.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Implementations
{
    public class EventRepository : Repository, IEventRepository
    {

        public int AddEvent(Event ev)
        {

            ctx.Events.Add(ev);

            if (ev.PlayerRegNum != -1 && ev.Type != "I" && ev.Type != "B")
            {
                ChangeStatisticFromPlayer(ev.PlayerRegNum, ev.TeamId, ev.Type, ctx, "increase");
            }

            ctx.SaveChanges();

            AddUpdate(new Update
            {
                name = UpdateEnum.Event.ToUpdateString(),
                date = DateTime.Now,
                updatetype = UpdateType.Create,
                data1 = ev.Id
            });

            return ev.Id;
        }

        private void ChangeStatisticFromPlayer(int playerRegNum, int teamId, string type, FloorballEntities ctx, string direction)
        {
            Statistic stat = ctx.Statistics.Where(s => s.PlayerRegNum == playerRegNum && s.TeamId == teamId && s.Name == type).First();

            if (direction == "increase")
            {
                stat.Number++;
            }
            else
            {
                stat.Number--;
            }

            ctx.Statistics.Attach(stat);
            var entry = ctx.Entry(stat);
            entry.Property(e => e.Number).IsModified = true;
        }

        public IEnumerable<Event> GetAllEvent()
        {
            return ctx.Events;
        }

        public Event GetEventById(int id)
        {
            return ctx.Events.Include("EventMessage").Include("Player").Where(e => e.Id == id).FirstOrDefault();
        }

        public CountriesEnum GetCountryByEvent(int id)
        {
            return ctx.Events.Include("Match.League").Where(e => e.Id == id).First().Match.League.Country.ToEnum<CountriesEnum>();
        }

        public IEnumerable<Event> GetEventsByMatch(int matchId)
        {
            return ctx.Matches.Include("Events").Include("Events.Player").Include("Events.Eventmessage").Where(m => m.Id == matchId).First().Events.OrderByDescending(e => e.Time);
        }

        public void RemoveEvent(int id)
        {
            var e = ctx.Events.Include("Match.HomeTeam.Players").Include("Match.AwayTeam.Players").Where(ev => ev.Id == id).FirstOrDefault();

            if (e != null && e.Type == "G")
            {
                int t;

                if (e.Match.HomeTeam.Players.Contains(e.Player))
                {
                    t = e.Match.HomeTeamId;
                }
                else
                {
                    t = e.Match.AwayTeamId;
                }

                ChangeStatisticFromPlayer(e.PlayerRegNum, t, e.Type, ctx, "reduce");

                var e1 = ctx.Events.Include("Match.HomeTeam").Include("Match.AwayTeam").Where(ev => ev.MatchId == e.MatchId && ev.Time == e.Time && ev.Type == "A").FirstOrDefault();

                if (e1 != null && e1.Match.HomeTeam.Players.Contains(e1.Player))
                {
                    t = e1.Match.HomeTeamId;
                }
                else
                {
                    t = e1.Match.AwayTeamId;
                }

                ctx.Events.Remove(e1);

                AddUpdate(new Update
                {
                    name = UpdateEnum.Event.ToUpdateString(),
                    updatetype = UpdateType.Delete,
                    date = DateTime.Now,
                    data1 = e1.Id
                });
            }


            ctx.Events.Remove(e);

            AddUpdate(new Update
            {
                name = UpdateEnum.Event.ToUpdateString(),
                updatetype = UpdateType.Delete,
                date = DateTime.Now,
                data1 = e.Id
            });

            ctx.SaveChanges();
        }

        public int UpdateEvent(Event e)
        {
            var updated = ctx.Events.Find(e.Id);

            updated.EventMessageId = e.EventMessageId;
            updated.MatchId = e.MatchId;
            updated.PlayerRegNum = e.PlayerRegNum;
            updated.TeamId = e.TeamId;
            updated.Time = e.Time;
            updated.Type = e.Type;

            ctx.Events.Attach(updated);
            ctx.Entry(updated).State = System.Data.Entity.EntityState.Modified;

            ctx.SaveChanges();

            return updated.Id;
        }
    }
}
