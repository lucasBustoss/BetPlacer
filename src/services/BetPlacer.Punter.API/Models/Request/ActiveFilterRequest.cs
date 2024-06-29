using System.Text.Json.Serialization;

namespace BetPlacer.Punter.API.Models.Request
{
    public class ActiveFilterRequest
    {
        [JsonPropertyName("strategyCode")]
        public int StrategyCode { get; set; }

        [JsonPropertyName("filterCode")]
        public int FilterCode { get; set; }
    }
}
