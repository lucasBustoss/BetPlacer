using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BetPlacer.Backtest.API.Models.Filters;

namespace BetPlacer.Backtest.API.Models.Entities
{
    public class BacktestFilterModel
    {
        public BacktestFilterModel()
        {
            
        }

        public BacktestFilterModel(int backtestCode, BacktestFilter filter)
        {
            BacktestCode = backtestCode;
            FilterName = filter.FilterName;
            FilterCode = filter.FilterCode;
            CompareType = filter.CompareType;
            TeamType = filter.TeamType;
            PropType = filter.PropType;
            InitialValue = filter.InitialValue;
            FinalValue = filter.FinalValue;
        }

        public BacktestModel Backtest { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public int BacktestCode { get; set; }
        public int FilterCode { get; set; }
        public string FilterName { get; set; }
        public int CompareType { get; set; }
        public int TeamType { get; set; }
        public int PropType { get; set; }
        public double InitialValue { get; set; }
        public double FinalValue { get; set; }
    }
}
