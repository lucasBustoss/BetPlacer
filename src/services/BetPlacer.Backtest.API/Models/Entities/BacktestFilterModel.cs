using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Backtest.API.Models.Entities
{
    public class BacktestFilterModel
    {
        public BacktestModel Backtest { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public int BacktestCode { get; set; }
        public string Name { get; set; }
        public int CompareType { get; set; }
        public int TeamType { get; set; }
        public int PropType { get; set; }
        public double InitialValue { get; set; }
        public double FinalValue { get; set; }
    }
}
