using Bll.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Implementations
{
    public class PlayerRepository : IPlayerRepository
    {

        [Inject]
        public FloorballEntities ctx { get; set; }

        public int AddPlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public void AddPlayerToMatch(int playerId, int matchId)
        {
            throw new NotImplementedException();
        }

        public void AddPlayerToTeam(int playerId, int matchId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Player> GetAllPlayer()
        {
            throw new NotImplementedException();
        }

        public Player GetPlayerById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Player> GetPlayersByLeague(int leagueId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Player> GetPlayersByMatch(int matchId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Player> GetPlayersByTeam(int teamId)
        {
            throw new NotImplementedException();
        }

        public void RemovePlayerFromMatch(int playerId, int matchId)
        {
            throw new NotImplementedException();
        }

        public void RemovePlayerFromTeam(int playerId, int matchId)
        {
            throw new NotImplementedException();
        }
    }
}
