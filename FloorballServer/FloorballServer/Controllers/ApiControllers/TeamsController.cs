using Bll;
using Bll.Repository;
using FloorballServer.Attributes;
using FloorballServer.Helper;
using FloorballServer.Models.Floorball;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FloorballServer.Controllers.ApiControllers
{
    [FloorballExceptionFilter]
    [RoutePrefix("api/floorball")]
    public class TeamsController : BaseApiController
    {
        public TeamsController(IUnitOfWork UoW) : base(UoW) { }

        /// <summary>
        /// Get all teams
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/teams
        [Route("teams")]
        [HttpGet]
        public HttpResponseMessage Teams()
        {
            List<TeamModel> teams = new List<TeamModel>();

            UoW.TeamRepository.GetAllTeam().ToList().ForEach(t => teams.Add(ModelHelper.CreateTeamModel(t)));
            return Request.CreateResponse(HttpStatusCode.OK, teams);
        }

        /// <summary>
        /// Get team by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/teams/{id}
        [Route("teams/{id}")]
        [HttpGet]
        public HttpResponseMessage Teams(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ModelHelper.CreateTeamModel(UoW.TeamRepository.GetTeamById(id)));
        }

        /// <summary>
        /// Get teams by year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        //GET api/floorball/teams
        [Route("teams")]
        [HttpGet]
        public HttpResponseMessage Teams(string year)
        {
            List<TeamModel> teams = new List<TeamModel>();

            DateTime d = DateTime.ParseExact(year, "yyyy", CultureInfo.InvariantCulture);
            UoW.TeamRepository.GetTeamsByYear(d).ToList().ForEach(t => teams.Add(ModelHelper.CreateTeamModel(t)));

            return Request.CreateResponse(HttpStatusCode.OK, teams);
        }

        /// <summary>
        /// Get players by team
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/teams/{id}/players
        [Route("teams/{id}/players")]
        [HttpGet]
        public HttpResponseMessage PlayersByTeam(int id)
        {
            List<PlayerModel> players = new List<PlayerModel>();

            UoW.PlayerRepository.GetPlayersByTeam(id).ToList().ForEach(p => players.Add(ModelHelper.CreatePlayerModel(p)));

            return Request.CreateResponse(HttpStatusCode.OK, players);
        }

        /// <summary>
        /// Add team.
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        //POST api/floorball/teams
        [Route("teams")]
        [HttpPost]
        public HttpResponseMessage Teams(TeamModel team)
        {
            int id = UoW.TeamRepository.AddTeam(new Team
            {
                Name = team.Name,
                Sex = team.Sex,
                Year = team.Year,
                Coach = team.Coach,
                StadiumId = team.StadiumId,
                LeagueId = team.LeagueId,
                Country = team.Country.ToCountryString()
            });

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

    }
}
