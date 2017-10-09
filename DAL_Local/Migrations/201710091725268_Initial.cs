namespace DAL_Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Secret = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        ApplicationType = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                        AllowedOrigin = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Type = c.Int(nullable: false),
                        Time = c.Time(nullable: false, precision: 7),
                        MatchId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        EventMessageId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .ForeignKey("dbo.Players", t => t.PlayerId)
                .ForeignKey("dbo.Matches", t => t.MatchId)
                .ForeignKey("dbo.EventMessages", t => t.EventMessageId)
                .Index(t => t.MatchId)
                .Index(t => t.PlayerId)
                .Index(t => t.EventMessageId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Round = c.Short(nullable: false),
                        State = c.Int(nullable: false),
                        ScoreH = c.Short(nullable: false),
                        ScoreA = c.Short(nullable: false),
                        Time = c.Time(nullable: false, precision: 7),
                        HomeTeamId = c.Int(nullable: false),
                        AwayTeamId = c.Int(nullable: false),
                        LeagueId = c.Int(nullable: false),
                        StadiumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.AwayTeamId)
                .ForeignKey("dbo.Teams", t => t.HomeTeamId)
                .ForeignKey("dbo.Leagues", t => t.LeagueId)
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
                        MatchNumber = c.Short(nullable: false),
                        Scored = c.Short(nullable: false),
                        Get = c.Short(nullable: false),
                        StadiumId = c.Int(nullable: false),
                        LeagueId = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        Country = c.Int(nullable: false),
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
                        Type = c.Int(nullable: false),
                        Class = c.Int(nullable: false),
                        Rounds = c.Short(nullable: false),
                        Country = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        Number = c.Short(nullable: false),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Number = c.Short(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .Index(t => t.PlayerId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Stadiums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PostCode = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        City = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Referees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Country = c.Int(nullable: false),
                        Number = c.Short(nullable: false),
                        Penalty = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(nullable: false, maxLength: 50),
                        ClientId = c.String(nullable: false, maxLength: 50),
                        IssuedUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Updates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Data1 = c.Int(nullable: false),
                        Data2 = c.Int(nullable: false),
                        Updatetype = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PlayerTeam",
                c => new
                    {
                        Players_Id = c.Int(nullable: false),
                        Teams_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Players_Id, t.Teams_Id })
                .ForeignKey("dbo.Players", t => t.Players_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Teams_Id, cascadeDelete: true)
                .Index(t => t.Players_Id)
                .Index(t => t.Teams_Id);
            
            CreateTable(
                "dbo.PlayerMatch",
                c => new
                    {
                        Matches_Id = c.Int(nullable: false),
                        Players_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Matches_Id, t.Players_Id })
                .ForeignKey("dbo.Matches", t => t.Matches_Id, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.Players_Id, cascadeDelete: true)
                .Index(t => t.Matches_Id)
                .Index(t => t.Players_Id);
            
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
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Events", "EventMessageId", "dbo.EventMessages");
            DropForeignKey("dbo.RefereeMatch", "Referees_Id", "dbo.Referees");
            DropForeignKey("dbo.RefereeMatch", "Matches_Id", "dbo.Matches");
            DropForeignKey("dbo.PlayerMatch", "Players_Id", "dbo.Players");
            DropForeignKey("dbo.PlayerMatch", "Matches_Id", "dbo.Matches");
            DropForeignKey("dbo.Events", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.Statistics", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "StadiumId", "dbo.Stadiums");
            DropForeignKey("dbo.Matches", "StadiumId", "dbo.Stadiums");
            DropForeignKey("dbo.PlayerTeam", "Teams_Id", "dbo.Teams");
            DropForeignKey("dbo.PlayerTeam", "Players_Id", "dbo.Players");
            DropForeignKey("dbo.Statistics", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Events", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Teams", "LeagueId", "dbo.Leagues");
            DropForeignKey("dbo.Matches", "LeagueId", "dbo.Leagues");
            DropForeignKey("dbo.Matches", "HomeTeamId", "dbo.Teams");
            DropForeignKey("dbo.Events", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Matches", "AwayTeamId", "dbo.Teams");
            DropIndex("dbo.RefereeMatch", new[] { "Referees_Id" });
            DropIndex("dbo.RefereeMatch", new[] { "Matches_Id" });
            DropIndex("dbo.PlayerMatch", new[] { "Players_Id" });
            DropIndex("dbo.PlayerMatch", new[] { "Matches_Id" });
            DropIndex("dbo.PlayerTeam", new[] { "Teams_Id" });
            DropIndex("dbo.PlayerTeam", new[] { "Players_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Statistics", new[] { "TeamId" });
            DropIndex("dbo.Statistics", new[] { "PlayerId" });
            DropIndex("dbo.Teams", new[] { "LeagueId" });
            DropIndex("dbo.Teams", new[] { "StadiumId" });
            DropIndex("dbo.Matches", new[] { "StadiumId" });
            DropIndex("dbo.Matches", new[] { "LeagueId" });
            DropIndex("dbo.Matches", new[] { "AwayTeamId" });
            DropIndex("dbo.Matches", new[] { "HomeTeamId" });
            DropIndex("dbo.Events", new[] { "TeamId" });
            DropIndex("dbo.Events", new[] { "EventMessageId" });
            DropIndex("dbo.Events", new[] { "PlayerId" });
            DropIndex("dbo.Events", new[] { "MatchId" });
            DropTable("dbo.RefereeMatch");
            DropTable("dbo.PlayerMatch");
            DropTable("dbo.PlayerTeam");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Updates");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RefreshTokens");
            DropTable("dbo.Referees");
            DropTable("dbo.Stadiums");
            DropTable("dbo.Statistics");
            DropTable("dbo.Players");
            DropTable("dbo.Leagues");
            DropTable("dbo.Teams");
            DropTable("dbo.Matches");
            DropTable("dbo.Events");
            DropTable("dbo.EventMessages");
            DropTable("dbo.Clients");
        }
    }
}
