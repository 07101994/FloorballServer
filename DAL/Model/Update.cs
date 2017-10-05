namespace DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Update
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        public DateTime date { get; set; }

        public int data1 { get; set; }

        public int data2 { get; set; }

        [Required]
        [StringLength(10)]
        public string updatetype { get; set; }
    }
}
