using BetPlacer.Backtest.API.Models.Request.Filters;
using System.Text.Json.Serialization;

namespace BetPlacer.Backtest.API.Models.Request
{
    public class BacktestFilterRequestModel
    {
        [JsonPropertyName("firstToScorePercent")]
        public BacktestFilterValuesRequestModel FirstToScorePercentFilterModel { get; set; }
    }
}
