using BetPlacer.Fixtures.API.Models.RequestModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetPlacer.Fixtures.API.Models.Entities
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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Code { get; set; }
        public int FixtureCode { get; set; }
        public double HomeOdd { get; set; }
        public double DrawOdd { get; set; }
        public double AwayOdd { get; set; }
        public double Over25Odd { get; set; }
        public double Under25Odd { get; set; }
        
        [Column("btts_yes_odd")]
        public double BTTSYesOdd { get; set; }
        
        [Column("btts_no_odd")]
        public double BTTSNoOdd { get; set; }
    }
}
