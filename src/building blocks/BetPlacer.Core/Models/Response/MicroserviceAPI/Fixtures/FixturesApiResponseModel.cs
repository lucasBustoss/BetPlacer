using System.Text.Json.Serialization;

namespace BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures
{
    public class FixturesApiResponseModel
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
        
        [JsonPropertyName("leagueName")]
        public string LeagueName { get; set; }

        [JsonPropertyName("homeTeamName")]
        public string HomeTeamName { get; set; }

        [JsonPropertyName("awayTeamName")]
        public string AwayTeamName { get; set; }

        [JsonPropertyName("homeTeamCode")]
        public int HomeTeamCode { get; set; }

        [JsonPropertyName("awayTeamCode")]
        public int AwayTeamCode { get; set; }

        [JsonPropertyName("goals")]
        public List<FixtureGoalsApiResponseModel> Goals { get; set; }

        [JsonPropertyName("stats")]
        public FixtureStatsApiResponseModel Stats { get; set; }
    }
}
