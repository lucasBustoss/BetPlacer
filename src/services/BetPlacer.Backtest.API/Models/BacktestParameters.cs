using BetPlacer.Backtest.API.Models.Enums;
using BetPlacer.Backtest.API.Models.Filters;
using BetPlacer.Backtest.API.Models.Request;

namespace BetPlacer.Backtest.API.Models
{
    public class BacktestParameters
    {
        public BacktestParameters(BacktestRequestModel backtestRequest)
        {
            ResultType = (ResultType)backtestRequest.ResultType;
            ResultTeamType = (ResultTeamType)backtestRequest.ResultTeamType;
            Filters = new BacktestFilters(backtestRequest.Filters);
        }

        public ResultType ResultType { get; set; }
        public ResultTeamType ResultTeamType { get; set; }
        public BacktestFilters Filters { get; set; }
    }
}
