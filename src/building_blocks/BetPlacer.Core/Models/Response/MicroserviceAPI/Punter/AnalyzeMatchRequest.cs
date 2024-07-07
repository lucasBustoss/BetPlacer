using System.Text.Json.Serialization;

namespace BetPlacer.Core.Models.Response.MicroserviceAPI.Punter
{
    public class AnalyzeMatchRequest
    {
        public AnalyzeMatchRequest()
        {
            
        }

        public AnalyzeMatchRequest(List<int> leagueCodes, string date)
        {
            LeagueCodes = leagueCodes;
            Date = date;
        }

        [JsonPropertyName("leagueCodes")]
        public List<int> LeagueCodes { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }
    }
}
