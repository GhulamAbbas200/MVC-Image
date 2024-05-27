using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication1.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<TBImg> TBImgs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TBImg>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<TBImg>()
                .Property(e => e.Image)
                .IsUnicode(false);
        }
    }
}
