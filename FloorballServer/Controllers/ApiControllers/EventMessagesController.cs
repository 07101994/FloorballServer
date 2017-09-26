using DAL;
using DAL.Model;
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
    public class EventMessagesController : BaseApiController
    {
        public EventMessagesController(IUnitOfWork UoW) : base(UoW) { }

        /// <summary>
        /// Get all eventmessage
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/eventmessages
        [Route("eventmessages")]
        [HttpGet]
        public HttpResponseMessage Eventmessages()
        {
            List<EventMessageModel> list = new List<EventMessageModel>();

            UoW.EventMessageRepository.GetAllEventMessage().ToList().ForEach(e => list.Add(ModelHelper.CreateEventMessageModel(e)));
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        /// <summary>
        /// Get eventmessages by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/eventmessages/{id}
        [Route("eventmessages/{id}")]
        [HttpGet]
        public HttpResponseMessage EventMessages(int id)
        {
            EventMessageModel eventMessages = ModelHelper.CreateEventMessageModel(UoW.EventMessageRepository.GetEventMessageById(id));

            return Request.CreateResponse(HttpStatusCode.OK, eventMessages);
        }

        /// <summary>
        /// Get eventmessages by category
        /// </summary>
        /// <param name="categoryNumber"></param>
        /// <returns></returns>
        //GET api/floorball/events
        [Route("eventmessages")]
        [HttpGet]
        public HttpResponseMessage EventMessages(char categoryNumber)
        {
            List<EventMessageModel> eventMessages = new List<EventMessageModel>();

            UoW.EventMessageRepository.GetEventMessagesByCategory(categoryNumber).ToList().ForEach(e => eventMessages.Add(ModelHelper.CreateEventMessageModel(e)));

            return Request.CreateResponse(HttpStatusCode.OK, eventMessages);
        }

        /// <summary>
        /// Add event message
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("eventmessages")]
        [HttpPost]
        public HttpResponseMessage EventMessages(EventMessageModel model)
        {
            int id = UoW.EventMessageRepository.AddEventMessage(new EventMessage
            {
                Code = model.Code,
                Message = model.Message
            });


            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        /// <summary>
        /// Update eventmessage.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("eventmessages")]
        [HttpPut]
        public HttpResponseMessage Put(EventMessageModel model)
        {
            int id = UoW.EventMessageRepository.UpdateEventmessage(new EventMessage
            {
                Code = model.Code,
                Id = model.Id,
                Message = model.Message
            });

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }


    }
}
