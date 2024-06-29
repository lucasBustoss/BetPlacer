using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Punter.API.Models.Entities
{
    public class FixtureIntervalModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Code { get; set; }
        public int FixtureCode { get; set; }
        public string IntervalName { get; set; }
    }
}
