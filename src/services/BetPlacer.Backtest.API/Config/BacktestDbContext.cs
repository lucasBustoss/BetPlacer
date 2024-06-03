using BetPlacer.Core.Helpers.Database;
using BetPlacer.Backtest.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BetPlacer.Backtest.API.Config
{
    public class BacktestDbContext : DbContext
    {
        public BacktestDbContext() { }

        public BacktestDbContext(DbContextOptions<BacktestDbContext> options) : base(options) { }

        public DbSet<BacktestModel> Backtest { get; set; }
        public DbSet<BacktestFilterModel> BacktestFilters { get; set; }
        public DbSet<LeagueBacktestModel> BacktestLeaguesList { get; set; }
        public DbSet<LeagueSeasonBacktestModel> BacktestLeagueSeasonsList { get; set; }
        public DbSet<TeamBacktestModel> BacktestTeamsList { get; set; }

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
