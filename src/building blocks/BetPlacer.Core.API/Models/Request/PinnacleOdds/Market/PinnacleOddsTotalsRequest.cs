using System.Text.Json.Serialization;

namespace BetPlacer.Core.API.Models.Request.PinnacleOdds.Market
{
    public class PinnacleOddsTotalsRequest
    {
        [JsonPropertyName("2.5")]
        public PinnacleOddsTotalsOverUnder25Request OverUnder25 { get; set; }
    }
}
