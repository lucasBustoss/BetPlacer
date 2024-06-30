using System.Text.Json.Serialization;

namespace BetPlacer.Core.Models.Response.Microservice.Teams
{
    public class TeamsApiResponseModel
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
}
