using BetPlacer.Backtest.API.Models.Enums;
using BetPlacer.Backtest.API.Models.Request.Filters;

namespace BetPlacer.Backtest.API.Models.Filters
{
    public class FilterValue
    {
        public FilterValue(BacktestFilterValuesRequestModel values)
        {
            CompareType = (FilterCompareType)values.CompareType;
            TeamType = (FilterTeamType)values.TeamType;
            PropType = (FilterPropType)values.PropType;
            InitialValue = values.InitialValue;
            FinalValue = values.FinalValue;
        }

        public FilterCompareType CompareType { get; set; }
        public FilterTeamType TeamType { get; set; }
        public FilterPropType PropType { get; set; }
        public double InitialValue { get; set; }
        public double FinalValue { get; set; }
    }
}
