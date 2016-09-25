using Bll;
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


        public void AddEventToMatch(EventModel e, string country)
        {

            hub.Clients.Group(country).AddEventToMatch(e);

        }


        public void StartMatch(int matchId, StateEnum state, string country)
        {

            hub.Clients.Group(country).ChangeMatchState(matchId,state);

        }

        public void UpdateMatchTime(int matchId, TimeSpan time, string country)
        {
            hub.Clients.Group(country).UpdateMatchTime(matchId, time);
        }

    }
}