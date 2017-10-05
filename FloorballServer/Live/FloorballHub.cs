using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using FloorballServer.Models.Floorball;
using Newtonsoft.Json;
using DAL;

namespace FloorballServer.Live
{
    public class FloorballHub : Hub
    {

        public override Task OnConnected()
        {
            var clientLeagues = JsonConvert.DeserializeObject<SortedSet<string>>(Context.Request.QueryString.Get("leagues"));

            foreach (var league in clientLeagues)
            {
                Groups.Add(Context.ConnectionId, league);
            }

            return base.OnConnected();
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            var clientLeagues = JsonConvert.DeserializeObject<SortedSet<string>>(Context.Request.QueryString.Get("leagues"));

            foreach (var league in clientLeagues)
            {
                Groups.Remove(Context.ConnectionId, league);
            }

            return base.OnDisconnected(stopCalled);
        }

    }
}