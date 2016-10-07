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
                switch (u.name.ToEnum<UpdateEnum>())
                {
                    case UpdateEnum.League:
                        updateDataList.Add(GetLeague(u));
                        break;
                    case UpdateEnum.Team:
                        updateDataList.Add(GetTeam(u));
                        break;
                    case UpdateEnum.Match:
                        updateDataList.Add(GetMatch(u));
                        break;
                    case UpdateEnum.Player:
                        updateDataList.Add(GetPlayer(u));
                        break;
                    case UpdateEnum.Stadium:
                        updateDataList.Add(GetStadium(u));
                        break;
                    case UpdateEnum.Referee:
                        updateDataList.Add(GetReferee(u));
                        break;
                    case UpdateEnum.Event:
                        updateDataList.Add(GetEvent(u));
                        break;
                    case UpdateEnum.EventMessage:
                        updateDataList.Add(GetEventMessage(u));
                        break;
                    case UpdateEnum.PlayerTeam:
                        updateDataList.Add(GetPlayerToTeam(u));
                        break;
                    case UpdateEnum.PlayerMatch:
                        updateDataList.Add(GetPlayerToMatch(u));
                        break;
                    case UpdateEnum.RefereeMatch:
                        updateDataList.Add(GetRefereeToMatch(u));
                        break;
                    default:
                        break;
                }

            }

            //json = JsonConvert.SerializeObject(updateDataList, Formatting.Indented, new JsonSerializerSettings
            //{
            //    TypeNameHandling = TypeNameHandling.All,
            //    TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            //});

            json = JsonConvert.SerializeObject(updateDataList, Formatting.Indented);

            return json;
            //return json;
        }

        private static UpdateData GetRefereeToMatch(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.RefereeMatch,
                    Entity = new { refereeId = u.data1, matchId = u.data2 },
                    IsAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateEnum.RefereeMatch,
                    Entity = new { refereeId = u.data1, matchId = u.data2 },
                    IsAdding = false
                };
            }
        }

        private static UpdateData GetPlayerToMatch(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.PlayerMatch,
                    Entity = new { playerId = u.data1, matchId = u.data2 },
                    IsAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateEnum.PlayerMatch,
                    Entity = new { playerId = u.data1, matchId = u.data2 },
                    IsAdding = false
                };
            }
        }

        private static UpdateData GetPlayerToTeam(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.PlayerTeam,
                    Entity = new { playerId = u.data1, teamId = u.data2 },
                    IsAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateEnum.PlayerTeam,
                    Entity = new { playerId = u.data1, teamId = u.data2 },
                    IsAdding = false
                };
            }
        }

        private static UpdateData GetEventMessage(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.EventMessage,
                    Entity = ModelHelper.CreateEventMessageModel(DatabaseManager.GetEventMessageById(u.data1)),
                    IsAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateEnum.EventMessage,
                    Entity = new { id = u.data1 },
                    IsAdding = false
                };
            }
        }

        private static UpdateData GetEvent(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Event,
                    Entity = ModelHelper.CreateEventModel(DatabaseManager.GetEventById(u.data1)),
                    IsAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Event,
                    Entity = new { id = u.data1 },
                    IsAdding = false
                };
            }
        }

        private static UpdateData GetReferee(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Referee,
                    Entity = ModelHelper.CreateRefereeModel(DatabaseManager.GetRefereeById(u.data1)),
                    IsAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Referee,
                    Entity = new { id = u.data1 },
                    IsAdding = false
                };
            }
        }

        private static UpdateData GetStadium(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Stadium,
                    Entity = ModelHelper.CreateStadiumModel(DatabaseManager.GetStadiumById(u.data1)),
                    IsAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Stadium,
                    Entity = new { id = u.data1 },
                    IsAdding = false
                };
            }
        }

        private static UpdateData GetPlayer(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Player,
                    Entity = ModelHelper.CreatePlayerModel(DatabaseManager.GetPlayerById(u.data1)),
                    IsAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Player,
                    Entity = new { id = u.data1 },
                    IsAdding = false
                };
            }
        }

        private static UpdateData GetMatch(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Match,
                    Entity = ModelHelper.CreateMatchModel(DatabaseManager.GetMatchById(u.data1)),
                    IsAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Match,
                    Entity = new { id = u.data1 },
                    IsAdding = false
                };
            }
        }

        private static UpdateData GetTeam(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Team,
                    Entity = ModelHelper.CreateTeamModel(DatabaseManager.GetTeamById(u.data1)),
                    IsAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Team,
                    Entity = new { id = u.data1 },
                    IsAdding = false
                };
            }
        }

        private static UpdateData GetLeague(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.League,
                    Entity = ModelHelper.CreateLeagueModel(DatabaseManager.GetLeagueById(u.data1)),
                    IsAdding = true
                }; 
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateEnum.League,
                    Entity = new { id = u.data1},
                    IsAdding = false
                };
            }
        } 

    }
}