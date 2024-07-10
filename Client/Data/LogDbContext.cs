using Client.Model;
using Client.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Client.Data
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options)
            : base(options)
        {
        }
        //public DbSet<House> Houses { get; set; }
        public DbSet<CleaningLog> houseCleaningLogs { get; set; }
        public DbSet<LandscapingLogs> landscapingLogs { get; set; }
        public DbSet<SnowLogs> snowLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<House>(entity =>
            //{

            //    entity.Property(e => e.Rent)
            //        .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed
            //});

            // Other entity configurations
        }
    }

}
