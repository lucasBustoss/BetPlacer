using System.Text.Json.Serialization;

namespace BetPlacer.Core.API.Models.Request.PinnacleOdds.SpecialMarket
{
    public class PinnacleOddsSpecialMarketRequest
    {
        [JsonPropertyName("specials")]  
        public List<PinnacleOddsSpecialMarketMarketsRequest> Markets { get; set; }
    }
}
