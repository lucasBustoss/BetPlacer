using BetPlacer.Punter.API.Models.ValueObjects.Match;

namespace BetPlacer.Punter.API.Models.ValueObjects
{
    public class FixtureToFilter
    {
        public FixtureToFilter(MatchAnalyzed matchAnalyzed, MatchBarCode matchBarCode)
        {
            MatchCode = matchAnalyzed.MatchCode;

            MatchOddsClassification = matchAnalyzed.MatchOddsClassification;
            MatchOddsHTClassification = matchAnalyzed.MatchOddsHTClassification;
            GoalsClassification = matchAnalyzed.GoalsClassification;
            BttsClassification = matchAnalyzed.BttsClassification;

            PowerPoint = matchBarCode.PowerPoint.Value;
            PowerPointHT = matchBarCode.PowerPointHT.Value;
            CVMatchOdds = matchBarCode.CVMatchOdds;
            HomeOdd = matchBarCode.HomeOdd;
            DrawOdd = matchBarCode.DrawOdd;
            AwayOdd = matchBarCode.AwayOdd;
            Over25Odd = matchBarCode.Over25Odd;
            Under25Odd = matchBarCode.Under25Odd;
            BttsYesOdd = matchBarCode.BttsYesOdd;
            BttsNoOdd = matchBarCode.BttsNoOdd;

            HomeCVPoints = matchBarCode.HomeCVPoints;
            HomeCVPointsHT = matchBarCode.HomeCVPointsHT;
            HomePoints = matchBarCode.HomePoints;
            HomePointsHT = matchBarCode.HomePointsHT;
            HomeDifferenceGoals = matchBarCode.HomeDifferenceGoals;
            HomeDifferenceGoalsHT = matchBarCode.HomeDifferenceGoalsHT;
            HomeCVDifferenceGoals = matchBarCode.HomeCVDifferenceGoals;
            HomeCVDifferenceGoalsHT = matchBarCode.HomeCVDifferenceGoalsHT;
            HomePoisson = matchBarCode.HomePoisson;
            HomePoissonHT = matchBarCode.HomePoissonHT;
            HomeGoalsScored = matchBarCode.HomeGoalsScored;
            HomeGoalsScoredValue = matchBarCode.HomeGoalsScoredValue;
            HomeGoalsScoredCost = matchBarCode.HomeGoalsScoredCost;
            HomeGoalsScoredCV = matchBarCode.HomeGoalsScoredCV;
            HomeGoalsScoredHT = matchBarCode.HomeGoalsScoredHT;
            HomeGoalsScoredValueHT = matchBarCode.HomeGoalsScoredValueHT;
            HomeGoalsScoredCostHT = matchBarCode.HomeGoalsScoredCostHT;
            HomeGoalsScoredCVHT = matchBarCode.HomeGoalsScoredCVHT;
            HomeGoalsConceded = matchBarCode.HomeGoalsConceded;
            HomeGoalsConcededValue = matchBarCode.HomeGoalsConcededValue;
            HomeGoalsConcededCost = matchBarCode.HomeGoalsConcededCost;
            HomeGoalsConcededCV = matchBarCode.HomeGoalsConcededCV;
            HomeGoalsConcededHT = matchBarCode.HomeGoalsConcededHT;
            HomeGoalsConcededValueHT = matchBarCode.HomeGoalsConcededValueHT;
            HomeGoalsConcededCostHT = matchBarCode.HomeGoalsConcededCostHT;
            HomeGoalsConcededCVHT = matchBarCode.HomeGoalsConcededCVHT;
            HomeOddsCV = matchBarCode.HomeOddsCV;
            HomeMatchOddsRPS = matchBarCode.HomeMatchOddsRPS;
            HomeMatchOddsHTRPS = matchBarCode.HomeMatchOddsHTRPS;
            HomeGoalsRPS = matchBarCode.HomeGoalsRPS;
            HomeBTTSRPS = matchBarCode.HomeBTTSRPS;

            AwayCVPoints = matchBarCode.AwayCVPoints;
            AwayCVPointsHT = matchBarCode.AwayCVPointsHT;
            AwayPoints = matchBarCode.AwayPoints;
            AwayPointsHT = matchBarCode.AwayPointsHT;
            AwayDifferenceGoals = matchBarCode.AwayDifferenceGoals;
            AwayDifferenceGoalsHT = matchBarCode.AwayDifferenceGoalsHT;
            AwayCVDifferenceGoals = matchBarCode.AwayCVDifferenceGoals;
            AwayCVDifferenceGoalsHT = matchBarCode.AwayCVDifferenceGoalsHT;
            AwayPoisson = matchBarCode.AwayPoisson;
            AwayPoissonHT = matchBarCode.AwayPoissonHT;
            AwayGoalsScored = matchBarCode.AwayGoalsScored;
            AwayGoalsScoredValue = matchBarCode.AwayGoalsScoredValue;
            AwayGoalsScoredCost = matchBarCode.AwayGoalsScoredCost;
            AwayGoalsScoredCV = matchBarCode.AwayGoalsScoredCV;
            AwayGoalsScoredHT = matchBarCode.AwayGoalsScoredHT;
            AwayGoalsScoredValueHT = matchBarCode.AwayGoalsScoredValueHT;
            AwayGoalsScoredCostHT = matchBarCode.AwayGoalsScoredCostHT;
            AwayGoalsScoredCVHT = matchBarCode.AwayGoalsScoredCVHT;
            AwayGoalsConceded = matchBarCode.AwayGoalsConceded;
            AwayGoalsConcededValue = matchBarCode.AwayGoalsConcededValue;
            AwayGoalsConcededCost = matchBarCode.AwayGoalsConcededCost;
            AwayGoalsConcededCV = matchBarCode.AwayGoalsConcededCV;
            AwayGoalsConcededHT = matchBarCode.AwayGoalsConcededHT;
            AwayGoalsConcededValueHT = matchBarCode.AwayGoalsConcededValueHT;
            AwayGoalsConcededCostHT = matchBarCode.AwayGoalsConcededCostHT;
            AwayGoalsConcededCVHT = matchBarCode.AwayGoalsConcededCVHT;
            AwayOddsCV = matchBarCode.AwayOddsCV;
            AwayMatchOddsRPS = matchBarCode.AwayMatchOddsRPS;
            AwayMatchOddsHTRPS = matchBarCode.AwayMatchOddsHTRPS;
            AwayGoalsRPS = matchBarCode.AwayGoalsRPS;
            AwayBTTSRPS = matchBarCode.AwayBTTSRPS;
    }

        public int MatchCode { get; set; }
        
        public string MatchOddsClassification { get; set; }
        public string MatchOddsHTClassification { get; set; }
        public string GoalsClassification { get; set; }
        public string BttsClassification { get; set; }

        public double PowerPoint { get; set; }
        public double PowerPointHT { get; set; }
        public double CVMatchOdds { get; set; }
        public double HomeOdd { get; set; }
        public double DrawOdd { get; set; }
        public double AwayOdd { get; set; }
        public double Over25Odd { get; set; }
        public double Under25Odd { get; set; }
        public double BttsYesOdd { get; set; }
        public double BttsNoOdd { get; set; }

        public double HomeCVPoints { get; set; }
        public double HomeCVPointsHT { get; set; }
        public double HomePoints { get; set; }
        public double HomePointsHT { get; set; }
        public double HomeDifferenceGoals { get; set; }
        public double HomeDifferenceGoalsHT { get; set; }
        public double HomeCVDifferenceGoals { get; set; }
        public double HomeCVDifferenceGoalsHT { get; set; }
        public double HomePoisson { get; set; }
        public double HomePoissonHT { get; set; }
        public double HomeGoalsScored { get; set; }
        public double HomeGoalsScoredValue { get; set; }
        public double HomeGoalsScoredCost { get; set; }
        public double HomeGoalsScoredCV { get; set; }
        public double HomeGoalsScoredHT { get; set; }
        public double HomeGoalsScoredValueHT { get; set; }
        public double HomeGoalsScoredCostHT { get; set; }
        public double HomeGoalsScoredCVHT { get; set; }
        public double HomeGoalsConceded { get; set; }
        public double HomeGoalsConcededValue { get; set; }
        public double HomeGoalsConcededCost { get; set; }
        public double HomeGoalsConcededCV { get; set; }
        public double HomeGoalsConcededHT { get; set; }
        public double HomeGoalsConcededValueHT { get; set; }
        public double HomeGoalsConcededCostHT { get; set; }
        public double HomeGoalsConcededCVHT { get; set; }
        public double HomeOddsCV { get; set; }
        public double HomeMatchOddsRPS { get; set; }
        public double HomeMatchOddsHTRPS { get; set; }
        public double HomeGoalsRPS { get; set; }
        public double HomeBTTSRPS { get; set; }

        public double AwayCVPoints { get; set; }
        public double AwayCVPointsHT { get; set; }
        public double AwayPoints { get; set; }
        public double AwayPointsHT { get; set; }
        public double AwayDifferenceGoals { get; set; }
        public double AwayDifferenceGoalsHT { get; set; }
        public double AwayCVDifferenceGoals { get; set; }
        public double AwayCVDifferenceGoalsHT { get; set; }
        public double AwayPoisson { get; set; }
        public double AwayPoissonHT { get; set; }
        public double AwayGoalsScored { get; set; }
        public double AwayGoalsScoredValue { get; set; }
        public double AwayGoalsScoredCost { get; set; }
        public double AwayGoalsScoredCV { get; set; }
        public double AwayGoalsScoredHT { get; set; }
        public double AwayGoalsScoredValueHT { get; set; }
        public double AwayGoalsScoredCostHT { get; set; }
        public double AwayGoalsScoredCVHT { get; set; }
        public double AwayGoalsConceded { get; set; }
        public double AwayGoalsConcededValue { get; set; }
        public double AwayGoalsConcededCost { get; set; }
        public double AwayGoalsConcededCV { get; set; }
        public double AwayGoalsConcededHT { get; set; }
        public double AwayGoalsConcededValueHT { get; set; }
        public double AwayGoalsConcededCostHT { get; set; }
        public double AwayGoalsConcededCVHT { get; set; }
        public double AwayOddsCV { get; set; }
        public double AwayMatchOddsRPS { get; set; }
        public double AwayMatchOddsHTRPS { get; set; }
        public double AwayGoalsRPS { get; set; }
        public double AwayBTTSRPS { get; set; }
    }
}
