namespace DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            Events = new HashSet<Event>();
            HomeMatches = new HashSet<Match>();
            AwayMatches = new HashSet<Match>();
            Statistics = new HashSet<Statistic>();
            Players = new HashSet<Player>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime Year { get; set; }

        [Required]
        public string Coach { get; set; }

        public short Points { get; set; }

        public short Standing { get; set; }

        public int TeamId { get; set; }

        public short MatchNumber { get; set; }

        [DefaultValue(0)]
        public short Scored { get; set; }

        public short Get { get; set; }

        public int StadiumId { get; set; }

        public int LeagueId { get; set; }

        [Required]
        public GenderEnum Gender { get; set; }

        public CountriesEnum Country { get; set; }

        [StringLength(50)]
        public string ImageName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }

        public virtual League League { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Match> HomeMatches { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Match> AwayMatches { get; set; }

        public virtual Stadium Stadium { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Statistic> Statistics { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Player> Players { get; set; }
    }
}
