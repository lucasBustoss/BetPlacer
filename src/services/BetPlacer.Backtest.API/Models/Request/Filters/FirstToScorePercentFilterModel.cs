using System.Text.Json.Serialization;

namespace BetPlacer.Backtest.API.Models.Request.Filters
{
    public class BacktestFilterValuesRequestModel
    {
        [JsonPropertyName("compareType")]
        public int CompareType { get; set; }

        [JsonPropertyName("teamType")]
        public int TeamType { get; set; }

        [JsonPropertyName("propType")]
        public int PropType { get; set; }

        [JsonPropertyName("initialValue")]
        public double InitialValue { get; set; }
        
        [JsonPropertyName("finalValue")]
        public double FinalValue { get; set; }
    }
}
