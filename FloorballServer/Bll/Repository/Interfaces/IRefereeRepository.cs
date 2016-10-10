using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Interfaces
{
    public interface IRefereeRepository : IDisposable
    {

        #region READ

        Referee GetrefereeById(int id);

        IEnumerable<Referee> GetRefereesByMatch(int matchId);

        IEnumerable<Referee> GetAllReferee();

        #endregion

        #region CREATE

        int AddReferee(Referee referee);

        #endregion

        #region UPDATE

        void AddRefereeToMatch(int refereeId, int matchId);

        void RemoveRefereeFromMatch(int refereeId, int matchId);

        #endregion
    }
}
