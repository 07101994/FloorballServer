using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class DatabaseManager
    {

        #region GET

        public static List<DateTime> GetAllYear()
        {
            using (var db = new FloorballEntities())
            {
                var q = from l in db.Leagues
                        select l.Year;

                return q.Distinct().OrderBy(t => t.Year).ToList();
            }
        }

        public static List<League> GetAllLeague()
        {
            using (var db = new FloorballEntities())
            {
                return db.Leagues.ToList();
            }
        }

        public static List<Player> GetAllPlayer()
        {
            using (var db = new FloorballEntities())
            {
                return db.Players.ToList();
            }
        }

        public static List<Team> GetTeamsByLeague(int id)
        {
            using (var db = new FloorballEntities())
            {

                return db.Leagues.Include("Teams").Where(l => l.Id == id).First().Teams.ToList();

            }
        }

        public static List<Match> GetMatchesByLeague(int id)
        {
            using (var db = new FloorballEntities())
            {
                return db.Leagues.Include("Matches").Include("Matches.HomeTeam").Include("Matches.AwayTeam").Where(l => l.Id == id).First().Matches.ToList();
            }
        }


        public static List<Team> GetTeamsByYear(DateTime year)
        {
            using (var db = new FloorballEntities())
            {
                var q = from t in db.Teams
                        where t.Year.Year == year.Year
                        select t;

                return q.ToList();
            }
        }

        public static List<Player> GetPlayersByTeam(int id)
        {
            using (var db = new FloorballEntities())
            {
                var q = (from t in db.Teams
                         where t.Id == id
                         select t.Players).First();

                return q.ToList();
            }
        }

        public static List<Player> GetPlayersByLeague(int leagueId)
        {
            using (var db = new FloorballEntities())
            {

                List<Player> players = new List<Player>();

                IEnumerable<ICollection<Player>> playerCollections = db.Leagues.Include("Teams.Players").Where(l => l.Id == leagueId).First().Teams.Select(t => t.Players);
                foreach (var player in playerCollections)
                {
                    players.AddRange(player);
                }

                return players;
            }
        }

        public static List<Player> GetPlayersByMatch(int id)
        {
            using (var db = new FloorballEntities())
            {
                return db.Matches.Include("Players").Where(m => m.Id == id).First().Players.ToList();
            }
        }

        public static List<Statistic> GetStatisticsByleague(int leagueId)
        {
            using (var db = new FloorballEntities())
            {
                return db.Statistics.Where(s => s.Team.LeagueId == leagueId).ToList();
            }
        }

        public static Statistic GetStatisticById(int statId)
        {
            using (var db = new FloorballEntities())
            {
                return db.Statistics.Where(s => s.Id == statId).First();
            }
        }

        public static Stadium GetStadiumById(int stadiumId)
        {
            using (var db = new FloorballEntities())
            {
                return db.Stadiums.Where(s => s.Id == stadiumId).First();
            }
        }

        public static Match GetMatchById(int id)
        {
            using (var db = new FloorballEntities())
            {
                var q = (from m in db.Matches.Include("HomeTeam").Include("AwayTeam").Include("Players").Include("League").Include("HomeTeam.Players").Include("AwayTeam.Players")
                         where m.Id == id
                         select m).First();

                return q;
            }
        }

        public static List<Match> GetActualMatches()
        {
            using (var db = new FloorballEntities())
            {
                DateTime threshold = DateTime.Now.AddDays(3);
                
                return db.Matches.Where(m => m.Date < threshold && m.Date >= DateTime.Now).ToList();
            }
        }

        public static List<Referee> GetAllReferee()
        {
            using (var db = new FloorballEntities())
            {
                return db.Referees.ToList();
            }
        }

        public static List<Match> GetMatchesByReferee(int refereeId)
        {
            using (var db = new FloorballEntities())
            {
                return db.Referees.Include("Matches").Include("Matches.Players").Where(r => r.Id == refereeId).First().Matches.ToList();
            }
        }

        public static List<Stadium> GetAllStadium()
        {
            using (var db = new FloorballEntities())
            {
                return db.Stadiums.ToList();
            }
        }

        public static List<Match> GetAllMatch()
        {
            using (var db = new FloorballEntities())
            {
                return db.Matches.ToList();
            }
        }

        public static List<Event> GetAllEvent()
        {
            using (var db = new FloorballEntities())
            {
                return db.Events.ToList();
            }
        }

        public static List<EventMessage> GetAllEventMessage()
        {
            using (var db = new FloorballEntities())
            {
                return db.EventMessages.ToList();
            }
        }

        public static List<Statistic> GetAllStatistic()
        {
            using (var db = new FloorballEntities())
            {
                return db.Statistics.ToList();
            }
        }


        public static List<Team> GetAllTeam()
        {
            using (var db = new FloorballEntities())
            {
                return db.Teams.ToList();
            }
        }

        public static List<List<int>> GetAllPlayerAndMatchId()
        {
            using (var db = new FloorballEntities())
            {
                List<List<int>> list = new List<List<int>>();
                foreach (var player in db.Players.Include("Matches"))
                {
                    foreach (var match in player.Matches)
                    {
                        list.Add(new List<int> { player.RegNum, match.Id});

                    }

                }

                return list;
            }
        }

        public static List<List<int>> GetAllPlayerAndTeamId()
        {
            using (var db = new FloorballEntities())
            {
                List<List<int>> list = new List<List<int>>();
                foreach (var player in db.Players.Include("Teams"))
                {
                    foreach (var team in player.Teams)
                    {
                        list.Add(new List<int> { player.RegNum, team.Id });

                    }

                }

                return list;
            }
        }

        public static List<List<int>> GetAllRefereeAndMatchId()
        {
            using (var db = new FloorballEntities())
            {
                List<List<int>> list = new List<List<int>>();
                foreach (var referee in db.Referees.Include("Matches"))
                {
                    foreach (var match in referee.Matches)
                    {
                        list.Add(new List <int> { referee.Id, match.Id });

                    }

                }

                return list;
            }
        }

        public static int GetNumberOfRoundsInLeague(int leagueId)
        {
            using (var db = new FloorballEntities())
            {
                return db.Leagues.Where(l => l.Id == leagueId).First().Rounds;
            }
        }

        public static League GetLeagueById(int leagueId)
        {
            using (var db = new FloorballEntities())
            {
                return db.Leagues.Where(l => l.Id == leagueId).First();
            }
        }

        public static List<League> GetLeaguesByYear(DateTime year)
        {
            using (var db = new FloorballEntities())
            {
                return db.Leagues.Where(l => l.Year == year).ToList();
            }
        }


        public static List<EventMessage> GetEventMessagesByCategory(char catagoryStartNumber)
        {
            using (var db = new FloorballEntities())
            {
                return db.EventMessages.ToList().Where(e => e.Code.ToString()[0] == catagoryStartNumber).ToList();
            }
        }

        public static EventMessage GetEventMessageById(int id)
        {
            using (var db = new FloorballEntities())
            {
                return db.EventMessages.Where(e => e.Id == id).First();
            }
        }

        public static List<Event> GetEventsByMatch(int matchId)
        {

            using (var db = new FloorballEntities())
            {
                return db.Matches.Include("Events").Include("Events.Player").Include("Events.Eventmessage").Where(m => m.Id == matchId).First().Events.OrderByDescending(e => e.Time).ToList();

            }

        }

        public static Event GetEventById(int eventId)
        {
            using (var db = new FloorballEntities())
            {
                return db.Events.Include("EventMessage").Include("Player").Where(e => e.Id == eventId).First();
            }
        }

        public static Team GetTeamById(int id)
        {
            using (var db = new FloorballEntities())
            {
                return db.Teams.Where(t => t.Id == id).First();
            }
        }

        public static Referee GetRefereeById(int id)
        {
            using (var db = new FloorballEntities())
            {
                return db.Referees.Where(r => r.Id == id).First();
            }
        }

        public static Player GetPlayerById(int id)
        {
            using (var db = new FloorballEntities())
            {
                return db.Players.Where(p => p.RegNum == id).First();
            }
        }

        public static List<Update> GetUpdatesAfterDate(DateTime date)
        {
            using (var db = new FloorballEntities())
            {
                return db.Updates.Where(u => u.date > date).ToList();
            }
        }

        #endregion


        #region POST

        public static int AddLeague(string name, DateTime year, string type, string classname, int rounds)
        {
            using (var db = new FloorballEntities())
            {

                League l = new League();
                l.Name = name;
                l.Year = year;
                l.Type = type;
                l.ClassName = classname;
                l.Rounds = rounds;

                db.Leagues.Add(l);
                db.SaveChanges();

                AddUpdate(db, "addLeague", DateTime.Now, l.Id);

                return l.Id;
            }
        }

        public static int AddStadium(string name, string address)
        {
            using (var db = new FloorballEntities())
            {
                Stadium s = new Stadium();
                s.Name = name;
                s.Address = address;


                db.Stadiums.Add(s);
                db.SaveChanges();

                AddUpdate(db, "addStadium", DateTime.Now, s.Id);


                return s.Id;
            }
        }

        public static int AddTeam(string name, string sex, DateTime year, string coach, int stadiumId, int leagueId)
        {
            using (var db = new FloorballEntities())
            {
                Team t = new Team();
                t.Name = name;
                t.Year = year;
                t.Coach = coach;
                t.Get = 0;
                t.Scored = 0;
                t.Points = 0;
                t.StadiumId = stadiumId;
                t.LeagueId = leagueId;
                t.Sex = sex;
                t.Standing = (short)(db.Leagues.Include("Teams").Where(l => l.Id == leagueId).First().Teams.Count + 1);

                db.Teams.Add(t);

                //AddUpdate(db, "addTeam", DateTime.Now, t.Id);

                //try
                //{
                //    db.SaveChanges();

                //}
                //catch (DbEntityValidationException e)
                //{
                //    foreach (var eve in e.EntityValidationErrors)
                //    {
                //        System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //        foreach (var ve in eve.ValidationErrors)
                //        {
                //            System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                //ve.PropertyName,
                //eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                //ve.ErrorMessage);
                //        }
                //    }
                //}

                db.SaveChanges();

                AddUpdate(db, "addTeam", DateTime.Now, t.Id);


                return t.Id;
            }
        }

        public static int AddReferee(string name)
        {
            using (var db = new FloorballEntities())
            {
                Referee r = new Referee();
                r.Name = name;
                r.Number = 0;
                r.Penalty = 0;

                db.Referees.Add(r);
                db.SaveChanges();

                AddUpdate(db, "addReferee", DateTime.Now, r.Id);


                return r.Id;
            }
        }

        public static int AddPlayer(string name, int regNum, int number, DateTime date)
        {
            using (var db = new FloorballEntities())
            {
                Player p = new Player();
                p.Name = name;
                p.RegNum = regNum;
                p.Number = (short)number;
                p.Date = date.Date;

                db.Players.Add(p);
                db.SaveChanges();

                AddUpdate(db, "addPlayer", DateTime.Now, p.RegNum);

                return p.RegNum;
            }
        }

        public static void AddPlayerToTeam(int playerId, int teamId)
        {
            using (var db = new FloorballEntities())
            {
                var player = (from p in db.Players
                              where p.RegNum == playerId
                              select p).First();

                var team = (from t in db.Teams
                            where t.Id == teamId
                            select t).First();

                team.Players.Add(player);
                AddStatisticsForPlayerInTeam(player, team, db);
                db.SaveChanges();

                AddUpdate(db, "playerToTeam", DateTime.Now, player.RegNum, team.Id);

            }
        }

        public static void AddPlayerToMatch(int playerId, int matchId)
        {
            using (var db = new FloorballEntities())
            {

                var player = (from p in db.Players.Include("Matches").Include("Teams")
                              where p.RegNum == playerId
                              select p).First();

                var match = (from m in db.Matches.Include("Players")
                             where m.Id == matchId
                             select m).First();

                if (!(player.Teams.Contains(match.HomeTeam) || player.Teams.Contains(match.AwayTeam)))
                    throw new Exception("Player cannot be added to match!");

                match.Players.Add(player);
                db.SaveChanges();

                AddUpdate(db, "playerToMacth", DateTime.Now, player.RegNum, match.Id);

            }
        }

        private static void AddStatisticsForPlayerInTeam(Player player, Team team, FloorballEntities db)
        {

            string[] types = new string[] { "G", "A", "P2", "P5", "P10", "PV", "APP" };

            foreach (var type in types)
            {
                Statistic s = new Statistic();
                s.Name = type;
                s.Number = 0;
                s.Team = team;
                s.Player = player;

                db.Statistics.Add(s);

                //AddUpdate(db, "addStat", DateTime.Now, player.RegNum, team.Id);
            }
        }

        public static void AddRefereeToMatch(int refereeId, int matchId)
        {
            using (var db = new FloorballEntities())
            {
                var referee = db.Referees.Where(r => r.Id == refereeId).First();
                var match = db.Matches.Where(m => m.Id == matchId).First();

                match.Referees.Add(referee);
                db.SaveChanges();

                AddUpdate(db, "addRefereeToMatch", DateTime.Now, refereeId, matchId);
            }
        }

        public static int AddEvent(int matchId, string type, TimeSpan time, int playerId, int evenetMessageId, int teamId)
        {
            using (var db = new FloorballEntities())
            {
                Event e = new Event();
                e.MatchId = matchId;
                e.PlayerRegNum = playerId;
                e.EventMessageId = evenetMessageId;
                e.Time = time;
                e.Type = type;
                e.TeamId = teamId;

                db.Events.Add(e);


                if (playerId != -1 && type != "I" && type != "B")
                {
                    ChangeStatisticFromPlayer(playerId, teamId, type, db, "increase");
                }

                db.SaveChanges();

                AddUpdate(db, "addEvent", DateTime.Now, e.Id);


                return e.Id;
            }
        }

        private static void AddUpdate(FloorballEntities db, string name, DateTime date, int data1, int data2 = -1)
        {
            
            Update u = new Update();
            u.name = name;
            u.date = date;
            u.data1 = data1;
            u.data2 = data2;

            db.Updates.Add(u);

            
                try
                {
                    db.SaveChanges();

                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                ve.PropertyName,
                eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                ve.ErrorMessage);
                        }
                    }
                }

        }

        #endregion


        #region DELETE

        public static void RemovePlayerFromTeam(int playerId, int teamId)
        {
            using (var db = new FloorballEntities())
            {
                var player = (from p in db.Players
                              where p.RegNum == playerId
                              select p).First();

                var team = (from t in db.Teams
                            where t.Id == teamId
                            select t).First();

                RemoveStatisticsForPlayerInTeam(player, team, db);
                team.Players.Remove(player);

                //db.Teams.Attach(team);
                //db.Entry(team).Collection(e => e.Players). = true;
                
                db.SaveChanges();

                AddUpdate(db, "removePlayerFromTeam", DateTime.Now, playerId, teamId);


            }
        }

        public static void RemovePlayerFromMatch(int playerId, int matchId)
        {
            using (var db = new FloorballEntities())
            {

                var player = (from p in db.Players.Include("Matches").Include("Teams")
                              where p.RegNum == playerId
                              select p).First();

                var match = (from m in db.Matches.Include("Players")
                             where m.Id == matchId
                             select m).First();

                if (!(player.Teams.Contains(match.HomeTeam) || player.Teams.Contains(match.AwayTeam)))
                    throw new Exception("Player cannot be removed from match!");


                match.Players.Remove(player);

                db.Matches.Attach(match);
                db.Entry(match).Property(e => e.Players).IsModified = true;


                db.SaveChanges();

                AddUpdate(db, "removePlayerFromMatch", DateTime.Now, playerId, matchId);


            }
        }

        private static void RemoveStatisticsForPlayerInTeam(Player player, Team team, FloorballEntities db)
        {

            var statisctics = db.Statistics.Where(s => s.Player.RegNum == player.RegNum && s.Team.Id == team.Id).ToList();

            foreach (var s in statisctics)
            {
                db.Statistics.Remove(s);
            }

        }

        public static void RemoveEvent(int eventId)
        {
            using (var db = new FloorballEntities())
            {
                var e = db.Events.Include("Match.HomeTeam.Players").Include("Match.AwayTeam.Players").Where(ev => ev.Id == eventId).First();

                if (e.Type == "G")
                {
                    int t;

                    if (e.Match.HomeTeam.Players.Contains(e.Player))
                    {
                        t = e.Match.HomeTeamId;
                    }
                    else
                    {
                        t = e.Match.AwayTeamId;
                    }

                    ChangeStatisticFromPlayer(e.PlayerRegNum, t, e.Type, db, "reduce");

                    var e1 = db.Events.Include("Match.HomeTeam").Include("Match.AwayTeam").Where(ev => ev.MatchId == e.MatchId && ev.Time == e.Time && ev.Type == "A").First();

                    if (e1.Match.HomeTeam.Players.Contains(e1.Player))
                    {
                        t = e1.Match.HomeTeamId;
                    }
                    else
                    {
                        t = e1.Match.AwayTeamId;
                    }

                    db.Events.Remove(e1);

                    AddUpdate(db, "removeEvent", DateTime.Now, e1.Id);
                }


                db.Events.Remove(e);

                AddUpdate(db, "removeEvent", DateTime.Now, e.Id);

                db.SaveChanges();
            }
        }

        public static void RemoveRefereeFromMatch(int refereeId, int matchId)
        {
            using (var db = new FloorballEntities())
            {
                var referee = db.Referees.Where(r => r.Id == refereeId).First();
                var match = db.Matches.Where(m => m.Id == matchId).First();

                if (!(referee.Matches.Contains(match)))
                    throw new Exception("Referee cannot be removed from match!");


                match.Referees.Remove(referee);

                db.Matches.Attach(match);
                db.Entry(match).Property(e => e.Referees).IsModified = true;

                db.SaveChanges();

                AddUpdate(db, "removeRefereeFromMatch", DateTime.Now, refereeId, matchId);

            }
        }

        #endregion


        #region PUT

        private static void ChangeStatisticFromPlayer(int playerId, int teamId, string type, FloorballEntities db, string direction)
        {

            Statistic stat = db.Statistics.Where(s => s.PlayerRegNum == playerId && s.TeamId == teamId && s.Name == type).First();

            if (direction == "increase")
            {
                stat.Number++;
            }
            else
            {
                stat.Number--;
            }

            db.Statistics.Attach(stat);
            var entry = db.Entry(stat);
            entry.Property(e => e.Number).IsModified = true;

        }

        #endregion
        
    }
}
