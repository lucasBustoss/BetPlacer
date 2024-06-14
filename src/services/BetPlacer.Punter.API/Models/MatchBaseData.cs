using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Punter.API.Models
{
    public class MatchBaseData
    {
        [Key]
        public int MatchCode { get; set; }
        public string Season { get; set; }

        public string FormattedSeason
        {
            get
            {
                if (Season.Length > 4)
                    return $"{Season.Substring(0, 4)}-{Season.Substring(4, 4)}";

                return Season;
            }
        }

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
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public string MatchResult { get; set; }
        public int HomePoints { get; set; }
        public int AwayPoints { get; set; }
        public int HomeGoalsDifference { get; set; }
        public int AwayGoalsDifference { get; set; }
        public int HomeGoalsHT { get; set; }
        public int AwayGoalsHT { get; set; }
        public string MatchResultHT { get; set; }
        public int HomePointsHT { get; set; }
        public int AwayPointsHT { get; set; }
        public int HomeGoalsDifferenceHT { get; set; }
        public int AwayGoalsDifferenceHT { get; set; }
        public double HomePercentageOdd { get; set; }
        public double AwayPercentageOdd { get; set; }
        public double HomeScoredGoalValue { get; set; }
        public double HomeScoredGoalCost { get; set; }
        public double AwayScoredGoalValue { get; set; }
        public double AwayScoredGoalCost { get; set; }
        public double HomeConcededGoalValue { get; set; }
        public double HomeConcededGoalCost { get; set; }
        public double AwayConcededGoalValue { get; set; }
        public double AwayConcededGoalCost { get; set; }
        public double HomePointsValue { get; set; }
        public double AwayPointsValue { get; set; }
        public double HomeGoalsDifferenceValue { get; set; }
        public double AwayGoalsDifferenceValue { get; set; }
        public double HomeScoredGoalValueHT { get; set; }
        public double HomeScoredGoalCostHT { get; set; }
        public double AwayScoredGoalValueHT { get; set; }
        public double AwayScoredGoalCostHT { get; set; }
        public double HomeConcededGoalValueHT { get; set; }
        public double HomeConcededGoalCostHT { get; set; }
        public double AwayConcededGoalValueHT { get; set; }
        public double AwayConcededGoalCostHT { get; set; }
        public double HomePointsValueHT { get; set; }
        public double AwayPointsValueHT { get; set; }
        public double HomeGoalsDifferenceValueHT { get; set; }
        public double AwayGoalsDifferenceValueHT { get; set; }
    }
}
