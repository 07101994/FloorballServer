using Bll.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Implementations
{
    public class TeamRepository : Repository, ITeamRepository
    {

        public int AddTeam(Team team)
        {
            team.Get = 0;
            team.Scored = 0;
            team.Points = 0;
            team.Standing = 0;

            ctx.Teams.Add(team);
            ctx.SaveChanges();

            AddUpdate(new Update
            {
                name = UpdateEnum.Team.ToUpdateString(),
                updatetype = UpdateType.Create,
                data1 = team.Id,
                date = DateTime.Now
            });
            

            return team.Id;
        }

        public IEnumerable<Team> GetAllTeam()
        {
            return ctx.Teams;
        }

        public Team GetTeamById(int id)
        {
            return ctx.Teams.Find(id);
        }

        public IEnumerable<Team> GetTeamsByLeague(int leagueId)
        {
            return ctx.Teams.Where( t => t.LeagueId == leagueId);
        }

        public IEnumerable<Team> GetTeamsByYear(DateTime year)
        {
            return ctx.Teams.Where( t => t.Year == year );
        }

        public int UpdateTeam(Team team)
        {
            Team updated = ctx.Teams.Find(team.Id);

            updated.Name = team.Name;
            updated.Sex = team.Sex;
            updated.Year = team.Year;
            updated.Coach = team.Coach;
            updated.Country = team.Country;
            updated.StadiumId = team.StadiumId;
            updated.LeagueId = team.LeagueId;

            ctx.Teams.Attach(updated);
            ctx.Entry(updated).State = System.Data.Entity.EntityState.Modified;

            ctx.SaveChanges();

            //AddUpdate(new Update
            //{
            //    name = UpdateEnum.Team.ToUpdateString(),
            //    UpdateType = UpdateType.Create,
            //    data1 = team.Id,
            //    date = DateTime.Now
            //});

            return team.Id;
        }
    }
}
