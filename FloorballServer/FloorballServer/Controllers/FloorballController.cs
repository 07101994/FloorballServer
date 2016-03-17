using Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FloorballServer.Controllers
{
    public class FloorballController : ApiController
    {
        
        //GET api/floorball/updates
        [HttpGet]
        public HttpResponseMessage Updates()
        {

            //TODO: implement

            return Request.CreateResponse();
        }

        //GET api/floorball/years
        [HttpGet]
        public HttpResponseMessage Years()
        {

            //TODO: implement

            return Request.CreateResponse();
        }


        //GET api/floorball/leagues/{year}
        [HttpGet]
        public HttpResponseMessage Leagues()
        {

            //TODO: implement

            return Request.CreateResponse();
        }

        //GET api/floorball/matches/{id}
        [HttpGet]
        public HttpResponseMessage Matches(int id)
        {

            //TODO: implement

            return Request.CreateResponse();
        }

        //GET api/floorball/match/{id}
        [HttpGet]
        public HttpResponseMessage Match(int id)
        {

            //TODO: implement

            return Request.CreateResponse();
        }


        //GET api/floorball/team/{id}
        [HttpGet]
        public HttpResponseMessage Team(int id)
        {

            //TODO: implement

            return Request.CreateResponse();
        }

        //GET api/floorball/teams/{id}
        [HttpGet]
        public HttpResponseMessage Teams(int id)
        {

            //TODO: implement

            List<Team> teams = DatabaseManager.GetTeamsByLeagueId(id);

            return Request.CreateResponse();
        }

        //GET api/floorball/players/{id}
        [HttpGet]
        public HttpResponseMessage Players(int id)
        {

            //TODO: implement

            return Request.CreateResponse();
        }

        //GET api/floorball/player/{id}
        [HttpGet]
        public HttpResponseMessage Player(int id)
        {

            //TODO: implement

            return Request.CreateResponse();
        }

        //GET api/floorball/referee/{id}
        [HttpGet]
        public HttpResponseMessage Referee(int id)
        {

            //TODO: implement

            return Request.CreateResponse();
        }

        //GET api/floorball/actualmatches/
        [HttpGet]
        public HttpResponseMessage ActualMatches()
        {

            //TODO: implement

            return Request.CreateResponse();
        }

    }
}
