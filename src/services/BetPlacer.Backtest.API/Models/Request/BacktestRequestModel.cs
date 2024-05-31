using System.Text.Json.Serialization;

namespace BetPlacer.Backtest.API.Models.Request
{
    public class BacktestRequestModel
    {
        [JsonPropertyName("resultType")]
        public int ResultType { get; set; }
        
        [JsonPropertyName("resultTeamType")]
        public int ResultTeamType { get; set; }

        [JsonPropertyName("filters")]
        public BacktestFilterRequestModel Filters { get;set; }
    }
}

