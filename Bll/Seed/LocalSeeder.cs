using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using DAL.Seed;

namespace Bll.Seed
{
    public class LocalSeeder : BaseSeeder, ISeeder
    {
        public void Seed(FloorballBaseCtx ctx)
        {
            Init(ctx);

            AddStadiums(ctx);
            AddLeagues(ctx);
            AddTeams(ctx);
            AddMatches(ctx);
            AddPlayers(ctx);
            AddReferees(ctx);
            AddStatistics(ctx);
            AddEvents(ctx);

        }

        private void AddStadiums(FloorballBaseCtx ctx)
        {
            ctx.Stadiums.Add(new Stadium { Id = 1, Name = "Arena Budapest", Address = "Hungary, 1111, Budapest, Valahol Street 5." });
            ctx.Stadiums.Add(new Stadium { Id = 2, Name = "Falun Sport Hall", Address = "Sweden, 1111, Falun, Valahol Street 50." });
            ctx.Stadiums.Add(new Stadium { Id = 3, Name = "University Szeged", Address = "Hungary, 1111, Szeged, University Street 15." });

            ctx.SaveChanges();
        }

        private void AddLeagues(FloorballBaseCtx ctx)
        {
            ctx.Leagues.Add(new League { Id = 1, Name = "Salming League", Country = "HU", Type = "Championship", Rounds = 10, Sex = "men", Year = new DateTime(2016,1,1), ClassName = "first" });
            ctx.Leagues.Add(new League { Id = 2, Name = "Unihoc Cup", Country = "HU", Type = "Cup", Rounds = 5, Sex = "men", Year = new DateTime(2016,1,1), ClassName = "all" });
            ctx.Leagues.Add(new League { Id = 3, Name = "Excel Elit League", Country = "SE", Type = "Championship", Rounds = 8, Sex = "men", Year = new DateTime(2017,1,1), ClassName = "first" });
            ctx.Leagues.Add(new League { Id = 4, Name = "Excel Elit League Women", Country = "SE", Type = "Championship", Rounds = 8, Sex = "women", Year = new DateTime(2017,1,1), ClassName = "first" });

            ctx.SaveChanges();
        }

        private void AddTeams(FloorballBaseCtx ctx)
        {
            ctx.Teams.Add(new Team { Id = 1, TeamId = 1, Name = "FC Budapest", Country = "HU", Coach = "Gipsz Jakab", Year = new DateTime(2016, 1, 1), Sex = "men", LeagueId = 1, StadiumId = 1 });
            ctx.Teams.Add(new Team { Id = 2, TeamId = 1, Name = "FC Budapest", Country = "HU", Coach = "Gipsz Jakab", Year = new DateTime(2016, 1, 1), Sex = "men", LeagueId = 2, StadiumId = 1 });
            ctx.Teams.Add(new Team { Id = 3, TeamId = 2, Name = "Innebandy Club Falun", Country = "SE", Coach = "Jutti Nilson", Year = new DateTime(2017, 1, 1), Sex = "men", LeagueId = 3, StadiumId = 2 });
            ctx.Teams.Add(new Team { Id = 4, TeamId = 3, Name = "Szegedi Farkasok", Country = "HU", Coach = "Vadász János", Year = new DateTime(2016, 1, 1), Sex = "men", LeagueId = 1, StadiumId = 3 });
            ctx.Teams.Add(new Team { Id = 5, TeamId = 4, Name = "Floorball Ladies", Country = "SE", Coach = "Kili Jilli", Year = new DateTime(2017, 1, 1), Sex = "women", LeagueId = 4, StadiumId = 2 });

            ctx.SaveChanges();

        }

        private void AddMatches(FloorballBaseCtx ctx)
        {
            
        }

        private void AddPlayers(FloorballBaseCtx ctx)
        {
            
        }

        private void AddReferees(FloorballBaseCtx ctx)
        {
            ctx.Referees.Add(new Referee { Id = 1, Name = "Bíró János" });
            ctx.Referees.Add(new Referee { Id = 2, Name = "Spori Krisztián" });
            ctx.Referees.Add(new Referee { Id = 3, Name = "Nagy Júlia" });
            ctx.Referees.Add(new Referee { Id = 4, Name = "Miko Mikako" });
            ctx.Referees.Add(new Referee { Id = 5, Name = "Haki Hakiki" });

            ctx.SaveChanges();
        }

        private void AddStatistics(FloorballBaseCtx ctx)
        {

        }

        private void AddEvents(FloorballBaseCtx ctx)
        {
            
        }

    }
}
