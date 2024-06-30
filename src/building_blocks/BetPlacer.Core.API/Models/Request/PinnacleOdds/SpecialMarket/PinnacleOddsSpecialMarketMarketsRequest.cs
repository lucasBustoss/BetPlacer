using System.Text.Json.Serialization;

namespace BetPlacer.Core.API.Models.Request.PinnacleOdds.SpecialMarket
{
    public class PinnacleOddsSpecialMarketMarketsRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("lines")]
        public Dictionary<string, PinnacleOddsSpecialMarketLinesRequest> Lines { get; set; }
    }
}
