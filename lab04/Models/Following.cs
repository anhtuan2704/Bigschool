namespace lab04.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Following")]
    public partial class Following
    {
        [Key]
        [Column(Order = 0)]
        public string FllowerID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string FlloweeID { get; set; }
    }
}
