using Bll;
using Bll.Repository;
using FloorballServer.Models.Floorball;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Helper;

namespace FloorballServer.Helper
{
    public class UpdateCollector
    {
        private IUnitOfWork UoW;

        public UpdateCollector(IUnitOfWork UoW)
        {
            this.UoW = UoW;
        }

        public UpdateModel CollectUpdates(IEnumerable<Update> updates, DateTime updateTime)
        {
            UpdateModel updateModel = new UpdateModel();
            updateModel.UpdateTime = updateTime;

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

            updateModel.Updates = updateDataList;

            return updateModel;
        }

        private UpdateData GetRefereeToMatch(Update u)
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

        private UpdateData GetPlayerToMatch(Update u)
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

        private UpdateData GetPlayerToTeam(Update u)
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

        private UpdateData GetEventMessage(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.EventMessage,
                    Entity = ModelHelper.CreateEventMessageModel(UoW.EventMessageRepository.GetEventMessageById(u.data1)),
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

        private UpdateData GetEvent(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Event,
                    Entity = ModelHelper.CreateEventModel(UoW.EventRepository.GetEventById(u.data1)),
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

        private UpdateData GetReferee(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Referee,
                    Entity = ModelHelper.CreateRefereeModel(UoW.RefereeRepository.GetRefereeById(u.data1)),
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

        private UpdateData GetStadium(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Stadium,
                    Entity = ModelHelper.CreateStadiumModel(UoW.StadiumRepository.GetStadiumById(u.data1)),
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

        private UpdateData GetPlayer(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Player,
                    Entity = ModelHelper.CreatePlayerModel(UoW.PlayerRepository.GetPlayerById(u.data1)),
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

        private UpdateData GetMatch(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Match,
                    Entity = ModelHelper.CreateMatchModel(UoW.MatchRepository.GetMatchById(u.data1)),
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

        private UpdateData GetTeam(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.Team,
                    Entity = ModelHelper.CreateTeamModel(UoW.TeamRepository.GetTeamById(u.data1)),
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

        private UpdateData GetLeague(Update u)
        {
            if (u.isAdding)
            {
                return new UpdateData
                {
                    Type = UpdateEnum.League,
                    Entity = ModelHelper.CreateLeagueModel(UoW.LeagueRepository.GetLeagueById(u.data1)),
                    IsAdding = true
                };
            }
            else
            {
                return new UpdateData
                {
                    Type = UpdateEnum.League,
                    Entity = new { id = u.data1 },
                    IsAdding = false
                };
            }
        }
    }
}