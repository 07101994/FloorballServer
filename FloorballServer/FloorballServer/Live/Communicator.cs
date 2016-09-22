﻿using FloorballServer.Models.Floorball;
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


        public void StartMatch(int matchId, string country)
        {

            hub.Clients.Group(country).StartMatch(matchId);

        }

        public void EndMatch(int matchId, string country)
        {

            hub.Clients.Group(country).EndMatch(matchId);

        }

    }
}