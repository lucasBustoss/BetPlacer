using System.Text.Json.Serialization;

namespace BetPlacer.Core.Models.Response.API.Fixtures
{
    public class FixturesResponseModel
    {
        [JsonPropertyName("id")]
        public int Code { get; set; }

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

        /// <summary>
        /// PPG pré partida como mandante
        /// </summary>
        [JsonPropertyName("pre_match_home_ppg")]
        public double HomePreMatchPointsPerGame { get; set; }

        /// <summary>
        /// PPG pré partida como visitante
        /// </summary>
        [JsonPropertyName("pre_match_away_ppg")]
        public double AwayPreMatchPointsPerGame { get; set; }

        /// <summary>
        /// PPG pré partida total do mandante no campeonato
        /// </summary>
        [JsonPropertyName("pre_match_teamA_overall_ppg")]
        public double HomePreMatchOverallPointsPerGame { get; set; }

        /// <summary>
        /// PPG pré partida total do visitante no campeonato
        /// </summary>
        [JsonPropertyName("pre_match_teamB_overall_ppg")]
        public double AwayPreMatchOverallPointsPerGame { get; set; }
    }
}
