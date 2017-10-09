using DAL.Model;
using DAL.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Implementations
{
    public class EventRepository : FlorballRepository, IEventRepository
    {

        public int AddEvent(Event ev)
        {
            Ctx.Events.Add(ev);

            if (ev.PlayerId != -1 && ev.Type != EventType.T && ev.Type != EventType.S)
            {
                ChangeStatisticFromPlayer(ev.PlayerId, ev.TeamId, (StatType)Enum.Parse(typeof(EventType), ev.Type.ToString()), Ctx, "increase");
            }

            if (ev.Type == EventType.G)
            {
                ChangeMatchGoals(ev.MatchId, ev.TeamId, ev.Time, "up");
            }

            Ctx.SaveChanges();

            AddUpdate(new Update
            {
                Name = UpdateEnum.Event,
                Date = DateTime.Now,
                Updatetype = UpdateType.Create,
                Data1 = ev.Id
            });

            return ev.Id;
        }

        private void ChangeMatchGoals(int matchId, int teamId, TimeSpan time, string direction)
        {
            var match = Ctx.Matches.Find(matchId);

            if (match == null)
            {
                throw new Exception("Match not found");
            }

            match.Time = match.Time < time ? time : match.Time;

            if (teamId == match.HomeTeamId)
            {
                match.ScoreH = direction == "up" ? (short)(match.ScoreH + 1) : (short)(match.ScoreH - 1);
                Ctx.Matches.Attach(match);
                var entry = Ctx.Entry(match);
                entry.Property(e => e.ScoreH).IsModified = true;
                entry.Property(e => e.Time).IsModified = true;
            }
            else
            {
                match.ScoreA = direction == "up" ? (short)(match.ScoreA + 1) : (short)(match.ScoreA - 1);
                Ctx.Matches.Attach(match);
                var entry = Ctx.Entry(match);
                entry.Property(e => e.ScoreA).IsModified = true;
                entry.Property(e => e.Time).IsModified = true;
            }

        }

        private void ChangeStatisticFromPlayer(int playerId, int teamId, StatType type, FloorballBaseCtx ctx, string direction)
        {
            Statistic stat = ctx.Statistics.FirstOrDefault(s => s.PlayerId == playerId && s.TeamId == teamId && s.Type == type);

            if (stat == null)
            {
                throw new Exception("Stat not found for player and team");
            }

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
            return Ctx.Events;
        }

        public Event GetEventById(int id)
        {
            return Ctx.Events.Include("EventMessage").Include("Player").Include("Match").Where(e => e.Id == id).FirstOrDefault();
        }

        public CountriesEnum GetCountryByEvent(int id)
        {
            return Ctx.Events.Include("Match.League").Where(e => e.Id == id).First().Match.League.Country;
        }

        public IEnumerable<Event> GetEventsByMatch(int matchId)
        {
            return Ctx.Matches.Include("Events").Include("Events.Player").Include("Events.Eventmessage").Where(m => m.Id == matchId).First().Events.OrderByDescending(e => e.Time);
        }

        public void RemoveEvent(int id)
        {
            var e = Ctx.Events.Include("Match.HomeTeam.Players").Include("Match.AwayTeam.Players").FirstOrDefault(ev => ev.Id == id);

            if (e != null)
            {
                int teamId;

                if (e.Match.HomeTeam.Players.Contains(e.Player))
                {
                    teamId = e.Match.HomeTeamId;
                }
                else
                {
                    teamId = e.Match.AwayTeamId;
                }

                ChangeStatisticFromPlayer(e.PlayerId, teamId, (StatType)Enum.Parse(typeof(EventType), e.Type.ToString()), Ctx, "reduce");

                if (e.Type == EventType.G)
                {
                    ChangeMatchGoals(e.MatchId, e.TeamId, e.Time, "down");
                }

                Ctx.Events.Remove(e);

                AddUpdate(new Update
                {
                    Name = UpdateEnum.Event,
                    Updatetype = UpdateType.Delete,
                    Date = DateTime.Now,
                    Data1 = e.Id
                });

                Ctx.SaveChanges();
            }
        }

        public int UpdateEvent(Event e)
        {
            var updated = Ctx.Events.Find(e.Id);

            updated.EventMessageId = e.EventMessageId;
            updated.MatchId = e.MatchId;
            updated.PlayerId = e.PlayerId;
            updated.TeamId = e.TeamId;
            updated.Time = e.Time;
            updated.Type = e.Type;

            Ctx.Events.Attach(updated);
            Ctx.Entry(updated).State = System.Data.Entity.EntityState.Modified;

            Ctx.SaveChanges();

            return updated.Id;
        }
    }
}
