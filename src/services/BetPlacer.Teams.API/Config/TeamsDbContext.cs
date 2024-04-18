using BetPlacer.Core.Helpers.Database;
using BetPlacer.Teams.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BetPlacer.Teams.Config
{
    public class TeamsDbContext : DbContext
    {
        public TeamsDbContext() { }

        public TeamsDbContext(DbContextOptions<TeamsDbContext> options) : base(options) { }

        public DbSet<TeamModel> Teams { get; set; }

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
