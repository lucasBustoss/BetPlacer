using BetPlacer.Core.Helpers.Database;
using BetPlacer.Scheduler.Worker.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BetPlacer.Scheduler.Worker.Config
{
    public class SchedulerDbContext : DbContext
    {
        public SchedulerDbContext() { }

        public SchedulerDbContext(DbContextOptions<SchedulerDbContext> options) : base(options) { }

        public DbSet<SchedulerModel> SchedulerExecution { get; set; }

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
