using BetPlacer.Core.Helpers.Database;
using BetPlacer.Fixtures.API.Models.Entities;
using BetPlacer.Fixtures.API.Models.Entities.Trade;
using Microsoft.EntityFrameworkCore;

namespace BetPlacer.Fixtures.Config
{
    public class FixturesDbContext : DbContext
    {
        public FixturesDbContext() { }

        public FixturesDbContext(DbContextOptions<FixturesDbContext> options) : base(options) { }

        public DbSet<FixtureModel> Fixtures { get; set; }
        public DbSet<FixtureGoalsModel> FixtureGoals { get; set; }
        public DbSet<FixtureStatsTradeModel> FixtureStatsTrade { get; set; }
        public DbSet<FixtureOdds> FixtureOdds { get; set; }

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
