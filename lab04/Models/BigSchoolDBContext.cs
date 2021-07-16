using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace lab04.Models
{
    public partial class BigschoolDBContext : DbContext
    {
        public BigschoolDBContext()
            : base("name=BigschoolDBContext1")
        {
        }

        public virtual DbSet<Attendee> Attendee { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Following> Following { get; set; }

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
