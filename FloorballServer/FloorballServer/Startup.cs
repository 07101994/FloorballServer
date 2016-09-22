using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using FloorballServer.Live;

[assembly: OwinStartup(typeof(FloorballServer.Startup))]

namespace FloorballServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            GlobalHost.HubPipeline.AddModule(new ErrorHandlingPipelineModule());
            app.MapSignalR();
            //var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            

        }
    }
}
