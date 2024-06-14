using BetPlacer.Backtest.API.Models.Entities.Odds;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BetPlacer.Backtest.API.Models.ValueObjects;

namespace BetPlacer.Backtest.API.Models.Entities
{
    public class BacktestModel
    {
        public BacktestModel()
        {

        }

        public BacktestModel(BacktestVO backtest)
        {
            Name = backtest.Name;
            UserId = backtest.UserId;
            CreationDate = backtest.CreationDate;
            Type = backtest.Type;
            TeamType = backtest.TeamType;
            FilteredFixtures = backtest.FilteredFixtures;
            MatchedFixtures = backtest.MatchedFixtures;
            MaxGoodRun = backtest.MaxGoodRun;
            MaxBadRun = backtest.MaxBadRun;
            UsesInFixture = backtest.UsesInFixture;

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Type { get; set; }
        public int TeamType { get; set; }
        public double FilteredFixtures { get; set; }
        public double MatchedFixtures { get; set; }
        public int MaxGoodRun { get; set; }
        public int MaxBadRun { get; set; }
        public bool UsesInFixture { get; set; }
    }
}
