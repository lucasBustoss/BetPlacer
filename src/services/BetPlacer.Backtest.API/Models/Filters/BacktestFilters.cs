using BetPlacer.Backtest.API.Models.Enums;
using BetPlacer.Backtest.API.Models.Request;

namespace BetPlacer.Backtest.API.Models.Filters
{
    public class BacktestFilters
    {
        public BacktestFilters(BacktestFilterRequestModel backtestFilters)
        {
            if (backtestFilters != null && backtestFilters.FirstToScorePercentFilterModel != null)
                ftsFilter = new FirstToScorePercentageFilter(backtestFilters.FirstToScorePercentFilterModel);
        }

        public FirstToScorePercentageFilter ftsFilter { get; set; }
    }
}
