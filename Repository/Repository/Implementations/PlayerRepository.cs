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
    public class PlayerRepository : FlorballRepository , IPlayerRepository
    {
        public int AddPlayer(Player player)
        {
            Ctx.Players.Add(player);
            Ctx.SaveChanges();

            AddUpdate(new Update
            {
                Updatetype = UpdateType.Create,
                Name = UpdateEnum.Player,
                Date = DateTime.Now,
                Data1 = player.Id
            });

            return player.Id;
        }

        public void AddPlayerToMatch(int playerId, int matchId)
        {
            var player = Ctx.Players.Include("Matches").Include("Teams").Where(p => p.Id == playerId).FirstOrDefault();

            var match = Ctx.Matches.Include("Players").Where(m => m.Id == matchId).FirstOrDefault();

            if (!(player.Teams.Contains(match.HomeTeam) || player.Teams.Contains(match.AwayTeam)))
                throw new Exception("Player cannot be added to match!");

            match.Players.Add(player);
            Ctx.SaveChanges();

            AddUpdate(new Update
            {
                Name = UpdateEnum.PlayerMatch,
                Date = DateTime.Now,
                Updatetype = UpdateType.Create,
                Data1 = player.Id,
                Data2 = match.Id
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
                Name = UpdateEnum.PlayerTeam,
                Date = DateTime.Now,
                Updatetype = UpdateType.Create,
                Data1 = player.Id,
                Data2 = team.Id
            });
        }

        private void AddStatisticsForPlayerInTeam(Player player, Team team, FloorballBaseCtx ctx)
        {

            foreach (var type in Enum.GetValues(typeof(StatType)))
            {
                Statistic s = new Statistic();
                s.Type = (StatType)type;
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
            Ctx.Matches.Include("Players").Where( m => m.Players.Any()).ToList().ForEach(m => dict.Add(m.Id, m.Players.Select(p => p.Id).ToList()));
            return dict;
        }

        public Dictionary<int, List<int>> GetAllPlayerAndTeamId()
        {
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            Ctx.Teams.Include("Players").Where( t => t.Players.Any()).ToList().ForEach(t => dict.Add(t.Id, t.Players.Select(p => p.Id).ToList()));
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
            var player = Ctx.Players.Include("Matches").Include("Teams").Where(p => p.Id == playerId).FirstOrDefault();

            var match = Ctx.Matches.Include("Players").Where(m => m.Id == matchId).FirstOrDefault();

            if (!(player.Teams.Contains(match.HomeTeam) || player.Teams.Contains(match.AwayTeam)))
                throw new Exception("Player cannot be removed from match!");


            match.Players.Remove(player);

            Ctx.Matches.Attach(match);
            Ctx.Entry(match).Property(e => e.Players).IsModified = true;

            Ctx.SaveChanges();

            AddUpdate(new Update
            {
                Name = UpdateEnum.PlayerMatch,
                Date = DateTime.Now,
                Updatetype = UpdateType.Delete, 
                Data1 = playerId,
                Data2 = matchId
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
                Name = UpdateEnum.PlayerTeam,
                Date = DateTime.Now,
                Data1 = playerId,
                Data2 = teamId,
                Updatetype = UpdateType.Delete
            });
        }

        private void RemoveStatisticsForPlayerInTeam(Player player, Team team, FloorballBaseCtx ctx)
        {
            var statisctics = ctx.Statistics.Where(s => s.Player.Id == player.Id && s.Team.Id == team.Id).ToList();

            foreach (var s in statisctics)
            {
                ctx.Statistics.Remove(s);
            }
        }

        public int UpdatePlayer(Player player)
        {
            var updated = Ctx.Players.Find(player.Id);

            updated.BirthDate = player.BirthDate;
            updated.FirstName = player.FirstName;
            updated.Number = player.Number;
            updated.LastName = player.LastName;

            Ctx.Players.Attach(updated);
            Ctx.Entry(updated).State = System.Data.Entity.EntityState.Modified;

            Ctx.SaveChanges();

            return updated.Id;
        }
    }
}
