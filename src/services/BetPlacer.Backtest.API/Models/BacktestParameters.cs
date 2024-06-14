using BetPlacer.Backtest.API.Models.Enums;
using BetPlacer.Backtest.API.Models.Filters;
using BetPlacer.Backtest.API.Models.Request;

namespace BetPlacer.Backtest.API.Models
{
    public class BacktestParameters
    {
        public BacktestParameters(BacktestRequestModel backtestRequest)
        {
            Name = backtestRequest.Name;
            ResultType = (ResultType)backtestRequest.ResultType;
            ResultTeamType = (ResultTeamType)backtestRequest.ResultTeamType;

            if (backtestRequest.Filters != null && backtestRequest.Filters.Count > 0)
            {
                Filters = new List<BacktestFilter>();

                foreach (var filterRequest in backtestRequest.Filters)
                    Filters.Add(new BacktestFilter(filterRequest));
            }

        }

        public string Name { get; set; }
        public ResultType ResultType { get; set; }
        public ResultTeamType ResultTeamType { get; set; }
        public List<BacktestFilter> Filters { get; set; }
    }
}
