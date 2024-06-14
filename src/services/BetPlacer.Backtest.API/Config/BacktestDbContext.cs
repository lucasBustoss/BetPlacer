using BetPlacer.Core.Helpers.Database;
using BetPlacer.Backtest.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using BetPlacer.Backtest.API.Models.Entities.Filters;

namespace BetPlacer.Backtest.API.Config
{
    public class BacktestDbContext : DbContext
    {
        public BacktestDbContext() { }

        public BacktestDbContext(DbContextOptions<BacktestDbContext> options) : base(options) { }

        public DbSet<BacktestModel> Backtest { get; set; }
        public DbSet<BacktestFilterModel> BacktestFilters { get; set; }
        public DbSet<BacktestAdditionalInformationModel> BacktestAdditionalInformation { get; set; }
        public DbSet<LeagueBacktestModel> BacktestLeaguesList { get; set; }
        public DbSet<LeagueSeasonBacktestModel> BacktestLeagueSeasonsList { get; set; }
        public DbSet<TeamBacktestModel> BacktestTeamsList { get; set; }
        public DbSet<BacktestFixtureFilterModel> BacktestFixtureFilter { get; set; }
        public DbSet<FilterBacktestModel> Filter { get; set; }

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

            modelBuilder.Entity<FilterBacktestModel>().HasData(
                new FilterBacktestModel { Code = 1, Name = "% de jogos sendo primeiro a marcar", Prop = "firstToScorePercent" },
                new FilterBacktestModel { Code = 2, Name = "% de jogos sendo primeiro a marcar 2x0", Prop = "twoZeroPercent" },
                new FilterBacktestModel { Code = 3, Name = "% de jogos sem sofrer gols", Prop = "cleanSheetPercent" },
                new FilterBacktestModel { Code = 4, Name = "% de jogos em que não marcou gols", Prop = "failedToScorePercent" },
                new FilterBacktestModel { Code = 5, Name = "% de jogos em que os dois times marcaram", Prop = "bothToScorePercent" },
                new FilterBacktestModel { Code = 6, Name = "Média de gols marcados", Prop = "avgGoalsScored" },
                new FilterBacktestModel { Code = 7, Name = "Média de gols sofridos", Prop = "avgGoalsConceded" },
                new FilterBacktestModel { Code = 8, Name = "% de jogos sendo primeiro a marcar no HT", Prop = "firstToScorePercentHT" },
                new FilterBacktestModel { Code = 9, Name = "% de jogos sendo primeiro a marcar 2x0 no HT", Prop = "twoZeroPercentHT" },
                new FilterBacktestModel { Code = 10, Name = "% de jogos sem sofrer gols no HT", Prop = "cleanSheetPercentHT" },
                new FilterBacktestModel { Code = 11, Name = "% de jogos em que não marcou gols no HT", Prop = "failedToScorePercentHT" },
                new FilterBacktestModel { Code = 12, Name = "% de jogos em que os dois times marcaram no HT", Prop = "bothToScorePercentHT" },
                new FilterBacktestModel { Code = 13, Name = "Média de gols marcados no HT", Prop = "avgGoalsScoredHT" },
                new FilterBacktestModel { Code = 14, Name = "Média de gols sofridos no HT", Prop = "avgGoalsConcededHT" }
            );
        }
    }
}
