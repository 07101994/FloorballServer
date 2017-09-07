using FloorballServer.Models.Floorball;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FloorballServer.Controllers.ApiControllers
{
    public class NotificationController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Subscribe(NotificationModel model)
        {




            return Request.CreateResponse(HttpStatusCode.OK);

        }

        [HttpPost]
        public HttpResponseMessage UnSubscribe(NotificationModel model)
        {



            return Request.CreateResponse(HttpStatusCode.OK);
        }


    }
}
