using BetPlacer.Core.Messages.PlanningBet.Core.Integration;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BetPlacer.Backtest.API.Messages
{
    public class FixtureMessage : BaseMessage
    {
        [JsonPropertyName("fixture")]
        public FixturesApiResponseModel Fixture { get; set; }
    }
}
