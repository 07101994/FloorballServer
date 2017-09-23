namespace Bll.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        Time = c.Time(nullable: false, precision: 7),
                        MatchId = c.Int(nullable: false),
                        PlayerRegNum = c.Int(nullable: false),
                        EventMessageId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .ForeignKey("dbo.Players", t => t.PlayerRegNum)
                .ForeignKey("dbo.Matches", t => t.MatchId)
                .ForeignKey("dbo.EventMessages", t => t.EventMessageId)
                .Index(t => t.MatchId)
                .Index(t => t.PlayerRegNum)
                .Index(t => t.EventMessageId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Round = c.Short(nullable: false),
                        State = c.String(nullable: false),
                        GoalsH = c.Short(nullable: false),
                        GoalsA = c.Short(nullable: false),
                        Time = c.Time(nullable: false, precision: 7),
                        HomeTeamId = c.Int(nullable: false),
                        AwayTeamId = c.Int(nullable: false),
                        LeagueId = c.Int(nullable: false),
                        StadiumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leagues", t => t.LeagueId)
                .ForeignKey("dbo.Teams", t => t.HomeTeamId)
                .ForeignKey("dbo.Teams", t => t.AwayTeamId)
                .ForeignKey("dbo.Stadiums", t => t.StadiumId)
                .Index(t => t.HomeTeamId)
                .Index(t => t.AwayTeamId)
                .Index(t => t.LeagueId)
                .Index(t => t.StadiumId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Year = c.DateTime(nullable: false),
                        Coach = c.String(nullable: false),
                        Points = c.Short(nullable: false),
                        Standing = c.Short(nullable: false),
                        TeamId = c.Int(nullable: false),
                        Match = c.Short(nullable: false),
                        Scored = c.Short(nullable: false),
                        Get = c.Short(nullable: false),
                        StadiumId = c.Int(nullable: false),
                        LeagueId = c.Int(nullable: false),
                        Sex = c.String(nullable: false, maxLength: 50),
                        Country = c.String(maxLength: 10),
                        ImageName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leagues", t => t.LeagueId)
                .ForeignKey("dbo.Stadiums", t => t.StadiumId)
                .Index(t => t.StadiumId)
                .Index(t => t.LeagueId);
            
            CreateTable(
                "dbo.Leagues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.DateTime(nullable: false),
                        Name = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        ClassName = c.String(nullable: false),
                        Rounds = c.Int(nullable: false),
                        Country = c.String(nullable: false),
                        Sex = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        RegNum = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        Number = c.Short(nullable: false),
                        Date = c.DateTime(nullable: false),
                        SecondName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RegNum);
            
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Number = c.Short(nullable: false),
                        PlayerRegNum = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerRegNum, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .Index(t => t.PlayerRegNum)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Stadiums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Referees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Number = c.Short(nullable: false),
                        Penalty = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Updates",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        date = c.DateTime(nullable: false),
                        data1 = c.Int(nullable: false),
                        data2 = c.Int(nullable: false),
                        updatetype = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.PlayerTeam",
                c => new
                    {
                        Players_RegNum = c.Int(nullable: false),
                        Teams_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Players_RegNum, t.Teams_Id })
                .ForeignKey("dbo.Players", t => t.Players_RegNum, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Teams_Id, cascadeDelete: true)
                .Index(t => t.Players_RegNum)
                .Index(t => t.Teams_Id);
            
            CreateTable(
                "dbo.PlayerMatch",
                c => new
                    {
                        Matches_Id = c.Int(nullable: false),
                        Players_RegNum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Matches_Id, t.Players_RegNum })
                .ForeignKey("dbo.Matches", t => t.Matches_Id, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.Players_RegNum, cascadeDelete: true)
                .Index(t => t.Matches_Id)
                .Index(t => t.Players_RegNum);
            
            CreateTable(
                "dbo.RefereeMatch",
                c => new
                    {
                        Matches_Id = c.Int(nullable: false),
                        Referees_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Matches_Id, t.Referees_Id })
                .ForeignKey("dbo.Matches", t => t.Matches_Id, cascadeDelete: true)
                .ForeignKey("dbo.Referees", t => t.Referees_Id, cascadeDelete: true)
                .Index(t => t.Matches_Id)
                .Index(t => t.Referees_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "EventMessageId", "dbo.EventMessages");
            DropForeignKey("dbo.RefereeMatch", "Referees_Id", "dbo.Referees");
            DropForeignKey("dbo.RefereeMatch", "Matches_Id", "dbo.Matches");
            DropForeignKey("dbo.PlayerMatch", "Players_RegNum", "dbo.Players");
            DropForeignKey("dbo.PlayerMatch", "Matches_Id", "dbo.Matches");
            DropForeignKey("dbo.Events", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.Statistics", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "StadiumId", "dbo.Stadiums");
            DropForeignKey("dbo.Matches", "StadiumId", "dbo.Stadiums");
            DropForeignKey("dbo.PlayerTeam", "Teams_Id", "dbo.Teams");
            DropForeignKey("dbo.PlayerTeam", "Players_RegNum", "dbo.Players");
            DropForeignKey("dbo.Statistics", "PlayerRegNum", "dbo.Players");
            DropForeignKey("dbo.Events", "PlayerRegNum", "dbo.Players");
            DropForeignKey("dbo.Matches", "AwayTeamId", "dbo.Teams");
            DropForeignKey("dbo.Matches", "HomeTeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "LeagueId", "dbo.Leagues");
            DropForeignKey("dbo.Matches", "LeagueId", "dbo.Leagues");
            DropForeignKey("dbo.Events", "TeamId", "dbo.Teams");
            DropIndex("dbo.RefereeMatch", new[] { "Referees_Id" });
            DropIndex("dbo.RefereeMatch", new[] { "Matches_Id" });
            DropIndex("dbo.PlayerMatch", new[] { "Players_RegNum" });
            DropIndex("dbo.PlayerMatch", new[] { "Matches_Id" });
            DropIndex("dbo.PlayerTeam", new[] { "Teams_Id" });
            DropIndex("dbo.PlayerTeam", new[] { "Players_RegNum" });
            DropIndex("dbo.Statistics", new[] { "TeamId" });
            DropIndex("dbo.Statistics", new[] { "PlayerRegNum" });
            DropIndex("dbo.Teams", new[] { "LeagueId" });
            DropIndex("dbo.Teams", new[] { "StadiumId" });
            DropIndex("dbo.Matches", new[] { "StadiumId" });
            DropIndex("dbo.Matches", new[] { "LeagueId" });
            DropIndex("dbo.Matches", new[] { "AwayTeamId" });
            DropIndex("dbo.Matches", new[] { "HomeTeamId" });
            DropIndex("dbo.Events", new[] { "TeamId" });
            DropIndex("dbo.Events", new[] { "EventMessageId" });
            DropIndex("dbo.Events", new[] { "PlayerRegNum" });
            DropIndex("dbo.Events", new[] { "MatchId" });
            DropTable("dbo.RefereeMatch");
            DropTable("dbo.PlayerMatch");
            DropTable("dbo.PlayerTeam");
            DropTable("dbo.Updates");
            DropTable("dbo.Referees");
            DropTable("dbo.Stadiums");
            DropTable("dbo.Statistics");
            DropTable("dbo.Players");
            DropTable("dbo.Leagues");
            DropTable("dbo.Teams");
            DropTable("dbo.Matches");
            DropTable("dbo.Events");
            DropTable("dbo.EventMessages");
        }
    }
}
