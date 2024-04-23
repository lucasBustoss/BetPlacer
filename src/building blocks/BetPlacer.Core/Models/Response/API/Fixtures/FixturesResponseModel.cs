using System.Text.Json.Serialization;

namespace BetPlacer.Core.Models.Response.API.Fixtures
{
    public class FixturesResponseModel
    {
        [JsonPropertyName("id")]
        public int Code { get; set; }

        [JsonPropertyName("competition_id")]
        public int LeagueSeasonCode { get; set; }

        [JsonPropertyName("date_unix")]
        public int DateTimestamp { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("homeID")]
        public int HomeTeamId { get; set; }

        [JsonPropertyName("awayID")]
        public int AwayTeamId { get; set; }

        [JsonPropertyName("home_name")]
        public string HomeTeamName { get; set; }

        [JsonPropertyName("away_name")]
        public string AwayTeamName { get; set; }

        [JsonPropertyName("home_image")]
        public string HomeTeamImage { get; set; }

        [JsonPropertyName("away_image")]
        public string AwayTeamImage { get; set; }

        [JsonPropertyName("homeGoalCount")]
        public int HomeGoals { get; set; }

        [JsonPropertyName("awayGoalCount")]
        public int AwayGoals { get; set; }

        [JsonPropertyName("home_ppg")]
        public double HomePointsPerGame { get; set; }

        [JsonPropertyName("away_ppg")]
        public double AwayPointsPerGame { get; set; }

        [JsonPropertyName("homeGoals")]
        public string[] HomeGoalsMinutes { get; set; }

        [JsonPropertyName("awayGoals")]
        public string[] AwayGoalsMinutes { get; set; }

        [JsonPropertyName("team_a_xg")]
        public double HomeTeamXG { get; set; }

        [JsonPropertyName("team_b_xg")]
        public double AwayTeamXG { get; set;}
    }                                                                                                                                                                                                                                   
}
