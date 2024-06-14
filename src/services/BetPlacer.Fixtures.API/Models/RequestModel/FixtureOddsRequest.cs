using System.Text.Json.Serialization;

namespace BetPlacer.Fixtures.API.Models.RequestModel
{
    public class FixtureOddsRequest
    {
        [JsonPropertyName("fixtureCode")]
        public int FixtureCode { get; set; }
        
        [JsonPropertyName("oddHome")]
        public double OddHome { get; set; }
        
        [JsonPropertyName("oddDraw")]
        public double OddDraw { get; set; }
        
        [JsonPropertyName("oddAway")]
        public double OddAway { get; set; }
        
        [JsonPropertyName("oddOver25")]
        public double OddOver25 { get; set; }
        
        [JsonPropertyName("oddUnder25")]
        public double OddUnder25 { get; set; }
        
        [JsonPropertyName("oddBttsYes")]
        public double OddBttsYes { get; set; }
        
        [JsonPropertyName("oddBttsNo")]
        public double OddBttsNo { get; set; }
    }
}
