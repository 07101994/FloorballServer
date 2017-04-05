using Bll;
using Bll.Repository;
using FloorballServer.Attributes;
using FloorballServer.Helper;
using FloorballServer.Live;
using FloorballServer.Models.Floorball;
using MessagingService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FloorballServer.Controllers.ApiControllers
{
    [FloorballExceptionFilter]
    [RoutePrefix("api/floorball")]
    public class EventsController : BaseApiController
    {
        public EventsController(IUnitOfWork UoW) : base(UoW) { }

        /// <summary>
        /// Get all event
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/events
        [Route("events")]
        [HttpGet]
        public HttpResponseMessage Events()
        {
            List<EventModel> list = new List<EventModel>();

            UoW.EventRepository.GetAllEvent().ToList().ForEach(e => list.Add(ModelHelper.CreateEventModel(e)));
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        /// <summary>
        /// Get event by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/events/{id}
        [Route("events")]
        [HttpGet]
        public HttpResponseMessage Events(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ModelHelper.CreateEventModel(UoW.EventRepository.GetEventById(id)));
        }

        /// <summary>
        /// Add event
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        //POST api/floorball/events
        [Route("events")]
        [HttpPost]
        public async Task<HttpResponseMessage> Events(EventModel e)
        {
            int id = UoW.EventRepository.AddEvent(new Event
            {
                MatchId = e.MatchId,
                Type = e.Type,
                Time = e.Time,
                PlayerRegNum = e.PlayerId,
                EventMessageId = e.EventMessageId,
                TeamId = e.TeamId
            });

            e.Id = id;

            Communicator comm = new Communicator();
            string leagueId = UoW.LeagueRepository.GetLeagueByEvent(e.Id).Id.ToString();
            comm.AddEventToMatch(e, leagueId);

            await Messanger.Instance.SendNewEvent(
                JsonConvert.SerializeObject(e),
                NotificationHelper.GetEventTitleArgs(UoW.EventRepository.GetEventById(e.Id)),
                NotificationHelper.GetEventBodyArgs(UoW.EventRepository.GetEventById(e.Id)),
                "/topics/event_" + leagueId);
        

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        /// <summary>
        /// Update event.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        [Route("events")]
        [HttpPut]
        public HttpResponseMessage Put(EventModel e)
        {
            int id = UoW.EventRepository.UpdateEvent(new Event
            {
                Id = e.Id,
                EventMessageId = e.EventMessageId,
                MatchId = e.MatchId,
                PlayerRegNum = e.PlayerId,
                TeamId = e.TeamId,
                Time = e.Time,
                Type = e.Type
            });

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        /// <summary>
        /// Delete event
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //DELETE api/floorball/events/{id}
        [Route("events/{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteEvent(int id)
        {
            var league = UoW.LeagueRepository.GetLeagueByEvent(id);

            UoW.EventRepository.RemoveEvent(id);

            Communicator comm = new Communicator();
            comm.RemoveEventFromMatch(id, league.Id.ToString());

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
