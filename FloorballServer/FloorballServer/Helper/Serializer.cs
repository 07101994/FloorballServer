using Bll;
using Bll.UpdateFolder;
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

            List<UpdateData> updateDataList = new List<UpdateData>();

            foreach (var u in updates)
            {
                switch (u.name.ToEnum<UpdateType>())
                {
                    case UpdateType.League:
                        updateDataList.Add(GetLeague(u));
                        break;
                    case UpdateType.Team:
                        updateDataList.Add(GetTeam(u));
                        break;
                    case UpdateType.Match:
                        updateDataList.Add(GetMatch(u));
                        break;
                    case UpdateType.Player:
                        updateDataList.Add(GetPlayer(u));
                        break;
                    case UpdateType.Stadium:
                        updateDataList.Add(GetStadium(u));
                        break;
                    case UpdateType.Referee:
                        updateDataList.Add(GetReferee(u));
                        break;
                    case UpdateType.Event:
                        updateDataList.Add(GetEvent(u));
                        break;
                    case UpdateType.EventMessage:
                        updateDataList.Add(GetEventMessage(u));
                        break;
                    case UpdateType.PlayerToTeam:
                        updateDataList.Add(GetPlayerToTeam(u));
                        break;
                    case UpdateType.PlayerToMatch:
                        updateDataList.Add(GetPlayerToMatch(u));
                        break;
                    case UpdateType.RefereeToMatch:
                        updateDataList.Add(GetRefereeToMatch(u));
                        break;
                    default:
                        break;
                }

            }

            json = JsonConvert.SerializeObject(updateDataList, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            });


            return json;
        }

        private static UpdateData GetRefereeToMatch(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateType.RefereeToMatch,
                    Entity = new { refereeId = u.data1, matchId = u.data2 },
                    isAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateType.RefereeToMatch,
                    Entity = new { refereeId = u.data1, matchId = u.data2 },
                    isAdding = false
                };
            }
        }

        private static UpdateData GetPlayerToMatch(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateType.PlayerToMatch,
                    Entity = new { playerId = u.data1, matchId = u.data2 },
                    isAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateType.PlayerToMatch,
                    Entity = new { playerId = u.data1, matchId = u.data2 },
                    isAdding = false
                };
            }
        }

        private static UpdateData GetPlayerToTeam(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateType.PlayerToTeam,
                    Entity = new { playerId = u.data1, teamId = u.data2 },
                    isAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateType.PlayerToTeam,
                    Entity = new { playerId = u.data1, teamId = u.data2 },
                    isAdding = false
                };
            }
        }

        private static UpdateData GetEventMessage(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateType.EventMessage,
                    Entity = ModelHelper.CreateEventMessageModel(DatabaseManager.GetEventMessageById(u.data1)),
                    isAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateType.EventMessage,
                    Entity = new { id = u.data1 },
                    isAdding = false
                };
            }
        }

        private static UpdateData GetEvent(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateType.Event,
                    Entity = ModelHelper.CreateEventModel(DatabaseManager.GetEventById(u.data1)),
                    isAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateType.Event,
                    Entity = new { id = u.data1 },
                    isAdding = false
                };
            }
        }

        private static UpdateData GetReferee(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateType.Referee,
                    Entity = ModelHelper.CreateRefereeModel(DatabaseManager.GetRefereeById(u.data1)),
                    isAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateType.Referee,
                    Entity = new { id = u.data1 },
                    isAdding = false
                };
            }
        }

        private static UpdateData GetStadium(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateType.Stadium,
                    Entity = ModelHelper.CreateStadiumModel(DatabaseManager.GetStadiumById(u.data1)),
                    isAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateType.Stadium,
                    Entity = new { id = u.data1 },
                    isAdding = false
                };
            }
        }

        private static UpdateData GetPlayer(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateType.Player,
                    Entity = ModelHelper.CreatePlayerModel(DatabaseManager.GetPlayerById(u.data1)),
                    isAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateType.Player,
                    Entity = new { id = u.data1 },
                    isAdding = false
                };
            }
        }

        private static UpdateData GetMatch(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateType.Match,
                    Entity = ModelHelper.CreateMatchModel(DatabaseManager.GetMatchById(u.data1)),
                    isAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateType.Match,
                    Entity = new { id = u.data1 },
                    isAdding = false
                };
            }
        }

        private static UpdateData GetTeam(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateType.Team,
                    Entity = ModelHelper.CreateTeamModel(DatabaseManager.GetTeamById(u.data1)),
                    isAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateType.Team,
                    Entity = new { id = u.data1 },
                    isAdding = false
                };
            }
        }

        private static UpdateData GetLeague(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateType.League,
                    Entity = ModelHelper.CreateLeagueModel(DatabaseManager.GetLeagueById(u.data1)),
                    isAdding = true
                }; 
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateType.League,
                    Entity = new { id = u.data1},
                    isAdding = false
                };
            }
        } 

    }
}