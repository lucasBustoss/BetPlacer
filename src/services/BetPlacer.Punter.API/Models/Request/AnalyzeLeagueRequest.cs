using System.Text.Json.Serialization;

namespace BetPlacer.Punter.API.Models.Request
{
    public class AnalyzeLeagueRequest
    {
        [JsonPropertyName("leagueCode")]
        public int LeagueCode { get; set; }
    }
}
