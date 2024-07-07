using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetPlacer.Fixtures.API.Models.Entities
{
    public class FixtureGoalsModel
    {
        public FixtureGoalsModel() { }

        public FixtureGoalsModel(int fixtureCode, string goalMinute, int teamId)
        {
            FixtureCode = fixtureCode;
            Minute = goalMinute;
            TeamId = teamId;
        }

        public FixtureModel Fixture { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public int FixtureCode { get; set; }
        public string Minute { get; set; }
        public int TeamId { get; set; }
    }
}
