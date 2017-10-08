using Bll.Seed;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL.Seed
{
    public class DemoSeeder : BaseSeeder, ISeeder
    {

        public void Seed(FloorballBaseCtx ctx)
        {
            Init(ctx);

            AddStadiums(ctx);
            AddReferees(ctx);
            AddLeagues(ctx);
            AddPlayers(ctx);
            AddTeams(ctx);
            AddMatches(ctx);
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
            ctx.Leagues.Add(new League { Id = 1, Name = "Salming League", Country = CountriesEnum.HU, Type = LeagueTypeEnum.League, Rounds = 10, Gender = GenderEnum.Men, Year = new DateTime(2016, 1, 1), Class = ClassEnum.FirstClass });
            ctx.Leagues.Add(new League { Id = 2, Name = "Unihoc Cup", Country = CountriesEnum.HU, Type = LeagueTypeEnum.Cup, Rounds = 5, Gender = GenderEnum.Men, Year = new DateTime(2016, 1, 1), Class = ClassEnum.All });
            ctx.Leagues.Add(new League { Id = 3, Name = "Excel Elit League", Country = CountriesEnum.SE, Type = LeagueTypeEnum.League, Rounds = 8, Gender = GenderEnum.Men, Year = new DateTime(2017, 1, 1), Class = ClassEnum.FirstClass });
            ctx.Leagues.Add(new League { Id = 4, Name = "Excel Elit League Women", Country = CountriesEnum.SE, Type = LeagueTypeEnum.League, Rounds = 8, Gender = GenderEnum.Women, Year = new DateTime(2017, 1, 1), Class = ClassEnum.FirstClass });

            ctx.SaveChanges();
        }

        private void AddTeams(FloorballBaseCtx ctx)
        {
            ctx.Teams.Add(new Team { Id = 1, TeamId = 1, Name = "FC Budapest", Country = CountriesEnum.HU, Coach = "Gipsz Jakab", Year = new DateTime(2016, 1, 1), Gender = GenderEnum.Men, LeagueId = 1, StadiumId = 1, Players = AddEntitiesById(ctx.Players, new List<int> { 1, 2, 3 }) });
            ctx.Teams.Add(new Team { Id = 2, TeamId = 1, Name = "FC Budapest", Country = CountriesEnum.HU, Coach = "Gipsz Jakab", Year = new DateTime(2016, 1, 1), Gender = GenderEnum.Men, LeagueId = 2, StadiumId = 1, Players = AddEntitiesById(ctx.Players, new List<int> { 1, 2, 3 }) });
            ctx.Teams.Add(new Team { Id = 3, TeamId = 2, Name = "Innebandy Club Falun", Country = CountriesEnum.SE, Coach = "Jutti Nilson", Year = new DateTime(2017, 1, 1), Gender = GenderEnum.Men, LeagueId = 3, StadiumId = 2 });
            ctx.Teams.Add(new Team { Id = 4, TeamId = 3, Name = "Szegedi Farkasok", Country = CountriesEnum.HU, Coach = "Vadász János", Year = new DateTime(2016, 1, 1), Gender = GenderEnum.Men, LeagueId = 1, StadiumId = 3, Players = AddEntitiesById(ctx.Players, new List<int> { 4, 5 }) });
            ctx.Teams.Add(new Team { Id = 5, TeamId = 4, Name = "Floorball Ladies", Country = CountriesEnum.SE, Coach = "Kili Jilli", Year = new DateTime(2017, 1, 1), Gender = GenderEnum.Women, LeagueId = 4, StadiumId = 2 });

            ctx.SaveChanges();

        }

        private void AddMatches(FloorballBaseCtx ctx)
        {
            ctx.Matches.Add(new Match { Id = 1, LeagueId = 1, HomeTeamId = 1, AwayTeamId = 4, Date = new DateTime(2017, 04, 04), StadiumId = 1, Time = new TimeSpan(18, 00, 00), Round = 1, Players = AddEntitiesById(ctx.Players, new List<int> { 1, 2, 3, 4, 5 }), Referees = AddEntitiesById(ctx.Referees, new List<int> { 1, 2 }) });
            ctx.Matches.Add(new Match { Id = 2, LeagueId = 1, HomeTeamId = 4, AwayTeamId = 1, Date = new DateTime(2017, 04, 012), StadiumId = 3, Time = new TimeSpan(20, 00, 00), Round = 1, Players = AddEntitiesById(ctx.Players, new List<int> { 1, 2, 3, 4, 5 }), Referees = AddEntitiesById(ctx.Referees, new List<int> { 3, 4 }), });

            ctx.SaveChanges();
        }

        private void AddPlayers(FloorballBaseCtx ctx)
        {

            ctx.Players.Add(new Player { FirstName = "Best", LastName = "Player", Id = 1, BirthDate = new DateTime(1990, 10, 30), Number = 10, });
            ctx.Players.Add(new Player { FirstName = "Worst", LastName = "Player", Id = 2, BirthDate = new DateTime(1980, 11, 3), Number = 8 });
            ctx.Players.Add(new Player { FirstName = "Good", LastName = "Player", Id = 3, BirthDate = new DateTime(1997, 1, 21), Number = 9 });
            ctx.Players.Add(new Player { FirstName = "Another Good", LastName = "Player", Id = 4, BirthDate = new DateTime(1997, 3, 21), Number = 19 });
            ctx.Players.Add(new Player { FirstName = "Noname", LastName = "Player", Id = 5, BirthDate = new DateTime(1967, 1, 1), Number = 29 });

            ctx.SaveChanges();
        }

        private void AddReferees(FloorballBaseCtx ctx)
        {
            ctx.Referees.Add(new Referee { Id = 1, Name = "Referee 1", Number = 1 });
            ctx.Referees.Add(new Referee { Id = 2, Name = "Referee 2", Number = 1 });
            ctx.Referees.Add(new Referee { Id = 3, Name = "Referee 3", Number = 1 });
            ctx.Referees.Add(new Referee { Id = 4, Name = "Referee 4", Number = 1 });
            ctx.Referees.Add(new Referee { Id = 5, Name = "Referee 5" });

            ctx.SaveChanges();
        }

        private void AddStatistics(FloorballBaseCtx ctx)
        {

        }

        private void AddEvents(FloorballBaseCtx ctx)
        {
            ctx.Events.Add(new Event { Id = 1, EventMessageId = 51, MatchId = 1, PlayerId = 1, TeamId = 1, Time = new TimeSpan(00, 10, 10), Type = EventType.G });
            ctx.Events.Add(new Event { Id = 2, EventMessageId = 52, MatchId = 1, PlayerId = 2, TeamId = 1, Time = new TimeSpan(00, 10, 10), Type = EventType.A });
            ctx.Events.Add(new Event { Id = 3, EventMessageId = 10, MatchId = 1, PlayerId = 4, TeamId = 4, Time = new TimeSpan(00, 22, 40), Type = EventType.G });
            ctx.Events.Add(new Event { Id = 4, EventMessageId = 47, MatchId = 1, PlayerId = 5, TeamId = 4, Time = new TimeSpan(00, 51, 00), Type = EventType.P2 });

            ctx.SaveChanges();
        }
    }
}
