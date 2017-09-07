using Bll;
using Bll.Repository;
using FloorballServer.Attributes;
using FloorballServer.Helper;
using FloorballServer.Live;
using FloorballServer.Models.Floorball;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
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
            DateTime updateTime = DateTime.Now;

            IEnumerable<Update> updates = UoW.Repository.GetUpdatesAfterDate(date);

            UpdateCollector collector = new UpdateCollector(new UnitOfWork(null));

            UpdateModel updateModel = collector.CollectUpdates(updates, updateTime);

            return Request.CreateResponse(HttpStatusCode.OK, collector.CollectUpdates(updates, updateTime));
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
            IEnumerable<int> years = UoW.LeagueRepository.GetAllYear();
            
            return Request.CreateResponse(HttpStatusCode.OK, years);
        }

        


        /// <summary>
        /// Use this action for testing.
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/test
        [Route("test")]
        [HttpGet]
        public HttpResponseMessage Test()
        {
            byte[] image = File.ReadAllBytes(HttpContext.Current.Server.MapPa‌​th("~/Content/NokianKRP.png"));

            ImageManager.SaveImage(image,"test1.png");

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
