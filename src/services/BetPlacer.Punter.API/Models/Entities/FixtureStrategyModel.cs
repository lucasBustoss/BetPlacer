using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Punter.API.Models.Entities
{
    public class FixtureStrategyModel
    {
        public FixtureStrategyModel()
        {
            
        }

        public FixtureStrategyModel(int fixtureCode, string strategyName)
        {
            FixtureCode = fixtureCode;
            StrategyName = strategyName;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Code { get; set; }
        public int FixtureCode { get; set; }
        public string StrategyName { get; set; }
    }
}
