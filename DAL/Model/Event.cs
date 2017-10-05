namespace DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Event
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        public TimeSpan Time { get; set; }

        public int MatchId { get; set; }

        public int PlayerRegNum { get; set; }

        public int EventMessageId { get; set; }

        public int TeamId { get; set; }

        public virtual EventMessage EventMessage { get; set; }

        public virtual Match Match { get; set; }

        public virtual Team Team { get; set; }

        public virtual Player Player { get; set; }
    }
}
