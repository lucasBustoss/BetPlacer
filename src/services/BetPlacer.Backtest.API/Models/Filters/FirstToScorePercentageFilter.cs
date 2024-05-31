using BetPlacer.Backtest.API.Models.Enums;
using BetPlacer.Backtest.API.Models.Request.Filters;

namespace BetPlacer.Backtest.API.Models.Filters
{
    public class FirstToScorePercentageFilter
    {
        public FirstToScorePercentageFilter(BacktestFilterValuesRequestModel ftsValues)
        {
            Type = (FirstToScorePercent)ftsValues.Type;
            InitialValue = ftsValues.InitialValue;
            FinalValue = ftsValues.FinalValue;
        }

        public FirstToScorePercent Type { get; set; }
        public double InitialValue { get; set; }
        public double FinalValue { get; set; }
    }
}
