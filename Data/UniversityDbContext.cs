using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
        public class UniversityDbContext(DbContextOptions<UniversityDbContext> options) : DbContext(options)
        {
            public DbSet<Student> Student { get; set; }
            public DbSet<Group> GroupsStudent { get; set; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Student>()
                    .HasOne(s => s.Group)
                    .WithMany(g => g.Students)
                    .HasForeignKey(s => s.GroupId);
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog= UniversityContext; Integrated Security=True;");
            }

        }
    }
