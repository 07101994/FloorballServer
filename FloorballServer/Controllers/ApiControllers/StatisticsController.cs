using DAL.Repository;
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
    public class StatisticsController : BaseApiController
    {
        public StatisticsController(IUnitOfWork UoW) : base(UoW) { }


        /// <summary>
        /// Get all statistic
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/statistics
        [Route("statistics")]
        [HttpGet]
        public HttpResponseMessage Statistics()
        {
            List<StatisticModel> list = new List<StatisticModel>();

            UoW.StatisticRepository.GetAllStatistic().ToList().ForEach(s => list.Add(ModelHelper.CreateStatisticsModel(s)));
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }
    }
}
