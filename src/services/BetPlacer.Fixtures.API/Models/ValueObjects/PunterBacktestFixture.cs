using System.Text.Json.Serialization;

namespace BetPlacer.Backtest.API.Models
{
    public class PunterBacktestFixture
    {
        [JsonPropertyName("fixtureCode")]
        public int FixtureCode { get; set; }
        
        [JsonPropertyName("strategyName")]
        public string StrategyName { get; set; }
    }
}
