using System.Text.Json.Serialization;

namespace BetPlacer.Core.API.Models.Request.PinnacleOdds.Market
{
    public class PinnacleOddsMoneyLineRequest
    {
        [JsonPropertyName("home")]
        public double HomeOdd { get; set; }

        [JsonPropertyName("draw")]
        public double DrawOdd { get; set; }

        [JsonPropertyName("away")]
        public double AwayOdd { get; set; }
    }
}
