using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace lab04.Models
{
    public partial class BigSchoolDBContext : DbContext
    {
        public BigSchoolDBContext()
            : base("name=BigSchoolDBContext")
        {
        }

        public virtual DbSet<Attendee> Attendee { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Course> Course { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Course)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.CatogoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Attendee)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);
        }
    }
}
