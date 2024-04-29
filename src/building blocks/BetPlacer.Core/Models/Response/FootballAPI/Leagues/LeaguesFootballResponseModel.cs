using System.Text.Json.Serialization;

namespace BetPlacer.Core.Models.Response.FootballAPI.Leagues
{
    public class LeagueSeasonResponseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }
    }

    public class LeaguesFootballResponseModel
    {
        [JsonPropertyName("Code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("league_name")]
        public string LeagueName { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("season")]
        public IEnumerable<LeagueSeasonResponseModel> Season { get; set; }
    }
}
