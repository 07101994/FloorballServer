﻿using Bll;
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
        public HttpResponseMessage Events(EventModel e)
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
            comm.AddEventToMatch(e, UoW.EventRepository.GetCountryByEvent(id).ToCountryString());

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
            UoW.EventRepository.RemoveEvent(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
