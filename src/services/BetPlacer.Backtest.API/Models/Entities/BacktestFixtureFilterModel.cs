using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Backtest.API.Models.Entities
{
    public class BacktestFixtureFilterModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public int BacktestCode { get; set; }
        public int FixtureCode { get; set; }
    }
}
