namespace DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Statistic
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public short Number { get; set; }

        public int PlayerRegNum { get; set; }

        public int TeamId { get; set; }

        public virtual Player Player { get; set; }

        public virtual Team Team { get; set; }
    }
}
