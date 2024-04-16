using System.Text.Json.Serialization;

namespace BetPlacer.Core.API.Models.Response
{
    public class Pager 
    {
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("max_page")]
        public int MaxPage { get; set; }

        [JsonPropertyName("results_per_page")]
        public int ResultsPerPage {  get; set; }

        [JsonPropertyName("total_results")]
        public int TotalPages { get; set; }
    }

    public class Metadata
    {
        [JsonPropertyName("request_limit")]
        public int? RequestLimit { get; set; }

        [JsonPropertyName("request_remaining")]
        public int? RequestRemaining { get; set; }

        [JsonPropertyName("request_reset_message")]
        public string? RequestResetMessage { get; set; }
    }


    public class BaseResponse<T>
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("pager")]
        public Pager Pager { get; set; }

        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }

        [JsonPropertyName("data")]
        public IEnumerable<T> Data { get; set; }
    }
}
