using System.Text.Json.Serialization;

namespace BetPlacer.Sync.API.Models.Response.Leagues
{
    public class LeagueSeasonSyncModel
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        
        [JsonPropertyName("leagueCode")] 
        public int LeagueCode { get; set; }
        
        [JsonPropertyName("year")] 
        public string Year { get; set; }
    }
}
