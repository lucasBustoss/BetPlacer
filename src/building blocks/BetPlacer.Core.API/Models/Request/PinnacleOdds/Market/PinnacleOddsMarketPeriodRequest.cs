using System.Text.Json.Serialization;

namespace BetPlacer.Core.API.Models.Request.PinnacleOdds.Market
{
    public class PinnacleOddsMarketPeriodRequest
    {
        [JsonPropertyName("num_0")]
        public PinnacleOddsMarketLinesRequest FTOdds { get; set; }
    }
}
