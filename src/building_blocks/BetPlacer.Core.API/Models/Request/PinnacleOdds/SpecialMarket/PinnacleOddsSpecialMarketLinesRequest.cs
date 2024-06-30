using System.Text.Json.Serialization;

namespace BetPlacer.Core.API.Models.Request.PinnacleOdds.SpecialMarket
{
    public class PinnacleOddsSpecialMarketLinesRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public double? Price { get; set; }
    }
}
