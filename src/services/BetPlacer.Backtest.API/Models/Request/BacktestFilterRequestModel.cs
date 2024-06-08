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

        [JsonPropertyName("firstToScorePercentHT")]
        public BacktestFilterValuesRequestModel FtsHTFilterModel { get; set; }

        [JsonPropertyName("twoZeroPercentHT")]
        public BacktestFilterValuesRequestModel ToScoreTwoZeroHTFilterModel { get; set; }

        [JsonPropertyName("cleanSheetPercentHT")]
        public BacktestFilterValuesRequestModel CleanSheetsHTFilterModel { get; set; }

        [JsonPropertyName("failedToScorePercentHT")]
        public BacktestFilterValuesRequestModel FailedToScoreHTFilterModel { get; set; }

        [JsonPropertyName("bothToScorePercentHT")]
        public BacktestFilterValuesRequestModel BothToScoreHTFilterModel { get; set; }

        [JsonPropertyName("avgGoalsScoredHT")]
        public BacktestFilterValuesRequestModel AverageGoalsScoredHTFilterModel { get; set; }

        [JsonPropertyName("avgGoalsConcededHT")]
        public BacktestFilterValuesRequestModel AverageGoalsConcededHTFilterModel { get; set; }
    }
}
