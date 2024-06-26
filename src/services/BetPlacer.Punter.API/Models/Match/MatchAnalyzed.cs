namespace BetPlacer.Punter.API.Models.Match
{
    public class MatchAnalyzed
    {
        public MatchAnalyzed(MatchBaseData match, string matchOddsClassification, string matchOddsHTClassification, string goalsClassification, string bttsClassification)
        {
            MatchCode = match.MatchCode;
            Date = match.Date;
            HomeOdd = match.HomeOdd;
            DrawOdd = match.DrawOdd;
            AwayOdd = match.AwayOdd;
            Over25Odd = match.Over25Odd;
            Under25Odd = match.Under25Odd;
            BttsYesOdd = match.BttsYesOdd;
            BttsNoOdd = match.BttsNoOdd;
            HomeGoals = match.HomeGoals;
            AwayGoals = match.AwayGoals;
            HomeHTGoals = match.HomeGoalsHT;
            AwayHTGoals = match.AwayGoalsHT;
            MatchOddsClassification = matchOddsClassification;
            MatchOddsHTClassification = matchOddsHTClassification;
            GoalsClassification = goalsClassification;
            BttsClassification = bttsClassification;
        }

        public int MatchCode { get; set; }
        public string Date { get; set; }
        public double HomeOdd { get; set; }
        public double DrawOdd { get; set; }
        public double AwayOdd { get; set; }
        public double Over25Odd { get; set; }
        public double Under25Odd { get; set; }
        public double BttsYesOdd { get; set; }
        public double BttsNoOdd { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public int HomeHTGoals { get; set; }
        public int AwayHTGoals { get; set; }
        public string MatchOddsClassification { get; set; }
        public string MatchOddsHTClassification { get; set; }
        public string GoalsClassification { get; set; }
        public string BttsClassification { get; set; }

        public int TotalGoals
        {
            get
            {
                return HomeGoals + AwayGoals;
            }
        }

        public int TotalHTGoals
        {
            get
            {
                return HomeHTGoals + AwayHTGoals;
            }
        }

        public int DifferenceGoals
        {
            get
            {
                return HomeGoals - AwayGoals;
            }
        }

        public int DifferenceHTGoals
        {
            get
            {
                return HomeHTGoals - AwayHTGoals;
            }
        }
    }
}
