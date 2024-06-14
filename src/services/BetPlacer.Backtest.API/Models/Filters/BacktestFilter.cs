using BetPlacer.Backtest.API.Models.Request;

namespace BetPlacer.Backtest.API.Models.Filters
{
    public class BacktestFilter
    {
        public BacktestFilter()
        {
        }

        public BacktestFilter(BacktestFilterRequestModel filterRequest)
        {
            FilterCode = filterRequest.FilterCode;
            FilterName = filterRequest.FilterName;
            CompareType = filterRequest.CompareType;
            TeamType = filterRequest.TeamType;
            PropType = filterRequest.PropType;
            MinCountMatches = filterRequest.MinCountMatches;
            InitialValue = filterRequest.InitialValue ?? 0; 
            FinalValue = filterRequest.FinalValue ?? 0; 
            CalculateType = filterRequest.CalculateType;
            CalculateOperation = filterRequest.CalculateOperation ?? 0; 
            RelativeValue = filterRequest.RelativeValue ?? 1.0;
        }

        public int FilterCode { get; set; }
        public string FilterName { get; set; }
        public int CompareType { get; set; }
        public int TeamType { get; set; }
        public int PropType { get; set; }
        public int MinCountMatches { get; set; }
        public double InitialValue { get; set; }
        public double FinalValue { get; set; }
        public int CalculateType { get; set; }
        public int CalculateOperation { get; set; }
        public double RelativeValue { get; set; }
    }
}
