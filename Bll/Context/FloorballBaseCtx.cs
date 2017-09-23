namespace DAL.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using global::Ninject;
    using Bll.Seed;

    public abstract class FloorballBaseCtx : DbContext
    {
       
        [Inject]
        public IDatabaseInitializer<FloorballBaseCtx> DbInitializer { get; set; }

        public FloorballBaseCtx(string ctx)
            : base(ctx)
        {
            Database.SetInitializer(DbInitializer);
        }

        public virtual DbSet<EventMessage> EventMessages { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<League> Leagues { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Referee> Referees { get; set; }
        public virtual DbSet<Stadium> Stadiums { get; set; }
        public virtual DbSet<Statistic> Statistics { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Update> Updates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventMessage>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.EventMessage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<League>()
                .HasMany(e => e.Matches)
                .WithRequired(e => e.League)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<League>()
                .HasMany(e => e.Teams)
                .WithRequired(e => e.League)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Match>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.Match)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Match>()
                .HasMany(e => e.Players)
                .WithMany(e => e.Matches)
                .Map(m => m.ToTable("PlayerMatch").MapLeftKey("Matches_Id").MapRightKey("Players_RegNum"));

            modelBuilder.Entity<Match>()
                .HasMany(e => e.Referees)
                .WithMany(e => e.Matches)
                .Map(m => m.ToTable("RefereeMatch").MapLeftKey("Matches_Id").MapRightKey("Referees_Id"));

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.Player)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Teams)
                .WithMany(e => e.Players)
                .Map(m => m.ToTable("PlayerTeam").MapLeftKey("Players_RegNum").MapRightKey("Teams_Id"));

            modelBuilder.Entity<Stadium>()
                .HasMany(e => e.Matches)
                .WithRequired(e => e.Stadium)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stadium>()
                .HasMany(e => e.Teams)
                .WithRequired(e => e.Stadium)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.Team)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.Matches)
                .WithRequired(e => e.HomeTeam)
                .HasForeignKey(e => e.HomeTeamId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.Matches1)
                .WithRequired(e => e.AwayTeam)
                .HasForeignKey(e => e.AwayTeamId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.Statistics)
                .WithRequired(e => e.Team)
                .WillCascadeOnDelete(false);
        }
    }
}
