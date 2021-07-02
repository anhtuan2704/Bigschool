using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace lab04.Models
{
    public partial class BigschoolContext : DbContext
    {
        public BigschoolContext()
            : base("name=BigschoolContext1")
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Course> Course { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Course)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.CatogoryID)
                .WillCascadeOnDelete(false);
        }
    }
}
