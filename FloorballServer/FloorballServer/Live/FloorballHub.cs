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

        public override Task OnConnected()
        {

            NameValueCollection queryString = Context.Request.QueryString as NameValueCollection;

            string[] countries = { "hu", "se", "fl", "sw", "cz"};

            foreach (var key in queryString.AllKeys)
            {

                if (countries.Contains(key) && Convert.ToBoolean(queryString.Get(key)))
                {
                        Groups.Add(Context.ConnectionId, key);
                }

            }

            return base.OnConnected();
        }

    }
}