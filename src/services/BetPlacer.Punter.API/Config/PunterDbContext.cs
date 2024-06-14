using BetPlacer.Punter.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BetPlacer.Punter.API.Config
{
    public class PunterDbContext : DbContext
    {
        public PunterDbContext() { }

        public PunterDbContext(DbContextOptions<PunterDbContext> options) : base(options) { }

        public DbSet<MatchBaseData> MatchBaseData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
