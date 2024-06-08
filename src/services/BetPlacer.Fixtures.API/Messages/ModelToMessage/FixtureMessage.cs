using BetPlacer.Core.Messages.PlanningBet.Core.Integration;
using BetPlacer.Fixtures.API.Models.ValueObjects;

namespace BetPlacer.Fixtures.API.Messages.ModelToMessage
{
    public class FixtureMessage: BaseMessage
    {
        public Fixture Fixture { get; set; }
    }
}
