using BetPlacer.Core.Helpers.Database;
using BetPlacer.Punter.API.Models;
using BetPlacer.Punter.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BetPlacer.Punter.API.Config
{
    public class PunterDbContext : DbContext
    {
        public PunterDbContext() { }

        public PunterDbContext(DbContextOptions<PunterDbContext> options) : base(options) { }

        public DbSet<MatchBaseData> MatchBaseData { get; set; }
        public DbSet<NextMatch> NextMatch { get; set; }
        public DbSet<PunterBacktestModel> PunterBacktest { get; set; }
        public DbSet<PunterBacktestClassificationModel> PunterBacktestClassification { get; set; }
        public DbSet<PunterBacktestIntervalModel> PunterBacktestInterval { get; set; }
        public DbSet<PunterBacktestCombinedIntervalModel> PunterBacktestCombinedInterval { get; set; }
        public DbSet<FixtureStrategyModel> FixtureStrategy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Aplica apenas para as últimas 4 entidades do DbContext
                if (entityType.ClrType == typeof(PunterBacktestModel) ||
                    entityType.ClrType == typeof(PunterBacktestClassificationModel) ||
                    entityType.ClrType == typeof(PunterBacktestIntervalModel) ||
                    entityType.ClrType == typeof(PunterBacktestCombinedIntervalModel) ||
                    entityType.ClrType == typeof(FixtureStrategyModel))
                {
                    entityType.SetTableName(entityType.GetTableName().ToSnakeCase());

                    foreach (var property in entityType.GetProperties())
                    {
                        property.SetColumnName(property.GetColumnName().ToSnakeCase());
                        property.IsNullable = false; // Isso pode não ser necessário para todas as propriedades
                    }

                    foreach (var key in entityType.GetKeys())
                    {
                        key.SetName(key.GetName().ToSnakeCase());
                    }

                    foreach (var foreignKey in entityType.GetForeignKeys())
                    {
                        foreignKey.SetConstraintName(foreignKey.GetConstraintName().ToSnakeCase());
                    }
                }

                modelBuilder.Entity<FixtureStrategyModel>(entity =>
                {
                    entity.Property(e => e.StrategyName)
                        .IsRequired(false);
                });
            }
        }
    }
}
