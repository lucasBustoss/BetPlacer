using System.Text.Json.Serialization;

namespace BetPlacer.Punter.API.Models.Request
{
    public class AnalyzeMatchRequest
    {
        [JsonPropertyName("leagueCode")]
        public int LeagueCode { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }
    }
}
