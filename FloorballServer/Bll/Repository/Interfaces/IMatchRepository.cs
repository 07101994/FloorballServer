using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Interfaces
{
    public interface IMatchRepository : IDisposable
    {

        #region READ

        Match GetMatchById(int id);

        Match GetMatchesByLeague(int leagueId);

        Match GetMatchesByYear(DateTime year);

        IEnumerable<Match> GetAllMatch();

        IEnumerable<Match> GetMatchesByReferee(int refereeId);

        IEnumerable<Match> GetActualMatches();

        #endregion

        #region CREATE

        int AddMatch(Match match);

        #endregion

        #region UPDATE

        void UpdateMatch(int matchId, DateTime date, TimeSpan time, short round, int stadiumId, short goalsh, short goalsa, string state);

        #endregion

    }
}
