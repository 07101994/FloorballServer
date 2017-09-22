namespace DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Match
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Match()
        {
            Events = new HashSet<Event>();
            Players = new HashSet<Player>();
            Referees = new HashSet<Referee>();
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public short Round { get; set; }

        [Required]
        public string State { get; set; }

        public short GoalsH { get; set; }

        public short GoalsA { get; set; }

        public TimeSpan Time { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int LeagueId { get; set; }

        public int StadiumId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }

        public virtual League League { get; set; }

        public virtual Stadium Stadium { get; set; }

        public virtual Team HomeTeam { get; set; }

        public virtual Team AwayTeam { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Player> Players { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Referee> Referees { get; set; }
    }
}
