using BetPlacer.Punter.API.Models.ValueObjects.Match;

namespace BetPlacer.Punter.API.Models.ValueObjects
{
    public class FixtureToFilter
    {
        public FixtureToFilter(MatchAnalyzed matchAnalyzed, MatchBarCode matchBarCode)
        {
            MatchCode = matchAnalyzed.MatchCode;

            MatchOddsClassification = matchAnalyzed.MatchOddsClassification;
            GoalsClassification = matchAnalyzed.GoalsClassification;
            BttsClassification = matchAnalyzed.BttsClassification;

            PowerPoint = matchBarCode.PowerPoint.Value;
            CVMatchOdds = matchBarCode.CVMatchOdds;
            HomeOdd = matchBarCode.HomeOdd;
            DrawOdd = matchBarCode.DrawOdd;
            AwayOdd = matchBarCode.AwayOdd;
            Over25Odd = matchBarCode.Over25Odd;
            Under25Odd = matchBarCode.Under25Odd;
            BttsYesOdd = matchBarCode.BttsYesOdd;
            BttsNoOdd = matchBarCode.BttsNoOdd;

            HomeCVPoints = matchBarCode.HomeCVPoints.Value;
            HomePoints = matchBarCode.HomePoints;
            HomeDifferenceGoals = matchBarCode.HomeDifferenceGoals;
            HomeCVDifferenceGoals = matchBarCode.HomeCVDifferenceGoals;
            HomePoisson = matchBarCode.HomePoisson.Value;
            HomeGoalsScored = matchBarCode.HomeGoalsScored;
            HomeGoalsScoredValue = matchBarCode.HomeGoalsScoredValue;
            HomeGoalsScoredCost = matchBarCode.HomeGoalsScoredCost;
            HomeGoalsScoredCV = matchBarCode.HomeGoalsScoredCV;
            HomeGoalsConceded = matchBarCode.HomeGoalsConceded;
            HomeGoalsConcededValue = matchBarCode.HomeGoalsConcededValue;
            HomeGoalsConcededCost = matchBarCode.HomeGoalsConcededCost;
            HomeGoalsConcededCV = matchBarCode.HomeGoalsConcededCV;
            HomeOddsCV = matchBarCode.HomeOddsCV;
            HomeMatchOddsRPS = matchBarCode.HomeMatchOddsRPS;
            HomeGoalsRPS = matchBarCode.HomeGoalsRPS;
            HomeBTTSRPS = matchBarCode.HomeBTTSRPS;

            AwayCVPoints = matchBarCode.AwayCVPoints;
            AwayPoints = matchBarCode.AwayPoints;
            AwayDifferenceGoals = matchBarCode.AwayDifferenceGoals;
            AwayCVDifferenceGoals = matchBarCode.AwayCVDifferenceGoals;
            AwayPoisson = matchBarCode.AwayPoisson.Value;
            AwayGoalsScored = matchBarCode.AwayGoalsScored;
            AwayGoalsScoredValue = matchBarCode.AwayGoalsScoredValue;
            AwayGoalsScoredCost = matchBarCode.AwayGoalsScoredCost;
            AwayGoalsScoredCV = matchBarCode.AwayGoalsScoredCV;
            AwayGoalsConceded = matchBarCode.AwayGoalsConceded;
            AwayGoalsConcededValue = matchBarCode.AwayGoalsConcededValue;
            AwayGoalsConcededCost = matchBarCode.AwayGoalsConcededCost;
            AwayGoalsConcededCV = matchBarCode.AwayGoalsConcededCV;
            AwayOddsCV = matchBarCode.AwayOddsCV;
            AwayMatchOddsRPS = matchBarCode.AwayMatchOddsRPS;
            AwayGoalsRPS = matchBarCode.AwayGoalsRPS;
            AwayBTTSRPS = matchBarCode.AwayBTTSRPS;
    }

        public int MatchCode { get; set; }
        
        public string MatchOddsClassification { get; set; }
        public string GoalsClassification { get; set; }
        public string BttsClassification { get; set; }

        public double? PowerPoint { get; set; }
        public double? CVMatchOdds { get; set; }
        public double? HomeOdd { get; set; }
        public double? DrawOdd { get; set; }
        public double? AwayOdd { get; set; }
        public double? Over25Odd { get; set; }
        public double? Under25Odd { get; set; }
        public double? BttsYesOdd { get; set; }
        public double? BttsNoOdd { get; set; }

        public double? HomeCVPoints { get; set; }
        public double? HomePoints { get; set; }
        public double? HomeDifferenceGoals { get; set; }
        public double? HomeCVDifferenceGoals { get; set; }
        public double? HomePoisson { get; set; }
        public double? HomeGoalsScored { get; set; }
        public double? HomeGoalsScoredValue { get; set; }
        public double? HomeGoalsScoredCost { get; set; }
        public double? HomeGoalsScoredCV { get; set; }
        public double? HomeGoalsConceded { get; set; }
        public double? HomeGoalsConcededValue { get; set; }
        public double? HomeGoalsConcededCost { get; set; }
        public double? HomeGoalsConcededCV { get; set; }
        public double? HomeOddsCV { get; set; }
        public double? HomeMatchOddsRPS { get; set; }
        public double? HomeGoalsRPS { get; set; }
        public double? HomeBTTSRPS { get; set; }

        public double? AwayCVPoints { get; set; }
        public double? AwayPoints { get; set; }
        public double? AwayDifferenceGoals { get; set; }
        public double? AwayCVDifferenceGoals { get; set; }
        public double? AwayPoisson { get; set; }
        public double? AwayGoalsScored { get; set; }
        public double? AwayGoalsScoredValue { get; set; }
        public double? AwayGoalsScoredCost { get; set; }
        public double? AwayGoalsScoredCV { get; set; }
        public double? AwayGoalsConceded { get; set; }
        public double? AwayGoalsConcededValue { get; set; }
        public double? AwayGoalsConcededCost { get; set; }
        public double? AwayGoalsConcededCV { get; set; }
        public double? AwayOddsCV { get; set; }
        public double? AwayMatchOddsRPS { get; set; }
        public double? AwayGoalsRPS { get; set; }
        public double? AwayBTTSRPS { get; set; }
    }
}
