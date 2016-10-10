using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Interfaces
{
    public interface IPlayerRepository : IDisposable
    {

        #region READ

        Player GetPlayerById(int id);

        IEnumerable<Player> GetPlayersByLeague(int leagueId);

        IEnumerable<Player> GetPlayersByMatch(int matchId);

        IEnumerable<Player> GetPlayersByTeam(int teamId);

        IEnumerable<Player> GetAllPlayer();

        Dictionary<int, List<int>> GetAllPlayerAndMatchId();

        Dictionary<int, List<int>> GetAllPlayerAndTeamId();

        #endregion

        #region CREATE

        int AddPlayer(Player player);

        #endregion

        #region UPDATE

        void AddPlayerToMatch(int playerId, int matchId);

        void AddPlayerToTeam(int playerId, int matchId);

        void RemovePlayerFromMatch(int playerId, int matchId);

        void RemovePlayerFromTeam(int playerId, int matchId);

        #endregion

    }
}
