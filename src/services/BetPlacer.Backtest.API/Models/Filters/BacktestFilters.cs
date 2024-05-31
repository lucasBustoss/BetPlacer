using BetPlacer.Backtest.API.Models.Enums;
using BetPlacer.Backtest.API.Models.Request;

namespace BetPlacer.Backtest.API.Models.Filters
{
    public class BacktestFilters
    {
        public BacktestFilters(BacktestFilterRequestModel backtestFilters)
        {
            FirstToScorePercentType = (FirstToScorePercent)backtestFilters.FirstToScorePercentFilterModel.Type;
            FirstToScorePercentInitial = backtestFilters.FirstToScorePercentFilterModel.InitialValue;
            FirstToScorePercentFinal = backtestFilters.FirstToScorePercentFilterModel.FinalValue;
        }

        public FirstToScorePercent FirstToScorePercentType { get; set; }
        public double FirstToScorePercentInitial { get; set; }
        public double FirstToScorePercentFinal { get; set; }
    }
}
