using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.Request;
using System.Text.Json.Serialization;

namespace BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.Entities
{
    public class FixtureOdds
    {
        public FixtureOdds()
        {

        }

        public FixtureOdds(FixtureOddsRequest oddsRequest)
        {
            FixtureCode = oddsRequest.FixtureCode;
            HomeOdd = oddsRequest.OddHome;
            DrawOdd = oddsRequest.OddDraw;
            AwayOdd = oddsRequest.OddAway;
            Over25Odd = oddsRequest.OddOver25;
            Under25Odd = oddsRequest.OddUnder25;
            BTTSYesOdd = oddsRequest.OddBttsYes;
            BTTSNoOdd = oddsRequest.OddBttsNo;
        }

        public FixtureOdds(int fixtureCode, double homeOdd, double drawOdd, double awayOdd, double over25Odd, double under25Odd, double bttsYesOdd, double bttsNoOdd)
        {
            FixtureCode = fixtureCode;
            HomeOdd = homeOdd;
            DrawOdd = drawOdd;
            AwayOdd = awayOdd;
            Over25Odd = over25Odd;
            Under25Odd = under25Odd;
            BTTSYesOdd = bttsYesOdd;
            BTTSNoOdd = bttsNoOdd;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("fixtureCode")]
        public int FixtureCode { get; set; }
        
        [JsonPropertyName("homeOdd")]
        public double HomeOdd { get; set; }
        
        [JsonPropertyName("drawOdd")]
        public double DrawOdd { get; set; }
        
        [JsonPropertyName("awayOdd")]
        public double AwayOdd { get; set; }
        
        [JsonPropertyName("over25Odd")]
        public double Over25Odd { get; set; }
        
        [JsonPropertyName("away25Odd")]
        public double Under25Odd { get; set; }

        [Column("btts_yes_odd")]
        [JsonPropertyName("bttsYesOdd")]
        public double BTTSYesOdd { get; set; }

        [Column("btts_no_odd")]
        [JsonPropertyName("bttsNoOdd")]
        public double BTTSNoOdd { get; set; }
    }
}
