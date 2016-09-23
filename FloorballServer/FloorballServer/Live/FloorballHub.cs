using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Specialized;
using FloorballServer.Models.Floorball;

namespace FloorballServer.Live
{
    public class FloorballHub : Hub
    {

        public static string[] countries = { "HU", "SE", "FL", "SW", "CZ" };

        public override Task OnConnected()
        {

            NameValueCollection queryString = Context.Request.QueryString as NameValueCollection;

            string[] clientCountries = queryString.Get("countries").Split(';');

            foreach (var country in clientCountries)
            {
                if (countries.Contains(country))
                {
                    Groups.Add(Context.ConnectionId, country);
                }
            }

            return base.OnConnected();
        }


        public override Task OnDisconnected(bool stopCalled)
        {

            NameValueCollection queryString = Context.Request.QueryString as NameValueCollection;

            string[] clientCountries = queryString.Get("countries").Split(';');

            foreach (var country in clientCountries)
            {
                if (countries.Contains(country))
                {
                    Groups.Remove(Context.ConnectionId, country);
                }
            }

            return base.OnDisconnected(stopCalled);
        }

    }
}