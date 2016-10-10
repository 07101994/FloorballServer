using Bll.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Implementations
{
    public class PlayerRepository : Repository , IPlayerRepository
    {


        public int AddPlayer(Player player)
        {
            ctx.Players.Add(player);
            ctx.SaveChanges();

            AddUpdate(ctx, UpdateEnum.Player.ToUpdateString(), DateTime.Now, true, player.RegNum);

            return player.RegNum;
        }

        public void AddPlayerToMatch(int playerId, int matchId)
        {
            var player = ctx.Players.Include("Matches").Include("Teams").Where(p => p.RegNum == playerId).FirstOrDefault();

            var match = ctx.Matches.Include("Players").Where(m => m.Id == matchId).FirstOrDefault();

            if (!(player.Teams.Contains(match.HomeTeam) || player.Teams.Contains(match.AwayTeam)))
                throw new Exception("Player cannot be added to match!");

            match.Players.Add(player);
            ctx.SaveChanges();

            AddUpdate(ctx, UpdateEnum.PlayerMatch.ToUpdateString(), DateTime.Now, true, player.RegNum, match.Id);
        }

        public void AddPlayerToTeam(int playerId, int teamId)
        {
            var player = ctx.Players.Find(playerId);

            var team = ctx.Teams.Find(teamId);

            team.Players.Add(player);
            AddStatisticsForPlayerInTeam(player, team, ctx);
            ctx.SaveChanges();

            AddUpdate(ctx, UpdateEnum.PlayerTeam.ToUpdateString(), DateTime.Now, true, player.RegNum, team.Id);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Player> GetAllPlayer()
        {
            return ctx.Players;
        }

        public Dictionary<int, List<int>> GetAllPlayerAndMatchId()
        {
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            ctx.Matches.Include("Players").ToList().ForEach(m => dict.Add(m.Id, m.Players.Select(p => p.RegNum).ToList()));
            return dict;
        }

        public Dictionary<int, List<int>> GetAllPlayerAndTeamId()
        {
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            ctx.Teams.Include("Players").ToList().ForEach(m => dict.Add(m.Id, m.Players.Select(p => p.RegNum).ToList()));
            return dict;
        }

        public Player GetPlayerById(int id)
        {
            return ctx.Players.Find(id);
        }

        public IEnumerable<Player> GetPlayersByLeague(int leagueId)
        {
            List<Player> players = new List<Player>();

            IEnumerable<ICollection<Player>> playerCollections = ctx.Leagues.Include("Teams.Players").Where(l => l.Id == leagueId).First().Teams.Select(t => t.Players);
            foreach (var player in playerCollections)
            {
                players.AddRange(player);
            }

            return players;
        }

        public IEnumerable<Player> GetPlayersByMatch(int id)
        {
            return ctx.Matches.Include("Players").Where(m => m.Id == id).First().Players;
        }

        public IEnumerable<Player> GetPlayersByTeam(int teamId)
        {
            return ctx.Teams.Find(teamId).Players;
        }

        public void RemovePlayerFromMatch(int playerId, int matchId)
        {
            var player = ctx.Players.Include("Matches").Include("Teams").Where(p => p.RegNum == playerId).FirstOrDefault();

            var match = ctx.Matches.Include("Players").Where(m => m.Id == matchId).FirstOrDefault();

            if (!(player.Teams.Contains(match.HomeTeam) || player.Teams.Contains(match.AwayTeam)))
                throw new Exception("Player cannot be removed from match!");


            match.Players.Remove(player);

            ctx.Matches.Attach(match);
            ctx.Entry(match).Property(e => e.Players).IsModified = true;

            ctx.SaveChanges();

            AddUpdate(ctx, UpdateEnum.PlayerMatch.ToUpdateString(), DateTime.Now, false, playerId, matchId);
        }

        public void RemovePlayerFromTeam(int playerId, int teamId)
        {
            var player = ctx.Players.Find(playerId);

            var team = ctx.Teams.Find(teamId);

            RemoveStatisticsForPlayerInTeam(player, team, ctx);
            team.Players.Remove(player);

            //db.Teams.Attach(team);
            //db.Entry(team).Collection(e => e.Players). = true;

            ctx.SaveChanges();

            AddUpdate(ctx, UpdateEnum.PlayerTeam.ToUpdateString(), DateTime.Now, false, playerId, teamId);
        }
    }
}
