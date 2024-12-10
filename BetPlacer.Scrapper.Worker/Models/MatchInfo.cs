namespace BetPlacer.Scrapper.Worker.Models
{
    public class MatchInfo
    {
        public DateTime Date { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
        public double HomeOdd { get; set; }
        public double DrawOdd { get; set; }
        public double AwayOdd { get; set; }
        public double Over25Odd { get; set; }
        public double Under25Odd { get; set; }
        public double BTTSYesOdd { get; set; }
        public double BTTSNoOdd { get; set; }
    }
}
