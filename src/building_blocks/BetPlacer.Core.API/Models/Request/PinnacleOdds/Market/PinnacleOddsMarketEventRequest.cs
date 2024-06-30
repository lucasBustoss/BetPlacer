using System.Text.Json.Serialization;

namespace BetPlacer.Core.API.Models.Request.PinnacleOdds.Market
{
    public class PinnacleOddsMarketEventRequest
    {
        [JsonPropertyName("event_id")]
        public int Code { get; set; }

        [JsonPropertyName("league_name")]
        public string LeagueName { get; set; }

        [JsonPropertyName("starts")]
        public string Date { get; set; }

        [JsonPropertyName("home")]
        public string HomeTeam { get; set; }

        [JsonPropertyName("away")]
        public string AwayTeam { get; set; }

        [JsonPropertyName("periods")]
        public PinnacleOddsMarketPeriodRequest Odds { get; set; }
    }
}
