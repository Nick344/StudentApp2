using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class UniversityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,int>
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
        { }
        public DbSet<Student> Student { get; set; }
        public DbSet<Group> GroupsStudent { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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



