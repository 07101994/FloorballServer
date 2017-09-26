using DAL;
using FloorballServer.Models.Floorball;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloorballServer.Live
{
    public class Communicator
    {

        private IHubContext hub;

        public Communicator()
        {
            hub = GlobalHost.ConnectionManager.GetHubContext<FloorballHub>();
        }


        public void AddEventToMatch(EventModel e, string leagueId)
        {

            hub.Clients.Group(leagueId).AddEventToMatch(e);

        }

        public void StartMatch(int matchId, StateEnum state, string leagueId)
        {
            hub.Clients.Group(leagueId).ChangeMatchState(matchId,state);
        }

        public void UpdateMatchTime(int matchId, TimeSpan time, string leagueId)
        {
            hub.Clients.Group(leagueId).UpdateMatchTime(matchId, time);
        }

        public void RemoveEventFromMatch(int eventId, string leagueId)
        {
            hub.Clients.Group(leagueId).RemoveEventFromMatch(eventId);
        }

    }
}