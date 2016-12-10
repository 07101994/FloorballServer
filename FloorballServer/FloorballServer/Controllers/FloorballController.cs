using Bll;
using Bll.Repository;
using Bll.UpdateFolder;
using FloorballServer.Helper;
using FloorballServer.Live;
using FloorballServer.Models.Floorball;
using Microsoft.AspNet.SignalR;
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

        private IUnitOfWork UoW;

        public FloorballController(IUnitOfWork UoW)
        {
            this.UoW = UoW;
        }
        


        #region GET

        //GET api/floorball/updates?date={date}
        //[Route("api/floorball/updates")]
        [HttpGet]
        public HttpResponseMessage Updates(DateTime date)
        {
            try
            {
                List<Update> updates = UoW.Repository.GetUpdatesAfterDate(date).ToList();

                Serializer serializer = new Serializer(new UnitOfWork(null));
                string json = serializer.SerializeUpdates(updates);

                return Request.CreateResponse(HttpStatusCode.OK, json);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get all years
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/years
        [HttpGet]
        public HttpResponseMessage Years()
        {
            try
            {
                List<int> years = UoW.LeagueRepository.GetAllYear().ToList();

                return Request.CreateResponse(HttpStatusCode.OK, years);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get all leagues.
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/leagues/
        [HttpGet]
        public HttpResponseMessage Leagues()
        {
            try
            {
                List<LeagueModel> list = new List<LeagueModel>();

                UoW.LeagueRepository.GetAllLeague().ToList().ForEach(l => list.Add(ModelHelper.CreateLeagueModel(l)));

                return Request.CreateResponse(HttpStatusCode.OK, list);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);


            }
        }

        /// <summary>
        /// Get all leagues by year.
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/leagues/
        [HttpGet]
        public HttpResponseMessage LeaguesByYear(string year)
        {
            try
            {
                List<LeagueModel> list = new List<LeagueModel>();

                DateTime d = DateTime.ParseExact(year, "yyyy", CultureInfo.InvariantCulture);
                UoW.LeagueRepository.GetLeaguesByYear(d).ToList().ForEach(l => list.Add(ModelHelper.CreateLeagueModel(l)));

                return Request.CreateResponse(HttpStatusCode.OK, list);

            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
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

            try
            {
                List<MatchModel> matches = new List<MatchModel>();

                UoW.MatchRepository.GetMatchesByLeague(id).ToList().ForEach(m => matches.Add(ModelHelper.CreateMatchModel(m)));

                return Request.CreateResponse(HttpStatusCode.OK, matches);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

                
            }
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
            try
            {

                List<MatchModel> matches = new List<MatchModel>();

                UoW.MatchRepository.GetMatchesByReferee(id).ToList().ForEach(m => matches.Add(ModelHelper.CreateMatchModel(m)));

                return Request.CreateResponse(HttpStatusCode.OK, matches);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

                
            }
        }

        /// <summary>
        /// Get statistics by league.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/referee/{id}/matches
        [Route("api/floorball/leagues/{id}/statistics")]
        [HttpGet]
        public HttpResponseMessage StatisticsByLeague(int id)
        {

            try
            {
                List<StatisticModel> statistics = new List<StatisticModel>();

                UoW.StatisticRepository.GetStatisticsByLeague(id).ToList().ForEach(s => statistics.Add(ModelHelper.CreateStatisticsModel(s)));
                return Request.CreateResponse(HttpStatusCode.OK, statistics);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

                
            }
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

            try
            {
                List<EventMessageModel> eventMessages = new List<EventMessageModel>();

                UoW.EventMessageRepository.GetEventMessagesByCategory(categoryNumber).ToList().ForEach(e => eventMessages.Add(ModelHelper.CreateEventMessageModel(e)));

                return Request.CreateResponse(HttpStatusCode.OK, eventMessages);

            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

               
            }
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

            try
            {
                EventMessageModel eventMessages = ModelHelper.CreateEventMessageModel(UoW.EventMessageRepository.GetEventMessageById(id));

                return Request.CreateResponse(HttpStatusCode.OK, eventMessages);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
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
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, UoW.LeagueRepository.GetNumberOfRoundsInLeague(id));
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

                
            }
        }

        /// <summary>
        /// Get all match.
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/matches/
        [Route("api/floorball/matches")]
        [HttpGet]
        public HttpResponseMessage Matches()
        {
            try
            {
                List<MatchModel> list = new List<MatchModel>();

                UoW.MatchRepository.GetAllMatch().ToList().ForEach(m => list.Add(ModelHelper.CreateMatchModel(m)));

                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

               
            }
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
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ModelHelper.CreateMatchModel(UoW.MatchRepository.GetMatchById(id)));
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

                
            }
        }

        /// <summary>
        /// Get all teams
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/teams/{id}
        [HttpGet]
        public HttpResponseMessage Teams()
        {
            try
            {
                List<TeamModel> teams = new List<TeamModel>();

                UoW.TeamRepository.GetAllTeam().ToList().ForEach(t => teams.Add(ModelHelper.CreateTeamModel(t)));
                return Request.CreateResponse(HttpStatusCode.OK, teams);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

                
            }
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
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ModelHelper.CreateTeamModel(UoW.TeamRepository.GetTeamById(id)));
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
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

            try
            {
                List<TeamModel> teams = new List<TeamModel>();

                UoW.TeamRepository.GetTeamsByLeague(id).ToList().ForEach(t => teams.Add(ModelHelper.CreateTeamModel(t)));

                return Request.CreateResponse(HttpStatusCode.OK, teams);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

                
            }
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

            try
            {
                List<TeamModel> teams = new List<TeamModel>();

                DateTime d = DateTime.ParseExact(year, "yyyy", CultureInfo.InvariantCulture);
                UoW.TeamRepository.GetTeamsByYear(d).ToList().ForEach(t => teams.Add(ModelHelper.CreateTeamModel(t)));

                return Request.CreateResponse(HttpStatusCode.OK, teams);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
        }

        /// <summary>
        /// Get all players
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/players
        [HttpGet]
        public HttpResponseMessage Players()
        {
            try
            {
                List<PlayerModel> players = new List<PlayerModel>();

                UoW.PlayerRepository.GetAllPlayer().ToList().ForEach(p => players.Add(ModelHelper.CreatePlayerModel(p)));

                return Request.CreateResponse(HttpStatusCode.OK, players);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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

            try
            {
                List<PlayerModel> players = new List<PlayerModel>();

                UoW.PlayerRepository.GetPlayersByTeam(id).ToList().ForEach(p => players.Add(ModelHelper.CreatePlayerModel(p)));

                return Request.CreateResponse(HttpStatusCode.OK, players);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
        }

        /// <summary>
        /// Get players by league
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET api/floorball/leagues/{id}/players
        [Route("api/floorball/leagues/{id}/players")]
        [HttpGet]
        public HttpResponseMessage PlayersByLeague(int id)
        {

            try
            {
                List<PlayerModel> players = new List<PlayerModel>();

                UoW.PlayerRepository.GetPlayersByLeague(id).ToList().ForEach(p => players.Add(ModelHelper.CreatePlayerModel(p)));
                return Request.CreateResponse(HttpStatusCode.OK, players);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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

            try
            {
                List<PlayerModel> players = new List<PlayerModel>();

                UoW.PlayerRepository.GetPlayersByMatch(id).ToList().ForEach(p => players.Add(ModelHelper.CreatePlayerModel(p)));
                return Request.CreateResponse(HttpStatusCode.OK, players);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ModelHelper.CreatePlayerModel(UoW.PlayerRepository.GetPlayerById(id)));
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
        }

        /// <summary>
        /// Get all refereees
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/referees
        [HttpGet]
        public HttpResponseMessage Referees()
        {
            try
            {
                List<RefereeModel> referees = new List<RefereeModel>();

                UoW.RefereeRepository.GetAllReferee().ToList().ForEach(r => referees.Add(ModelHelper.CreateRefereeModel(r)));
                return Request.CreateResponse(HttpStatusCode.OK, referees);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
        }

        /// <summary>
        /// Get all stadiums
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/stadiums
        [HttpGet]
        public HttpResponseMessage Stadiums()
        {
            try
            {
                List<StadiumModel> stadiums = new List<StadiumModel>();

                UoW.StadiumRepository.GetAllStadium().ToList().ForEach(s => stadiums.Add(ModelHelper.CreateStadiumModel(s)));
                return Request.CreateResponse(HttpStatusCode.OK, stadiums);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ModelHelper.CreateRefereeModel(UoW.RefereeRepository.GetRefereeById(id)));
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/actual/
        [Route("api/floorball/matches/actual")]
        [HttpGet]
        public HttpResponseMessage ActualMatches()
        {

            try
            {
                List<MatchModel> matches = new List<MatchModel>();

                UoW.MatchRepository.GetActualMatches().ToList().ForEach(m => matches.Add(ModelHelper.CreateMatchModel(m)));
                return Request.CreateResponse(HttpStatusCode.OK, matches);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ModelHelper.CreateLeagueModel(UoW.LeagueRepository.GetLeagueById(id)));
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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

            try
            {
                List<EventModel> events = new List<EventModel>();

                UoW.EventRepository.GetEventsByMatch(id).ToList().ForEach(e => events.Add(ModelHelper.CreateEventModel(e)));
                return Request.CreateResponse(HttpStatusCode.OK, events);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
        }

        /// <summary>
        /// Get all event
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/events
        [HttpGet]
        public HttpResponseMessage Events()
        {
            try
            {
                List<EventModel> list = new List<EventModel>();

                UoW.EventRepository.GetAllEvent().ToList().ForEach(e => list.Add(ModelHelper.CreateEventModel(e)));
                return Request.CreateResponse(HttpStatusCode.OK, list);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
        }


        /// <summary>
        /// Get all eventmessage
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/eventmessages
        [HttpGet]
        public HttpResponseMessage Eventmessages()
        {
            try
            {
                List<EventMessageModel> list = new List<EventMessageModel>();

                UoW.EventMessageRepository.GetAllEventMessage().ToList().ForEach(e => list.Add(ModelHelper.CreateEventMessageModel(e)));
                return Request.CreateResponse(HttpStatusCode.OK, list);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
        }

        /// <summary>
        /// Get all statistic
        /// </summary>
        /// <returns></returns>
        //GET api/floorball/statistics
        [HttpGet]
        public HttpResponseMessage Statistics()
        {
            try
            {
                List<StatisticModel> list = new List<StatisticModel>();

                UoW.StatisticRepository.GetAllStatistic().ToList().ForEach(s => list.Add(ModelHelper.CreateStatisticsModel(s)));
                return Request.CreateResponse(HttpStatusCode.OK, list);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
        }


        //GET api/floorball/players/teams
        //GET api/floorball/teams/players
        [Route("api/floorball/players/teams")]
        [Route("api/floorball/teams/players")]
        [HttpGet]
        public HttpResponseMessage PlayersAndTeams()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, UoW.PlayerRepository.GetAllPlayerAndTeamId());
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
        }

        //GET api/floorball/players/matches
        //GET api/floorball/matches/players
        [Route("api/floorball/players/matches")]
        [Route("api/floorball/matches/players")]
        [HttpGet]
        public HttpResponseMessage PlayersAndMatches()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, UoW.PlayerRepository.GetAllPlayerAndMatchId());
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
        }

        //GET api/floorball/refereess/matches
        //GET api/floorball/matches/referees
        [Route("api/floorball/referees/matches")]
        [Route("api/floorball/matches/referees")]
        [HttpGet]
        public HttpResponseMessage RefereesAndMatches()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, UoW.RefereeRepository.GetAllRefereeAndMatchId());
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ModelHelper.CreateEventModel(UoW.EventRepository.GetEventById(id)));
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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
            try
            {
                UoW.PlayerRepository.AddPlayerToTeam(playerId, teamId);

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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
            try
            {
                UoW.PlayerRepository.AddPlayerToMatch(playerId, matchId);

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
        }

        /// <summary>
        /// Add referee to match
        /// </summary>
        /// <param name="refereeId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        //PUT api/floorball/players/{playerId}/matches/{matchId}
        //PUT api/floorball/matches/{matchId}/players/{playerId}
        [Route("api/floorball/referees/{refereeId}/matches/{matchId}")]
        [Route("api/floorball/matches/{matchId}/referees/{refereeId}")]
        [HttpPut]
        public HttpResponseMessage AddRefereeToMatch(int refereeId, int matchId)
        {
            try
            {
                UoW.RefereeRepository.AddRefereeToMatch(refereeId, matchId);

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
        }

        /// <summary>
        /// Update match
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        //PUT api/floorball/matches
        [Route("api/floorball/matches")]
        [HttpPut]
        public HttpResponseMessage UpdateMatch(MatchModel match)
        {
            try
            {
                Match oldMatch = UoW.MatchRepository.GetMatchById(match.Id);

                UoW.MatchRepository.UpdateMatch(match.Id, match.Date, match.Time, match.Round, match.StadiumId, match.GoalsH, match.GoalsA, match.State);

                if (match.Time != oldMatch.Time)
                {
                    Communicator comm = new Communicator();
                    comm.UpdateMatchTime(match.Id, match.Time, oldMatch.League.Country);
                }

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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
            try
            {
                int id = UoW.LeagueRepository.AddLeague(new League
                {
                    Name = league.Name,
                    Year = league.Year,
                    Type = league.type,
                    ClassName = league.ClassName,
                    Rounds = league.Rounds,
                    Country = league.Country.ToCountryString()
                });

                return Request.CreateResponse(HttpStatusCode.OK, id);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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
            try
            {
                int id = UoW.StadiumRepository.AddStadium(new Stadium
                {
                    Name = stadium.Name,
                    Address = stadium.Address
                });

                return Request.CreateResponse(HttpStatusCode.OK, id);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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
            try
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
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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
            try
            {
                int id = UoW.RefereeRepository.AddReferee(new Referee { Name = referee.Name });

                return Request.CreateResponse(HttpStatusCode.OK, id);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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
            try
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
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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

            try
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

                Communicator comm = new Communicator();
                comm.AddEventToMatch(e, UoW.EventRepository.GetCountryByEvent(id).ToCountryString());

                return Request.CreateResponse(HttpStatusCode.OK, id);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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
            try
            {
                UoW.PlayerRepository.RemovePlayerFromTeam(playerId, teamId);

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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
            try
            {
                UoW.PlayerRepository.RemovePlayerFromMatch(playerId, matchId);

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
        }


        //DELETE api/floorball/players/{playerId}/matches/{matchId}
        //DELETE api/floorball/matches/{matchId}/players/{playerId}
        [Route("api/floorball/referees/{refereeId}/matches/{matchId}")]
        [Route("api/floorball/matches/{matchId}/referees/{refereeId}")]
        [HttpDelete]
        public HttpResponseMessage RemoveRefereeFromMatch(int refereeId, int matchId)
        {
            try
            {
                UoW.RefereeRepository.RemoveRefereeFromMatch(refereeId, matchId);

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
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
            try
            {
                UoW.EventRepository.RemoveEvent(id);

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception)
            {
                
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        
    }
}
