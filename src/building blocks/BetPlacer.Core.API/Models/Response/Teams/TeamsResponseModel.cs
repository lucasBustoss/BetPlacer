using System.Text.Json.Serialization;

namespace BetPlacer.Core.API.Models.Response.Teams
{
    public class TeamsResponseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("cleanName")]
        public string CleanName { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("season")]
        public string Season { get; set; }
    }
}
