using Client.Areas.Identity.Data;
using Client.Model;
using Client.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Client.Data
{
    public class LogDbContext : IdentityDbContext<ClientUser>
    {
        public LogDbContext(DbContextOptions<LogDbContext> options)
            : base(options)
        {
        }
        //public DbSet<House> Houses { get; set; }
        public DbSet<CleaningLog> houseCleaningLogs { get; set; }
        public DbSet<LandscapingLogs> landscapingLogs { get; set; }
        public DbSet<SnowLogs> snowLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
      
        }
    }

}
