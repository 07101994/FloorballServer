using Bll;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Web;

namespace FloorballServer.Helper
{
    public class Serializer
    {

        public static string SerializeUpdates(List<Update> updates)
        {
            string json = "";

            Dictionary<string, object> dict = new Dictionary<string, object>();

            foreach (var u in updates)
            {
                switch (u.name)
                {
                    case "addPlayer":

                        dict.Add("addPlayer", ModelHelper.CreatePlayerModel(DatabaseManager.GetPlayerById(u.data1)));

                        break;

                    case "addTeam":

                        dict.Add("addTeam", ModelHelper.CreateTeamModel(DatabaseManager.GetTeamById(u.data1)));

                        break;

                    case "addMatch":

                        dict.Add("addMatch", ModelHelper.CreateMatchModel(DatabaseManager.GetMatchById(u.data1)));


                        break;

                    case "addLeague":

                        dict.Add("addLeague", ModelHelper.CreateLeagueModel(DatabaseManager.GetLeagueById(u.data1)));

                        break;

                    case "addEvent":

                        dict.Add("addEvent", ModelHelper.CreateEventModel(DatabaseManager.GetEventById(u.data1)));

                        break;

                    case "addReferee":

                        dict.Add("addReferee", ModelHelper.CreateRefereeModel(DatabaseManager.GetRefereeById(u.data1)));

                        break;

                    case "addStadium":

                        dict.Add("addStadium", ModelHelper.CreateStadiumModel(DatabaseManager.GetStadiumById(u.data1)));

                        break;

                    case "playerToTeam":

                        dict.Add("playerToTeam", new { playerId = u.data1, teamId = u.data2 });

                        break;

                    case "playerToMatch":

                        dict.Add("playerToMatch", new { playerId = u.data1, matchId = u.data2 });

                        break;

                    case "refereeToMatch":

                        break;

                    case "removePlayerFromTeam":

                        dict.Add("removePlayerFromTeam", new { playerId = u.data1, teamId = u.data2 });

                        break;

                    case "removePlayerFromMatch":

                        dict.Add("removePlayerFromMatch", new { playerId = u.data1, matchId = u.data2 });

                        break;

                    case "removeRefereeFromMatch":

                        break;

                    //case "addStat":

                    //    dict.Add("addStat", ModelHelper.CreateStatisticsModel(DatabaseManager.GetStatisticById(u.data1)));

                    //    break;

                    //case "removeStat":

                    //    dict.Add("removeStat", new { statId = u.data1});

                    //    break;

                    default:
                        break;
                }


            }

            json = JsonConvert.SerializeObject(dict, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            });


            return json;
        }


    }
}