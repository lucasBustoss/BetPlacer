using System.Text.Json.Serialization;

namespace BetPlacer.Core.API.Models.Request.PinnacleOdds.Market
{
    public class PinnacleOddsMarketLinesRequest
    {
        [JsonPropertyName("money_line")]
        public PinnacleOddsMoneyLineRequest MoneyLine { get; set; }

        [JsonPropertyName("totals")]
        public PinnacleOddsTotalsRequest Totals { get; set; }
    }
}
