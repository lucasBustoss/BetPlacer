using Microsoft.EntityFrameworkCore;

namespace BetPlacer.Punter.API.Models
{
    [Keyless]
    public class NextMatch
    {
        public int MatchCode { get; set; }
        public string Season { get; set; }
        public string Date { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public double HomeOdd { get; set; }
        public double DrawOdd { get; set; }
        public double AwayOdd { get; set; }
        public double Over25Odd { get; set; }
        public double Under25Odd { get; set; }
        public double BttsYesOdd { get; set; }
        public double BttsNoOdd { get; set; }
    }
}
