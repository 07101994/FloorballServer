namespace DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Update
    {
        public int Id { get; set; }

        [Required]
        public UpdateEnum Name { get; set; }

        public DateTime Date { get; set; }

        public int Data1 { get; set; }

        public int Data2 { get; set; }

        [Required]
        [StringLength(10)]
        public UpdateType Updatetype { get; set; }
    }
}
