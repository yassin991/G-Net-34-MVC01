using GymSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Contexts
{
    public class GYMDbContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=EventHubDB;Trusted_Connection=True;TrustServerCertificate=True;");
        //}
        public GYMDbContext(DbContextOptions<GYMDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
       typeof(GYMDbContext).Assembly);

        }
        
        public DbSet<Plan> Plans { get; set; }
        public IQueryable<object> Members { get; internal set; }
    }
}
