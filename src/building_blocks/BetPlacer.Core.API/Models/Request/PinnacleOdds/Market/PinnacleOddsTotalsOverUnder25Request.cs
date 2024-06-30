using System.Text.Json.Serialization;

namespace BetPlacer.Core.API.Models.Request.PinnacleOdds.Market
{
    public class PinnacleOddsTotalsOverUnder25Request
    {
        [JsonPropertyName("over")]
        public double Over25Odd { get; set; }

        [JsonPropertyName("under")]
        public double Under25Odd { get; set; }
    }
}
