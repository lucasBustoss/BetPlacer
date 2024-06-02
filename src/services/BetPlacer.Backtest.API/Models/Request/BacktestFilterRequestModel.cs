using BetPlacer.Backtest.API.Models.Request.Filters;
using System.Text.Json.Serialization;

namespace BetPlacer.Backtest.API.Models.Request
{
    public class BacktestFilterRequestModel
    {
        [JsonPropertyName("firstToScorePercent")]
        public BacktestFilterValuesRequestModel FirstToScorePercentFilterModel { get; set; }

        [JsonPropertyName("twoZeroPercent")]
        public BacktestFilterValuesRequestModel TwoZeroPercentFilterModel { get; set; }

        [JsonPropertyName("cleanSheetPercent")]
        public BacktestFilterValuesRequestModel CleanSheetPercentFilterModel { get; set; }

        [JsonPropertyName("failedToScorePercent")]
        public BacktestFilterValuesRequestModel FailedToScorePercentFilterModel { get; set; }

        [JsonPropertyName("bothToScorePercent")]
        public BacktestFilterValuesRequestModel BothToScorePercentFilterModel { get; set; }

        [JsonPropertyName("avgGoalsScored")]
        public BacktestFilterValuesRequestModel AvgGoalsScoredFilterModel { get; set; }
        
        [JsonPropertyName("avgGoalsConceded")]
        public BacktestFilterValuesRequestModel AvgGoalsConcededFilterModel { get; set; }
    }
}
