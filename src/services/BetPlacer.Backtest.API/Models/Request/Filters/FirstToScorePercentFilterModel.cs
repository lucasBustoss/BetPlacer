using System.Text.Json.Serialization;

namespace BetPlacer.Backtest.API.Models.Request.Filters
{
    public class BacktestFilterValuesRequestModel
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }
        
        [JsonPropertyName("initialValue")]
        public double InitialValue { get; set; }
        
        [JsonPropertyName("finalValue")]
        public double FinalValue { get; set; }
    }
}
