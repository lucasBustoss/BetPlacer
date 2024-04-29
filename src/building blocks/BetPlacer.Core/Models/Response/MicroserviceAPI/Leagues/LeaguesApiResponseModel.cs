using System.Text.Json.Serialization;

namespace BetPlacer.Core.Models.Response.Microservice.Leagues
{
    public class LeagueApiSeasonResponseModel
    {
        [JsonPropertyName("code")]
        public int Id { get; set; }

        [JsonPropertyName("year")]
        public string Year { get; set; }
    }

    public class LeaguesApiResponseModel
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("imageUrl")]
        public string Image { get; set; }

        [JsonPropertyName("seasons")]
        public IEnumerable<LeagueApiSeasonResponseModel> Season { get; set; }
    }
}
