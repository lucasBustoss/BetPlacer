using BetPlacer.Backtest.API.Models.Enums;
using BetPlacer.Backtest.API.Models.Request;

namespace BetPlacer.Backtest.API.Models.Filters
{
    public class BacktestFilters
    {
        public BacktestFilters(BacktestFilterRequestModel backtestFilters)
        {
            if (backtestFilters != null && backtestFilters.FirstToScorePercentFilterModel != null)
                FtsFilter = new FilterValue(backtestFilters.FirstToScorePercentFilterModel);

            if (backtestFilters != null && backtestFilters.TwoZeroPercentFilterModel != null)
                TwoZeroFilter = new FilterValue(backtestFilters.TwoZeroPercentFilterModel);

            if (backtestFilters != null && backtestFilters.CleanSheetPercentFilterModel != null)
                CleanSheetsFilter = new FilterValue(backtestFilters.CleanSheetPercentFilterModel);

            if (backtestFilters != null && backtestFilters.FailedToScorePercentFilterModel != null)
                FailedToScoreFilter = new FilterValue(backtestFilters.FailedToScorePercentFilterModel);

            if (backtestFilters != null && backtestFilters.BothToScorePercentFilterModel != null)
                BothToScoreFilter = new FilterValue(backtestFilters.BothToScorePercentFilterModel);

            if (backtestFilters != null && backtestFilters.AvgGoalsScoredFilterModel != null)
                AverageGoalsScoredFilter = new FilterValue(backtestFilters.AvgGoalsScoredFilterModel);

            if (backtestFilters != null && backtestFilters.AvgGoalsConcededFilterModel != null)
                AverageGoalsConcededFilter = new FilterValue(backtestFilters.AvgGoalsConcededFilterModel);
        }

        public FilterValue FtsFilter { get; set; }
        public FilterValue TwoZeroFilter { get; set; }
        public FilterValue CleanSheetsFilter { get; set; }
        public FilterValue FailedToScoreFilter { get; set; }
        public FilterValue BothToScoreFilter { get; set; }
        public FilterValue AverageGoalsScoredFilter { get; set; }
        public FilterValue AverageGoalsConcededFilter { get; set; }
    }
}
