using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetPlacer.Backtest.API.Models.Entities.Filters
{
    [Table("filters")]
    public class FilterBacktestModel
    {
        [Key]
        public int Code { get; set; }
        public string Name { get; set; }
        public string Prop { get; set; }
    }
}
