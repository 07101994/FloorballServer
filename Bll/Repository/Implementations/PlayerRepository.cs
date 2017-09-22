using DAL.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Implementations
{
    public class PlayerRepository : Repository , IPlayerRepository
    {
        public int AddPlayer(Player player)
        {
            Ctx.Players.Add(player);
            Ctx.SaveChanges();

            AddUpdate(new Update
            {
                updatetype = UpdateType.Create.ToString(),
                name = UpdateEnum.Player.ToUpdateString(),
                date = DateTime.Now,
                data1 = player.RegNum
            });

            return player.RegNum;
        }

        public void AddPlayerToMatch(int playerId, int matchId)
        {
            var player = Ctx.Players.Include("Matches").Include("Teams").Where(p => p.RegNum == playerId).FirstOrDefault();

            var match = Ctx.Matches.Include("Players").Where(m => m.Id == matchId).FirstOrDefault();

            if (!(player.Teams.Contains(match.HomeTeam) || player.Teams.Contains(match.AwayTeam)))
                throw new Exception("Player cannot be added to match!");

            match.Players.Add(player);
            Ctx.SaveChanges();

            AddUpdate(new Update
            {
                name = UpdateEnum.PlayerMatch.ToUpdateString(),
                date = DateTime.Now,
                updatetype = UpdateType.Create.ToString(),
                data1 = player.RegNum,
                data2 = match.Id
            });
        }

        public void AddPlayerToTeam(int playerId, int teamId)
        {
            var player = Ctx.Players.Find(playerId);

            var team = Ctx.Teams.Find(teamId);

            team.Players.Add(player);
            AddStatisticsForPlayerInTeam(player, team, Ctx);
            Ctx.SaveChanges();

            AddUpdate(new Update
            {
                name = UpdateEnum.PlayerTeam.ToUpdateString(),
                date = DateTime.Now,
                updatetype = UpdateType.Create.ToString(),
                data1 = player.RegNum,
                data2 = team.Id
            });
        }

        private void AddStatisticsForPlayerInTeam(Player player, Team team, FloorballEntities ctx)
        {
            string[] types = new string[] { "G", "A", "P2", "P5", "P10", "PV", "APP" };

            foreach (var type in types)
            {
                Statistic s = new Statistic();
                s.Name = type;
                s.Number = 0;
                s.Team = team;
                s.Player = player;

                ctx.Statistics.Add(s);

                //AddUpdate(db, "addStat", DateTime.Now, player.RegNum, team.Id);

            }
        }

        public IEnumerable<Player> GetAllPlayer()
        {
            return Ctx.Players;
        }

        public Dictionary<int, List<int>> GetAllPlayerAndMatchId()
        {
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            Ctx.Matches.Include("Players").Where( m => m.Players.Any()).ToList().ForEach(m => dict.Add(m.Id, m.Players.Select(p => p.RegNum).ToList()));
            return dict;
        }

        public Dictionary<int, List<int>> GetAllPlayerAndTeamId()
        {
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            Ctx.Teams.Include("Players").Where( t => t.Players.Any()).ToList().ForEach(t => dict.Add(t.Id, t.Players.Select(p => p.RegNum).ToList()));
            return dict;
        }

        public Player GetPlayerById(int id)
        {
            return Ctx.Players.Find(id);
        }

        public IEnumerable<Player> GetPlayersByLeague(int leagueId)
        {
            List<Player> players = new List<Player>();

            IEnumerable<ICollection<Player>> playerCollections = Ctx.Leagues.Include("Teams.Players").Where(l => l.Id == leagueId).First().Teams.Select(t => t.Players);
            foreach (var player in playerCollections)
            {
                players.AddRange(player);
            }

            return players;
        }

        public IEnumerable<Player> GetPlayersByMatch(int id)
        {
            return Ctx.Matches.Include("Players").Where(m => m.Id == id).First().Players;
        }

        public IEnumerable<Player> GetPlayersByTeam(int teamId)
        {
            return Ctx.Teams.Find(teamId).Players;
        }

        public void RemovePlayerFromMatch(int playerId, int matchId)
        {
            var player = Ctx.Players.Include("Matches").Include("Teams").Where(p => p.RegNum == playerId).FirstOrDefault();

            var match = Ctx.Matches.Include("Players").Where(m => m.Id == matchId).FirstOrDefault();

            if (!(player.Teams.Contains(match.HomeTeam) || player.Teams.Contains(match.AwayTeam)))
                throw new Exception("Player cannot be removed from match!");


            match.Players.Remove(player);

            Ctx.Matches.Attach(match);
            Ctx.Entry(match).Property(e => e.Players).IsModified = true;

            Ctx.SaveChanges();

            AddUpdate(new Update
            {
                name = UpdateEnum.PlayerMatch.ToUpdateString(),
                date = DateTime.Now,
                updatetype = UpdateType.Delete.ToString(), 
                data1 = playerId,
                data2 = matchId
            });
        }

        public void RemovePlayerFromTeam(int playerId, int teamId)
        {
            var player = Ctx.Players.Find(playerId);

            var team = Ctx.Teams.Find(teamId);

            RemoveStatisticsForPlayerInTeam(player, team, Ctx);
            team.Players.Remove(player);

            Ctx.SaveChanges();

            AddUpdate(new Update
            {
                name = UpdateEnum.PlayerTeam.ToUpdateString(),
                date = DateTime.Now,
                data1 = playerId,
                data2 = teamId,
                updatetype = UpdateType.Delete.ToString()
            });
        }

        private void RemoveStatisticsForPlayerInTeam(Player player, Team team, FloorballEntities ctx)
        {
            var statisctics = ctx.Statistics.Where(s => s.Player.RegNum == player.RegNum && s.Team.Id == team.Id).ToList();

            foreach (var s in statisctics)
            {
                ctx.Statistics.Remove(s);
            }
        }

        public int UpdatePlayer(Player player)
        {
            var updated = Ctx.Players.Find(player.RegNum);

            updated.Date = player.Date;
            updated.FirstName = player.FirstName;
            updated.Number = player.Number;
            updated.SecondName = player.SecondName;

            Ctx.Players.Attach(updated);
            Ctx.Entry(updated).State = System.Data.Entity.EntityState.Modified;

            Ctx.SaveChanges();

            return updated.RegNum;
        }
    }
}
