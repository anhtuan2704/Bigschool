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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            Attendee = new HashSet<Attendee>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(128)]
        public string LectureID { get; set; }

        [Required]
        [StringLength(255)]
      

        public string Place { get; set; }

        public DateTime Datetime { get; set; }

        public int CatogoryID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendee> Attendee { get; set; }

        public virtual Category Category { get; set; }
        public List<Category> ListCategory = new List<Category>();
        public String Name;
        public string LectureName;
    }
}
