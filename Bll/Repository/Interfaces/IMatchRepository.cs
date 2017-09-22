using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IMatchRepository : IDisposable
    {

        #region READ

        Match GetMatchById(int id);

        IEnumerable<Match> GetMatchesByLeague(int leagueId);

        IEnumerable<Match> GetAllMatch();

        IEnumerable<Match> GetMatchesByReferee(int refereeId);

        IEnumerable<Match> GetActualMatches();

        #endregion

        #region CREATE

        int AddMatch(Match match);

        #endregion

        #region UPDATE

        void UpdateMatch(Match match);

        #endregion

    }
}
