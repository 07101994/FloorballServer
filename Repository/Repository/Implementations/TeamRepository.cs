using DAL.Model;
using DAL.Repository.Interfaces; 
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Implementations
{
    public class TeamRepository : FlorballRepository, ITeamRepository
    {
        public int AddTeam(Team team)
        {
            team.Get = 0;
            team.Scored = 0;
            team.Points = 0;
            team.Standing = 0;

            Ctx.Teams.Add(team);
            Ctx.SaveChanges();

            team.ImageName = team.Id + ".jpg";
            Ctx.Teams.Attach(team);
            Ctx.Entry(team).Property("ImageName").IsModified = true;
            Ctx.SaveChanges();

            AddUpdate(new Update
            {
                Name = UpdateEnum.Team,
                Updatetype = UpdateType.Create,
                Data1 = team.Id,
                Date = DateTime.Now
            });
            

            return team.Id;
        }

        public IEnumerable<Team> GetAllTeam()
        {
            return Ctx.Teams;
        }

        public Team GetTeamById(int id)
        {
            return Ctx.Teams.Find(id);
        }

        public IEnumerable<Team> GetTeamsByLeague(int leagueId)
        {
            return Ctx.Teams.Where( t => t.LeagueId == leagueId);
        }

        public IEnumerable<Team> GetTeamsByYear(DateTime year)
        {
            return Ctx.Teams.Where( t => t.Year == year );
        }

        public int UpdateTeam(Team team)
        {
            Team updated = Ctx.Teams.Find(team.Id);

            updated.Name = team.Name;
            updated.Gender = team.Gender;
            updated.Year = team.Year;
            updated.Coach = team.Coach;
            updated.Country = team.Country;
            updated.StadiumId = team.StadiumId;
            updated.LeagueId = team.LeagueId;

            Ctx.Teams.Attach(updated);
            Ctx.Entry(updated).State = System.Data.Entity.EntityState.Modified;

            Ctx.SaveChanges();

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
