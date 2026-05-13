using Microsoft.EntityFrameworkCore;
using MVC_Assignment01.Models;

namespace MVC_Assignment01.DbContexts
{
    public class GYMDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer("Server=.;Database=EventHubDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<Plan> Plans { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Plan>(new Configurations.PlanConfigurations());

        }
    }
}
