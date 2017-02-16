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
    public class RefereesController : BaseApiController
    {
        public RefereesController(IUnitOfWork UoW) : base(UoW) { }

        /// <summary>
        /// Get all refereees
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/referees
        [Route("referees")]
        [HttpGet]
        public HttpResponseMessage Referees()
        {
            List<RefereeModel> referees = new List<RefereeModel>();

            UoW.RefereeRepository.GetAllReferee().ToList().ForEach(r => referees.Add(ModelHelper.CreateRefereeModel(r)));
            return Request.CreateResponse(HttpStatusCode.OK, referees);
        }

        /// <summary>
        /// Get referee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/referees/{id}
        [Route("referee/{id}")]
        [HttpGet]
        public HttpResponseMessage Referees(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ModelHelper.CreateRefereeModel(UoW.RefereeRepository.GetRefereeById(id)));
        }

        /// <summary>
        /// Get matches by referee.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/referee/{id}/matches
        [Route("referees/{id}/matches")]
        [HttpGet]
        public HttpResponseMessage MatchesByReferee(int id)
        {
            List<MatchModel> matches = new List<MatchModel>();

            UoW.MatchRepository.GetMatchesByReferee(id).ToList().ForEach(m => matches.Add(ModelHelper.CreateMatchModel(m)));

            return Request.CreateResponse(HttpStatusCode.OK, matches);
        }

        //GET api/floorball/refereess/matches
        //GET api/floorball/matches/referees
        [Route("referees/matches")]
        [Route("matches/referees")]
        [HttpGet]
        public HttpResponseMessage RefereesAndMatches()
        {
            return Request.CreateResponse(HttpStatusCode.OK, UoW.RefereeRepository.GetAllRefereeAndMatchId());
        }

        /// <summary>
        /// Add referee to match
        /// </summary>
        /// <param name="refereeId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        //PUT api/floorball/players/{playerId}/matches/{matchId}
        //PUT api/floorball/matches/{matchId}/players/{playerId}
        [Route("referees/{refereeId}/matches/{matchId}")]
        [Route("matches/{matchId}/referees/{refereeId}")]
        [HttpPut]
        public HttpResponseMessage AddRefereeToMatch(int refereeId, int matchId)
        {
            UoW.RefereeRepository.AddRefereeToMatch(refereeId, matchId);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Add referee
        /// </summary>
        /// <param name="referee"></param>
        /// <returns></returns>
        //POST api/floorball/referees
        [Route("referees")]
        [HttpPost]
        public HttpResponseMessage Referees(RefereeModel referee)
        {
            int id = UoW.RefereeRepository.AddReferee(new Referee { Name = referee.Name });

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        //DELETE api/floorball/players/{playerId}/matches/{matchId}
        //DELETE api/floorball/matches/{matchId}/players/{playerId}
        [Route("referees/{refereeId}/matches/{matchId}")]
        [Route("matches/{matchId}/referees/{refereeId}")]
        [HttpDelete]
        public HttpResponseMessage RemoveRefereeFromMatch(int refereeId, int matchId)
        {
            UoW.RefereeRepository.RemoveRefereeFromMatch(refereeId, matchId);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Add referee
        /// </summary>
        /// <returns></returns>
        [Route("referees")]
        [HttpPost]
        public HttpResponseMessage Referee(RefereeModel refere)
        {
            int id = UoW.RefereeRepository.AddReferee(new Referee
            {
                Name = refere.Name,
            });

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        /// <summary>
        /// Update referee
        /// </summary>
        /// <returns></returns>
        [Route("referees")]
        [HttpPut]
        public HttpResponseMessage Put(RefereeModel refere)
        {
            int id = UoW.RefereeRepository.UpdateReferee(new Referee {
                Id = refere.Id,
                Name = refere.Name,
            });

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

    }
}
