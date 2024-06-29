using System.Text.Json.Serialization;

namespace BetPlacer.Core.API.Models.Request.PinnacleOdds
{
    public class PinnacleOddsModel
    {
        public PinnacleOddsModel(string date, string leagueName, string homeTeam, string awayTeam, double homeOdd, double drawOdd, double awayOdd, double over25Odd, double under25Odd, double? bttsYesOdd, double? bttsNoOdd)
        {
            Date = date;
            LeagueName = leagueName;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            HomeOdd = homeOdd;
            DrawOdd = drawOdd;
            AwayOdd = awayOdd;
            Over25Odd = over25Odd;
            Under25Odd = under25Odd;
            BttsYesOdd = bttsYesOdd;
            BttsNoOdd = bttsNoOdd;
        }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("leagueName")]
        public string LeagueName { get; set; }
        
        [JsonPropertyName("homeTeam")]
        public string HomeTeam { get; set; }
        
        [JsonPropertyName("awayTeam")]
        public string AwayTeam { get; set; }
        
        [JsonPropertyName("homeOdd")]
        public double HomeOdd { get; set; }
        
        [JsonPropertyName("drawOdd")]
        public double DrawOdd { get; set; }
        
        [JsonPropertyName("awayOdd")]
        public double AwayOdd { get; set; }
        
        [JsonPropertyName("over25Odd")]
        public double Over25Odd { get; set; }
        
        [JsonPropertyName("under25Odd")]
        public double Under25Odd { get; set; }
        
        [JsonPropertyName("bttsYesOdd")]
        public double? BttsYesOdd { get; set; }
        
        [JsonPropertyName("bttsNoOdd")]
        public double? BttsNoOdd { get; set; }
    }
}
