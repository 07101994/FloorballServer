using Bll;
using FloorballServer.Helper;
using FloorballServer.Models.Floorball;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace FloorballServer.Controllers
{
    public class FloorballController : ApiController
    {

        #region GET

        //GET api/floorball/updates
        [HttpGet]
        public HttpResponseMessage Updates()
        {

            //TODO: implement

            return Request.CreateResponse();
        }

        /// <summary>
        /// Get all years
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/years
        [HttpGet]
        public HttpResponseMessage Years()
        {

            List<DateTime> years = DatabaseManager.GetAllYear();
            
            return Request.CreateResponse(HttpStatusCode.OK,years);
        }

        /// <summary>
        /// Get leagues by year.
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/leagues/{year}
        [HttpGet]
        public HttpResponseMessage Leagues()
        {
            List<LeagueModel> list = new List<LeagueModel>();

            foreach (var l in DatabaseManager.GetAllLeague())
            {
                LeagueModel model = ModelHelper.CreateLeagueModel(l);
                list.Add(model);
            }

            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        /// <summary>
        /// Get matches by league.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/leagues/{id}/matches
        [Route("api/floorball/leagues/{id}/matches")]
        [HttpGet]
        public HttpResponseMessage MatchesByLeague(int id)
        {

            List<MatchModel> matches = new List<MatchModel>();

            foreach (var m in DatabaseManager.GetMatchesByLeague(id))
            {
                matches.Add(ModelHelper.CreateMatchModel(m));
            }


            return Request.CreateResponse(HttpStatusCode.OK, matches);
        }

        /// <summary>
        /// Get matches by referee.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/referee/{id}/matches
        [Route("api/floorball/referee/{id}/matches")]
        [HttpGet]
        public HttpResponseMessage MatchesByReferee(int id)
        {

            List<MatchModel> matches = new List<MatchModel>();

            foreach (var m in DatabaseManager.GetMatchesByReferee(id))
            {
                matches.Add(ModelHelper.CreateMatchModel(m));
            }


            return Request.CreateResponse(HttpStatusCode.OK, matches);
        }


        /// <summary>
        /// Get events by category
        /// </summary>
        /// <param name="categoryNumber"></param>
        /// <returns></returns>
        //GET api/floorball/events
        [HttpGet]
        public HttpResponseMessage EventMessages(char categoryNumber)
        {

            List<EventMessageModel> eventMessages = new List<EventMessageModel>();

            foreach (var e in DatabaseManager.GetEventMessagesByCategory(categoryNumber))
            {
                eventMessages.Add(ModelHelper.CreateEventMessageModel(e));
            }


            return Request.CreateResponse(HttpStatusCode.OK, eventMessages);
        }

        /// <summary>
        /// Get events by category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/eventmessages/{id}
        [HttpGet]
        public HttpResponseMessage EventMessages(int id)
        {

            EventMessageModel eventMessages = ModelHelper.CreateEventMessageModel(DatabaseManager.GetEventMessageById(id));

            return Request.CreateResponse(HttpStatusCode.OK, eventMessages);
        }

        /// <summary>
        /// Get rounds by league
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/league/{id}/rounds
        [Route("api/floorball/leagues/{id}/rounds")]
        [HttpGet]
        public HttpResponseMessage Rounds(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, DatabaseManager.GetNumberOfRoundsInLeague(id));
        }

        /// <summary>
        /// Get match.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/matches/{id}
        [HttpGet]
        public HttpResponseMessage Matches(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ModelHelper.CreateMatchModel(DatabaseManager.GetMatchById(id)));
        }

        /// <summary>
        /// Get team by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/teams/{id}
        [HttpGet]
        public HttpResponseMessage Teams(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK,ModelHelper.CreateTeamModel(DatabaseManager.GetTeamById(id)));
        }

        /// <summary>
        /// Get teams by league
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/teams/{id}
        [Route("api/floorball/leagues/{id}/teams")]
        [HttpGet]
        public HttpResponseMessage TeamsByLeague(int id)
        {

            List<TeamModel> teams = new List<TeamModel>();

            foreach (var t in DatabaseManager.GetTeamsByLeague(id))
            {
                teams.Add(ModelHelper.CreateTeamModel(t));
            }


            return Request.CreateResponse(HttpStatusCode.OK,teams);
        }

        /// <summary>
        /// Get teams by year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        //GET api/floorball/teams
        [HttpGet]
        public HttpResponseMessage Teams(string year)
        {

            List<TeamModel> teams = new List<TeamModel>();

            foreach (var t in DatabaseManager.GetTeamsByYear(DateTime.ParseExact(year,"yyyy", CultureInfo.InvariantCulture)))
            {
                teams.Add(ModelHelper.CreateTeamModel(t));
            }

            return Request.CreateResponse(HttpStatusCode.OK, teams);
        }

        /// <summary>
        /// Get all players
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/players
        [HttpGet]
        public HttpResponseMessage Players()
        {
            List<PlayerModel> players = new List<PlayerModel>();

            foreach (var p in DatabaseManager.GetAllPlayer())
            {
                players.Add(ModelHelper.CreatePlayerModel(p));
            }

            return Request.CreateResponse(HttpStatusCode.OK,players);
        }

        /// <summary>
        /// Get players by team
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/teams/{id}/players
        [Route("api/floorball/teams/{id}/players")]
        [HttpGet]
        public HttpResponseMessage PlayersByTeam(int id)
        {

            List<PlayerModel> players = new List<PlayerModel>();

            foreach (var p in DatabaseManager.GetPlayersByTeam(id))
            {
                players.Add(ModelHelper.CreatePlayerModel(p));
            }

            return Request.CreateResponse(HttpStatusCode.OK,players);
        }

        /// <summary>
        /// Get players by match
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/matches/{id}/players
        [Route("api/floorball/matches/{id}/players")]
        [HttpGet]
        public HttpResponseMessage PlayersByMatch(int id)
        {

            List<PlayerModel> players = new List<PlayerModel>();

            foreach (var p in DatabaseManager.GetPlayersByMatch(id))
            {
                players.Add(ModelHelper.CreatePlayerModel(p));
            }

            return Request.CreateResponse(HttpStatusCode.OK, players);
        }

        /// <summary>
        /// Get player by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/players/{id}
        [HttpGet]
        public HttpResponseMessage Players(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ModelHelper.CreatePlayerModel(DatabaseManager.GetPlayerById(id)));
        }

        /// <summary>
        /// Get all refereees
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/referees
        [HttpGet]
        public HttpResponseMessage Referees()
        {

            List<RefereeModel> referees = new List<RefereeModel>();
            foreach (var r in DatabaseManager.GetAllReferee())
            {
                referees.Add(ModelHelper.CreateRefereeModel(r));
            }

            return Request.CreateResponse(HttpStatusCode.OK,referees);
        }

        /// <summary>
        /// Get all stadiums
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/stadiums
        [HttpGet]
        public HttpResponseMessage Stadiums()
        {

            List<StadiumModel> referees = new List<StadiumModel>();
            foreach (var s in DatabaseManager.GetAllStadium())
            {
                referees.Add(ModelHelper.CreateStadiumModel(s));
            }

            return Request.CreateResponse(HttpStatusCode.OK, referees);
        }


        /// <summary>
        /// Get referee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/referees/{id}
        [HttpGet]
        public HttpResponseMessage Referees(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ModelHelper.CreateRefereeModel(DatabaseManager.GetRefereeById(id)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/actualmatches/
        [HttpGet]
        public HttpResponseMessage ActualMatches()
        {

            //TODO: implement

            return Request.CreateResponse();
        }

        /// <summary>
        /// Get league by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/leagues/{id}
        [HttpGet]
        public HttpResponseMessage Leagues(int id)
        {

            return Request.CreateResponse(HttpStatusCode.OK,ModelHelper.CreateLeagueModel(DatabaseManager.GetLeagueById(id)));
        }

        /// <summary>
        /// Get events by match
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/matches/{id}/events
        [Route("api/floorball/matches/{id}/events")]
        [HttpGet]
        public HttpResponseMessage EventsByMatch(int id)
        {

            List<EventModel> events = new List<EventModel>();
            foreach (var e in DatabaseManager.GetEventsByMatch(id))
            {
                events.Add(ModelHelper.CreateEventModel(e));
            }

            return Request.CreateResponse(HttpStatusCode.OK, events);
        }

        /// <summary>
        /// Get event by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/events/{id}
        [HttpGet]
        public HttpResponseMessage Events(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ModelHelper.CreateEventModel(DatabaseManager.GetEventById(id)));
        }

        #endregion

        #region PUT

        /// <summary>
        /// Add player to team
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        //PUT api/floorball/players/{playerId}/teams/{teamId}
        //PUT api/floorball/teams/{teamID}/players/{playerId}
        [Route("api/floorball/players/{playerId}/teams/{teamId}")]
        [Route("api/floorball/teams/{teamId}/players/{playerId}")]
        [HttpPut]
        public HttpResponseMessage AddPlayerToTeam(int playerId, int teamId)
        {
            DatabaseManager.AddPlayerToTeam(playerId, teamId);

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
        [Route("api/floorball/players/{playerId}/matches/{matchId}")]
        [Route("api/floorball/matches/{matchId}/players/{playerId}")]
        [HttpPut]
        public HttpResponseMessage AddPlayerToMatch(int playerId, int matchId)
        {
            DatabaseManager.AddPlayerToMatch(playerId, matchId);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion

        #region POST

        /// <summary>
        /// Add league
        /// </summary>
        /// <param name="league"></param>
        /// <returns></returns>
        //POST api/floorball/leagues
        [HttpPost]
        public HttpResponseMessage Leagues(LeagueModel league)
        {
            int id = DatabaseManager.AddLeague(league.Name, league.Year, league.type, league.ClassName, league.Rounds);

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        /// <summary>
        /// Add stadium
        /// </summary>
        /// <param name="stadium"></param>
        /// <returns></returns>
        //POST api/floorball/stadiums
        [HttpPost]
        public HttpResponseMessage Stadiums(StadiumModel stadium)
        {
            int id = DatabaseManager.AddStadium(stadium.Name, stadium.Address);

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        /// <summary>
        /// Add team.
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        //POST api/floorball/teams
        [HttpPost]
        public HttpResponseMessage Teams(TeamModel team)
        {
            int id = DatabaseManager.AddTeam(team.Name, team.Year, team.Coach, team.StadiumId, team.LeagueId);

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        /// <summary>
        /// Add referee
        /// </summary>
        /// <param name="referee"></param>
        /// <returns></returns>
        //POST api/floorball/referees
        [HttpPost]
        public HttpResponseMessage Referees(RefereeModel referee)
        {
            int id = DatabaseManager.AddReferee(referee.Name);

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        /// <summary>
        /// Add player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        //POST api/floorball/players
        [HttpPost]
        public HttpResponseMessage Players(PlayerModel player)
        {
            int id = DatabaseManager.AddPlayer(player.Name,player.RegNum,player.Number,player.BirthDate);

            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        /// <summary>
        /// Add event
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        //POST api/floorball/events
        [HttpPost]
        public HttpResponseMessage Events(EventModel e)
        {
            //HttpContent requestContent = Request.Content;
            //string jsonContent = requestContent.ReadAsStringAsync().Result;
            //EventModel model = JsonConvert.DeserializeObject<EventModel>(jsonContent);

            int id = DatabaseManager.AddEvent(e.MatchId, e.Type, TimeSpan.ParseExact(e.Time, "h\\h\\:m\\m\\:s\\s", CultureInfo.InvariantCulture), e.PlayerId, e.EventMessageId, e.TeamId);

            return Request.CreateResponse(HttpStatusCode.OK,id);
        }




        #endregion

        #region DELETE

        /// <summary>
        /// Remove player from team
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        //DELETE api/floorball/players/{playerId}/teams/{teamId}
        //DELETE api/floorball/teams/{teamId}/players/{playerId}
        [Route("api/floorball/players/{playerId}/teams/{teamId}")]
        [Route("api/floorball/teams/{teamId}/players/{playerId}")]
        [HttpDelete]
        public HttpResponseMessage RemovePlayerFromTeam(int playerId, int teamId)
        {
            DatabaseManager.RemovePlayerFromTeam(playerId, teamId);

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
        [Route("api/floorball/players/{playerId}/matches/{matchId}")]
        [Route("api/floorball/matches/{matchId}/players/{playerId}")]
        [HttpDelete]
        public HttpResponseMessage RemovePlayerFromMatch(int playerId, int matchId)
        {
            DatabaseManager.RemovePlayerFromMatch(playerId, matchId);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        
        /// <summary>
        /// Delete event
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //DELETE api/floorball/events/{id}
        [Route("api/floorball/events/{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteEvent(int id)
        {
            DatabaseManager.RemoveEvent(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        #endregion
    }
}
