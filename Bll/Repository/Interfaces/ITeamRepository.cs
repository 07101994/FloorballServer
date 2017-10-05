using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface ITeamRepository : IRepositoryBase
    {

        #region READ

        Team GetTeamById(int id);

        IEnumerable<Team> GetTeamsByLeague(int leagueId);

        IEnumerable<Team> GetTeamsByYear(DateTime year);

        IEnumerable<Team> GetAllTeam();

        #endregion

        #region CREATE

        int AddTeam(Team team);

        #endregion

        #region UPDATE

        int UpdateTeam(Team team);

        #endregion


    }
}
