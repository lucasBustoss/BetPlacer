using System.Text.Json.Serialization;

namespace BetPlacer.Core.API.Models.Request.PinnacleOdds.Market
{
    public class PinnacleOddsMarketRequest
    {
        [JsonPropertyName("events")]
        public List<PinnacleOddsMarketEventRequest> Events { get; set; }
    }
}
