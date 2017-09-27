using DAL;
using DAL.Model;
using DAL.Repository;
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
    [Authorize(Roles = "Admin")]
    [FloorballExceptionFilter]
    [RoutePrefix("api/floorball")]
    public class LeaguesController : BaseApiController
    {

        public LeaguesController(IUnitOfWork UoW) : base(UoW) { }

        /// <summary>
        /// Get all leagues.
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/leagues/
        [Route("leagues")]
        [HttpGet]
        public HttpResponseMessage Leagues()
        {
            List<LeagueModel> list = new List<LeagueModel>();

            UoW.LeagueRepository.GetAllLeague().ToList().ForEach(l => list.Add(ModelHelper.CreateLeagueModel(l)));

            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        /// <summary>
        /// Get league by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/leagues/{id}
        [Route("leagues/{id}")]
        [HttpGet]
        public HttpResponseMessage Leagues(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ModelHelper.CreateLeagueModel(UoW.LeagueRepository.GetLeagueById(id)));
        }

        /// <summary>
        /// Get all leagues by year.
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/leagues/
        [Route("leagues")]
        [HttpGet]
        public HttpResponseMessage LeaguesByYear(string year)
        {
            List<LeagueModel> list = new List<LeagueModel>();

            DateTime d = DateTime.ParseExact(year, "yyyy", CultureInfo.InvariantCulture);
            UoW.LeagueRepository.GetLeaguesByYear(d).ToList().ForEach(l => list.Add(ModelHelper.CreateLeagueModel(l)));

            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        /// <summary>
        /// Get matches by league.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/leagues/{id}/matches
        [Route("leagues/{id}/matches")]
        [HttpGet]
        public HttpResponseMessage MatchesByLeague(int id)
        {
            List<MatchModel> matches = new List<MatchModel>();

            UoW.MatchRepository.GetMatchesByLeague(id).ToList().ForEach(m => matches.Add(ModelHelper.CreateMatchModel(m)));

            return Request.CreateResponse(HttpStatusCode.OK, matches);
        }

        /// <summary>
        /// Get statistics by league.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/referee/{id}/matches
        [Route("leagues/{id}/statistics")]
        [HttpGet]
        public HttpResponseMessage StatisticsByLeague(int id)
        {
            List<StatisticModel> statistics = new List<StatisticModel>();

            UoW.StatisticRepository.GetStatisticsByLeague(id).ToList().ForEach(s => statistics.Add(ModelHelper.CreateStatisticsModel(s)));
            return Request.CreateResponse(HttpStatusCode.OK, statistics);
        }

        /// <summary>
        /// Get rounds by league
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/league/{id}/rounds
        [Route("leagues/{id}/rounds")]
        [HttpGet]
        public HttpResponseMessage Rounds(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, UoW.LeagueRepository.GetNumberOfRoundsInLeague(id));
        }

        /// <summary>
        /// Get teams by league
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/teams/{id}
        [Route("leagues/{id}/teams")]
        [HttpGet]
        public HttpResponseMessage TeamsByLeague(int id, bool withImage = false)
        {
            List<TeamModel> teams = new List<TeamModel>();

            UoW.TeamRepository.GetTeamsByLeague(id).ToList().ForEach(t => teams.Add(ModelHelper.CreateTeamModel(t, withImage)));

            return Request.CreateResponse(HttpStatusCode.OK, teams);
        }

        /// <summary>
        /// Get players by league
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/leagues/{id}/players
        [Route("leagues/{id}/players")]
        [HttpGet]
        public HttpResponseMessage PlayersByLeague(int id)
        {
            List<PlayerModel> players = new List<PlayerModel>();

            UoW.PlayerRepository.GetPlayersByLeague(id).ToList().ForEach(p => players.Add(ModelHelper.CreatePlayerModel(p)));
            return Request.CreateResponse(HttpStatusCode.OK, players);
        }

        /// <summary>
        /// Add league
        /// </summary>
        /// <param name="league"></param>
        /// <returns></returns>
        //POST api/floorball/leagues
        [Route("leagues")]
        [HttpPost]
        public HttpResponseMessage Leagues(LeagueModel league)
        {
            int id = UoW.LeagueRepository.AddLeague(new League
            {
                Name = league.Name,
                Year = league.Year,
                Type = league.type,
                ClassName = league.ClassName,
                Rounds = league.Rounds,
                Country = league.Country.ToCountryString(),
                Sex = league.Sex
            });

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }
        
        /// <summary>
        /// Update league.
        /// </summary>
        /// <param name="league"></param>
        /// <returns></returns>
        [Route("leagues")]
        [HttpPut]
        public HttpResponseMessage Put(LeagueModel league)
        {
            int id = UoW.LeagueRepository.UpdateLeague(new League {
                ClassName = league.ClassName,
                Country = league.Country.ToCountryString(),
                Id = league.Id,
                Name = league.Name,
                Rounds = league.Rounds,
                Sex = league.Sex,
                Type = league.type,
                Year = league.Year 

            });

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

    }
}
