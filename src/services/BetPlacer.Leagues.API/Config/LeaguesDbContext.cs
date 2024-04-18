using BetPlacer.Core.Helpers.Database;
using BetPlacer.Leagues.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BetPlacer.Leagues.Config
{
    public class LeaguesDbContext : DbContext
    {
        public LeaguesDbContext() { }

        public LeaguesDbContext(DbContextOptions<LeaguesDbContext> options) : base(options) { }

        public DbSet<LeagueModel> Leagues { get; set; }
        public DbSet<LeagueSeasonModel> LeagueSeasons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                    property.IsNullable = false;
            }

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                foreach (var property in entity.GetProperties())
                    property.SetColumnName(property.GetColumnName().ToSnakeCase());

                foreach (var key in entity.GetKeys())
                    key.SetName(key.GetName().ToSnakeCase());

                foreach (var foreignKey in entity.GetForeignKeys())
                    foreignKey.SetConstraintName(foreignKey.GetConstraintName().ToSnakeCase());
            }
        }
    }
}
