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

        [JsonPropertyName("firstToScoreHTPercent")]
        public BacktestFilterValuesRequestModel FtsHTFilterModel { get; set; }

        [JsonPropertyName("twoZeroHTPercent")]
        public BacktestFilterValuesRequestModel ToScoreTwoZeroHTFilterModel { get; set; }

        [JsonPropertyName("cleanSheetHTPercent")]
        public BacktestFilterValuesRequestModel CleanSheetsHTFilterModel { get; set; }

        [JsonPropertyName("failedToScoreHTPercent")]
        public BacktestFilterValuesRequestModel FailedToScoreHTFilterModel { get; set; }

        [JsonPropertyName("bothToScoreHTPercent")]
        public BacktestFilterValuesRequestModel BothToScoreHTFilterModel { get; set; }

        [JsonPropertyName("avgGoalsHTScored")]
        public BacktestFilterValuesRequestModel AverageGoalsScoredHTFilterModel { get; set; }

        [JsonPropertyName("avgGoalsHTConceded")]
        public BacktestFilterValuesRequestModel AverageGoalsConcededHTFilterModel { get; set; }
    }
}
