namespace DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class League
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public League()
        {
            Matches = new HashSet<Match>();
            Teams = new HashSet<Team>();
        }

        public int Id { get; set; }

        public DateTime Year { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public LeagueTypeEnum Type { get; set; }

        [Required]
        public ClassEnum Class { get; set; }

        public short Rounds { get; set; }

        [Required]
        public CountriesEnum Country { get; set; }

        [Required]
        public GenderEnum Gender { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Match> Matches { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Team> Teams { get; set; }
    }
}
