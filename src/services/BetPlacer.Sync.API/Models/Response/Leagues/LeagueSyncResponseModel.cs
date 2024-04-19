using System.Text.Json.Serialization;

namespace BetPlacer.Sync.API.Models.Response.Leagues
{
    public class LeagueSyncResponseModel
    {
        [JsonPropertyName("code")]
        public int? Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }
        
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }
        
        [JsonPropertyName("seasons")]
        public List<LeagueSeasonSyncModel> Seasons { get; set; }
    }
}
