using System.Text.Json.Serialization;

namespace BetPlacer.Core.Models
{
    public class BaseCoreResponseModel<T>
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("data")]
        public IEnumerable<T> Data { get; set; }
    }
}
