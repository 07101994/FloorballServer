using Bll;
using Bll.Repository;
using FloorballServer.Attributes;
using FloorballServer.Helper;
using FloorballServer.Live;
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
    public class MatchesController : BaseApiController
    {

        public MatchesController(IUnitOfWork UoW) : base(UoW) {}

        /// <summary>
        /// Get all match.
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/matches/
        [Route("matches")]
        [HttpGet]
        public HttpResponseMessage Matches()
        {
            List<MatchModel> list = new List<MatchModel>();

            UoW.MatchRepository.GetAllMatch().ToList().ForEach(m => list.Add(ModelHelper.CreateMatchModel(m)));

            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        /// <summary>
        /// Get match.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/matches/{id}
        [Route("matches/{id}")]
        [HttpGet]
        public HttpResponseMessage Matches(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ModelHelper.CreateMatchModel(UoW.MatchRepository.GetMatchById(id)));
        }

        /// <summary>
        /// Get players by match
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/matches/{id}/players
        [Route("matches/{id}/players")]
        [HttpGet]
        public HttpResponseMessage PlayersByMatch(int id)
        {
            List<PlayerModel> players = new List<PlayerModel>();

            UoW.PlayerRepository.GetPlayersByMatch(id).ToList().ForEach(p => players.Add(ModelHelper.CreatePlayerModel(p)));
            return Request.CreateResponse(HttpStatusCode.OK, players);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/actual/
        [Route("matches/actual")]
        [HttpGet]
        public HttpResponseMessage ActualMatches()
        {
            List<MatchModel> matches = new List<MatchModel>();

            UoW.MatchRepository.GetActualMatches().ToList().ForEach(m => matches.Add(ModelHelper.CreateMatchModel(m)));
            return Request.CreateResponse(HttpStatusCode.OK, matches);
        }

        /// <summary>
        /// Get events by match
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/matches/{id}/events
        [Route("matches/{id}/events")]
        [HttpGet]
        public HttpResponseMessage EventsByMatch(int id)
        {
            List<EventModel> events = new List<EventModel>();

            UoW.EventRepository.GetEventsByMatch(id).ToList().ForEach(e => events.Add(ModelHelper.CreateEventModel(e)));
            return Request.CreateResponse(HttpStatusCode.OK, events);
        }

        /// <summary>
        /// Update match
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        //PUT api/floorball/matches
        [Route("matches")]
        [HttpPut]
        public HttpResponseMessage UpdateMatch(MatchModel match)
        {
            Match oldMatch = UoW.MatchRepository.GetMatchById(match.Id);
            var oldTime = oldMatch.Time;

            UoW.MatchRepository.UpdateMatch(match.Id, match.Date, match.Time, match.Round, match.StadiumId, match.GoalsH, match.GoalsA, match.State);

            if (match.Time != oldTime)
            {
                Communicator comm = new Communicator();
                comm.UpdateMatchTime(match.Id, match.Time, oldMatch.League.Country);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
