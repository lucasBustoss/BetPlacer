using System.Text.Json.Serialization;

namespace BetPlacer.Punter.API.Models.Request
{
    public class AnalyzeMatchRequest
    {
        [JsonPropertyName("leagueCodes")]
        public List<int> LeagueCodes { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }
    }
}
