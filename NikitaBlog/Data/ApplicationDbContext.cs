using Microsoft.EntityFrameworkCore;
using NikitaBlog.Models;

namespace NikitaBlog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData
                (
                    new Category { Id = 1, Title = "Sport", DisplayOrder = 1 },
                    new Category { Id = 2, Title = "Hummor", DisplayOrder = 2 },
                    new Category { Id = 3, Title = "Science", DisplayOrder = 3 },
                    new Category { Id = 4, Title = "History", DisplayOrder = 4 }
                );
        }
    }
}
