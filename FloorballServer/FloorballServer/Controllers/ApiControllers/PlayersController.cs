using Bll;
using Bll.Repository;
using FloorballServer.Attributes;
using FloorballServer.Helper;
using FloorballServer.Models.Floorball;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FloorballServer.Controllers.ApiControllers
{
    [FloorballExceptionFilter]
    [RoutePrefix("api/floorball")]
    public class PlayersController : BaseApiController
    {
        public PlayersController(IUnitOfWork UoW) : base(UoW) { }

        /// <summary>
        /// Get all players
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/players
        [Route("players")]
        [HttpGet]
        public HttpResponseMessage Players()
        {
            List<PlayerModel> players = new List<PlayerModel>();

            UoW.PlayerRepository.GetAllPlayer().ToList().ForEach(p => players.Add(ModelHelper.CreatePlayerModel(p)));

            return Request.CreateResponse(HttpStatusCode.OK, players);
        }

        /// <summary>
        /// Get player by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/players/{id}
        [Route("players/{id}")]
        [HttpGet]
        public HttpResponseMessage Players(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ModelHelper.CreatePlayerModel(UoW.PlayerRepository.GetPlayerById(id)));
        }

        //GET api/floorball/players/teams
        //GET api/floorball/teams/players
        [Route("players/teams")]
        [Route("teams/players")]
        [HttpGet]
        public HttpResponseMessage PlayersAndTeams()
        {
            return Request.CreateResponse(HttpStatusCode.OK, UoW.PlayerRepository.GetAllPlayerAndTeamId());
        }

        //GET api/floorball/players/matches
        //GET api/floorball/matches/players
        [Route("players/matches")]
        [Route("matches/players")]
        [HttpGet]
        public HttpResponseMessage PlayersAndMatches()
        {
            return Request.CreateResponse(HttpStatusCode.OK, UoW.PlayerRepository.GetAllPlayerAndMatchId());
        }

        /// <summary>
        /// Add player to team
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        //PUT api/floorball/players/{playerId}/teams/{teamId}
        //PUT api/floorball/teams/{teamID}/players/{playerId}
        [Route("players/{playerId}/teams/{teamId}")]
        [Route("teams/{teamId}/players/{playerId}")]
        [HttpPut]
        public HttpResponseMessage AddPlayerToTeam(int playerId, int teamId)
        {
            UoW.PlayerRepository.AddPlayerToTeam(playerId, teamId);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Add player to match
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        //PUT api/floorball/players/{playerId}/matches/{matchId}
        //PUT api/floorball/matches/{matchId}/players/{playerId}
        [Route("players/{playerId}/matches/{matchId}")]
        [Route("matches/{matchId}/players/{playerId}")]
        [HttpPut]
        public HttpResponseMessage AddPlayerToMatch(int playerId, int matchId)
        {
            UoW.PlayerRepository.AddPlayerToMatch(playerId, matchId);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Add player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        //POST api/floorball/players
        [Route("players")]
        [HttpPost]
        public HttpResponseMessage Players(PlayerModel player)
        {
            int id = UoW.PlayerRepository.AddPlayer(new Player
            {
                FirstName = player.FirstName,
                SecondName = player.SecondName,
                RegNum = player.RegNum,
                Date = player.BirthDate
            });

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        /// <summary>
        /// Remove player from team
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        //DELETE api/floorball/players/{playerId}/teams/{teamId}
        //DELETE api/floorball/teams/{teamId}/players/{playerId}
        [Route("players/{playerId}/teams/{teamId}")]
        [Route("teams/{teamId}/players/{playerId}")]
        [HttpDelete]
        public HttpResponseMessage RemovePlayerFromTeam(int playerId, int teamId)
        {
            UoW.PlayerRepository.RemovePlayerFromTeam(playerId, teamId);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Remove player from match
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        //DELETE api/floorball/players/{playerId}/matches/{matchId}
        //DELETE api/floorball/matches/{matchId}/players/{playerId}
        [Route("players/{playerId}/matches/{matchId}")]
        [Route("matches/{matchId}/players/{playerId}")]
        [HttpDelete]
        public HttpResponseMessage RemovePlayerFromMatch(int playerId, int matchId)
        {
            UoW.PlayerRepository.RemovePlayerFromMatch(playerId, matchId);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
