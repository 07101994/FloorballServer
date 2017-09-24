using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IRefereeRepository : IRepository
    {

        #region READ

        Referee GetRefereeById(int id);

        IEnumerable<Referee> GetRefereesByMatch(int matchId);

        IEnumerable<Referee> GetAllReferee();

        Dictionary<int, List<int>> GetAllRefereeAndMatchId();

        #endregion

        #region CREATE

        int AddReferee(Referee referee);

        #endregion

        #region UPDATE

        int UpdateReferee(Referee referee);

        void AddRefereeToMatch(int refereeId, int matchId);

        void RemoveRefereeFromMatch(int refereeId, int matchId);

        #endregion
    }
}
