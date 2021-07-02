namespace lab04.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        public int id { get; set; }

        [Required]
        [StringLength(128)]
        public string LectureID { get; set; }

        [Required]
        [StringLength(255)]
        public string Place { get; set; }

        public DateTime Datetime { get; set; }

        public int CatogoryID { get; set; }

        public virtual Category Category { get; set; }
        public List<Category> ListCategory = new List<Category>();
        public string Name;
    }
}
