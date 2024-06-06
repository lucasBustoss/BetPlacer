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

            if (backtestFilters != null && backtestFilters.FtsHTFilterModel != null)
                FtsHTFilter = new FilterValue(backtestFilters.FtsHTFilterModel);

            if (backtestFilters != null && backtestFilters.ToScoreTwoZeroHTFilterModel != null)
                TwoZeroHTFilter = new FilterValue(backtestFilters.ToScoreTwoZeroHTFilterModel);

            if (backtestFilters != null && backtestFilters.CleanSheetsHTFilterModel != null)
                CleanSheetsHTFilter = new FilterValue(backtestFilters.AvgGoalsConcededFilterModel);

            if (backtestFilters != null && backtestFilters.FailedToScoreHTFilterModel != null)
                FailedToScoreHTFilter = new FilterValue(backtestFilters.FailedToScoreHTFilterModel);

            if (backtestFilters != null && backtestFilters.BothToScoreHTFilterModel != null)
                BothToScoreHTFilter = new FilterValue(backtestFilters.BothToScoreHTFilterModel);

            if (backtestFilters != null && backtestFilters.AverageGoalsScoredHTFilterModel != null)
                AverageGoalsScoredHTFilter = new FilterValue(backtestFilters.AverageGoalsScoredHTFilterModel);

            if (backtestFilters != null && backtestFilters.AverageGoalsConcededHTFilterModel != null)
                AverageGoalsConcededHTFilter = new FilterValue(backtestFilters.AverageGoalsConcededHTFilterModel);
        }

        public FilterValue FtsFilter { get; set; }
        public FilterValue TwoZeroFilter { get; set; }
        public FilterValue CleanSheetsFilter { get; set; }
        public FilterValue FailedToScoreFilter { get; set; }
        public FilterValue BothToScoreFilter { get; set; }
        public FilterValue AverageGoalsScoredFilter { get; set; }
        public FilterValue AverageGoalsConcededFilter { get; set; }
        public FilterValue FtsHTFilter { get; set; }
        public FilterValue TwoZeroHTFilter { get; set; }
        public FilterValue CleanSheetsHTFilter { get; set; }
        public FilterValue FailedToScoreHTFilter { get; set; }
        public FilterValue BothToScoreHTFilter { get; set; }
        public FilterValue AverageGoalsScoredHTFilter { get; set; }
        public FilterValue AverageGoalsConcededHTFilter { get; set; }
    }
}
