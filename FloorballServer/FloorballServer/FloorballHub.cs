using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace FloorballServer
{
    public class FloorballHub : Hub
    {
        public void UpdateNick(string param)
        {
            Clients.All.UpdateChatMessage(param);
         
            
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

    }
}