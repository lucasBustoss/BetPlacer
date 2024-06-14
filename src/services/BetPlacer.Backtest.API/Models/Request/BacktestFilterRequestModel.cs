using System.Text.Json.Serialization;

namespace BetPlacer.Backtest.API.Models.Request
{
    public class BacktestFilterRequestModel
    {
        [JsonPropertyName("filterCode")]
        public int FilterCode { get; set; }

        [JsonPropertyName("filterName")]
        public string FilterName { get; set; }

        [JsonPropertyName("compareType")]
        public int CompareType { get; set; }

        [JsonPropertyName("teamType")]
        public int TeamType { get; set; }

        [JsonPropertyName("propType")]
        public int PropType { get; set; }

        [JsonPropertyName("minCountMatches")]
        public int MinCountMatches { get; set; }

        [JsonPropertyName("minCountMatchesAtAway")]
        public int MinCountMatchesAtAway { get; set; }

        [JsonPropertyName("initialValue")]
        public double? InitialValue { get; set; }

        [JsonPropertyName("finalValue")]
        public double? FinalValue { get; set; }

        [JsonPropertyName("calculateType")]
        public int CalculateType { get; set; }

        [JsonPropertyName("calculateOperation")]
        public int? CalculateOperation { get; set; }

        [JsonPropertyName("relativeValue")]
        public double? RelativeValue { get; set; }
    }
}