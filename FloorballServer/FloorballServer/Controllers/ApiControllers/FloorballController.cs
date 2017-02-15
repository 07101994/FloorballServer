using Bll;
using Bll.Repository;
using Bll.UpdateFolder;
using FloorballServer.Attributes;
using FloorballServer.Helper;
using FloorballServer.Live;
using FloorballServer.Models.Floorball;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace FloorballServer.Controllers.ApiControllers
{
    [FloorballExceptionFilter]
    [RoutePrefix("api/floorball")]
    public class FloorballController : BaseApiController
    {

        public FloorballController(IUnitOfWork UoW) : base(UoW) {}

        //GET api/floorball/updates?date={date}
        [Route("updates")]
        [HttpGet]
        public HttpResponseMessage Updates(DateTime date)
        {
            List<Update> updates = UoW.Repository.GetUpdatesAfterDate(date).ToList();

            Serializer serializer = new Serializer(new UnitOfWork(null));
            string json = serializer.SerializeUpdates(updates);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        /// <summary>
        /// Get all years
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/years
        [Route("years")]
        [HttpGet]
        public HttpResponseMessage Years()
        {
            List<int> years = UoW.LeagueRepository.GetAllYear().ToList();

            return Request.CreateResponse(HttpStatusCode.OK, years);
        }
    }
}
