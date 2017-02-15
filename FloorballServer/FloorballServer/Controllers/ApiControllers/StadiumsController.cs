using Bll;
using Bll.Repository;
using FloorballServer.Attributes;
using FloorballServer.Helper;
using FloorballServer.Models.Floorball;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FloorballServer.Controllers.ApiControllers
{
    [FloorballExceptionFilter]
    [RoutePrefix("api/floorball")]
    public class StadiumsController : BaseApiController
    {
        public StadiumsController(IUnitOfWork UoW) : base(UoW) { }

        /// <summary>
        /// Get all stadiums
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/stadiums
        [Route("stadiums")]
        [HttpGet]
        public HttpResponseMessage Stadiums()
        {
            List<StadiumModel> stadiums = new List<StadiumModel>();

            UoW.StadiumRepository.GetAllStadium().ToList().ForEach(s => stadiums.Add(ModelHelper.CreateStadiumModel(s)));
            return Request.CreateResponse(HttpStatusCode.OK, stadiums);
        }

        /// <summary>
        /// Add stadium
        /// </summary>
        /// <param name="stadium"></param>
        /// <returns></returns>
        //POST api/floorball/stadiums
        [Route("stadiums")]
        [HttpPost]
        public HttpResponseMessage Stadiums(StadiumModel stadium)
        {
            int id = UoW.StadiumRepository.AddStadium(new Stadium
            {
                Name = stadium.Name,
                Address = stadium.Address
            });

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }
    }
}
