using System.Text.Json.Serialization;

namespace BetPlacer.Backtest.API.Models
{
    public class BacktestFixture
    {
        [JsonPropertyName("fixtureCode")]
        public int FixtureCode { get; set; }
        
        [JsonPropertyName("backtestName")]
        public string BacktestName { get; set; }
        
        [JsonPropertyName("backtestCode")]
        public int BacktestCode { get; set; }
    }
}
