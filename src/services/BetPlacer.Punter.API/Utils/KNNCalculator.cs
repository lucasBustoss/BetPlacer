using BetPlacer.Punter.API.Models.Match;

namespace BetPlacer.Punter.API.Utils
{
    public class KNNCalculator
    {
        private Dictionary<MatchBarCode, double> _listKNN;
        private List<MatchBarCode> _listTop3;

        public KNNCalculator()
        {
            _listKNN = new Dictionary<MatchBarCode, double>();
            _listTop3 = new List<MatchBarCode>();
        }

        public void CalculateKNN(MatchBarCode matchToCalculate, MatchBarCode matchReference)
        {
            double difference =
                Math.Abs((matchToCalculate.HomeOdd - matchReference.HomeOdd) +
                (matchToCalculate.DrawOdd - matchReference.DrawOdd) +
                (matchToCalculate.AwayOdd - matchReference.AwayOdd) +
                (matchToCalculate.Over25Odd - matchReference.Over25Odd) +
                (matchToCalculate.Under25Odd - matchReference.Under25Odd) +
                (matchToCalculate.BttsYesOdd - matchReference.BttsYesOdd) +
                (matchToCalculate.BttsNoOdd - matchReference.BttsNoOdd) +
                (matchToCalculate.PowerPoint.Value - matchReference.PowerPoint.Value) +
                (matchToCalculate.PowerPointHT.Value - matchReference.PowerPointHT.Value) +
                (matchToCalculate.CVMatchOdds - matchReference.CVMatchOdds) +
                (matchToCalculate.HomeCVPointsHT - matchReference.HomeCVPointsHT) +
                (matchToCalculate.HomePoints - matchReference.HomePoints) +
                (matchToCalculate.HomePointsHT - matchReference.HomePointsHT) +
                (matchToCalculate.HomeDifferenceGoals - matchReference.HomeDifferenceGoals) +
                (matchToCalculate.HomeDifferenceGoalsHT - matchReference.HomeDifferenceGoalsHT) +
                (matchToCalculate.HomeCVDifferenceGoals - matchReference.HomeCVDifferenceGoals) +
                (matchToCalculate.HomeCVDifferenceGoalsHT - matchReference.HomeCVDifferenceGoalsHT) +
                (matchToCalculate.HomePoisson - matchReference.HomePoisson) +
                (matchToCalculate.HomePoissonHT - matchReference.HomePoissonHT) +
                (matchToCalculate.HomeGoalsScored - matchReference.HomeGoalsScored) +
                (matchToCalculate.HomeGoalsScoredValue - matchReference.HomeGoalsScoredValue) +
                (matchToCalculate.HomeGoalsScoredCost - matchReference.HomeGoalsScoredCost) +
                (matchToCalculate.HomeGoalsScoredCV - matchReference.HomeGoalsScoredCV) +
                (matchToCalculate.HomeGoalsScoredHT - matchReference.HomeGoalsScoredHT) +
                (matchToCalculate.HomeGoalsScoredValueHT - matchReference.HomeGoalsScoredValueHT) +
                (matchToCalculate.HomeGoalsScoredCostHT - matchReference.HomeGoalsScoredCostHT) +
                (matchToCalculate.HomeGoalsScoredCVHT - matchReference.HomeGoalsScoredCVHT) +
                (matchToCalculate.HomeGoalsConceded - matchReference.HomeGoalsConceded) +
                (matchToCalculate.HomeGoalsConcededValue - matchReference.HomeGoalsConcededValue) +
                (matchToCalculate.HomeGoalsConcededCost - matchReference.HomeGoalsConcededCost) +
                (matchToCalculate.HomeGoalsConcededCV - matchReference.HomeGoalsConcededCV) +
                (matchToCalculate.HomeGoalsConcededHT - matchReference.HomeGoalsConcededHT) +
                (matchToCalculate.HomeGoalsConcededValueHT - matchReference.HomeGoalsConcededValueHT) +
                (matchToCalculate.HomeGoalsConcededCostHT - matchReference.HomeGoalsConcededCostHT) +
                (matchToCalculate.HomeGoalsConcededCVHT - matchReference.HomeGoalsConcededCVHT) +
                (matchToCalculate.HomeOddsCV - matchReference.HomeOddsCV) +
                (matchToCalculate.HomeMatchOddsRPS - matchReference.HomeMatchOddsRPS) +
                (matchToCalculate.HomeMatchOddsHTRPS - matchReference.HomeMatchOddsHTRPS) +
                (matchToCalculate.HomeGoalsRPS - matchReference.HomeGoalsRPS) +
                (matchToCalculate.HomeBTTSRPS - matchReference.HomeBTTSRPS) +
                (matchToCalculate.AwayCVPointsHT - matchReference.AwayCVPointsHT) +
                (matchToCalculate.AwayPoints - matchReference.AwayPoints) +
                (matchToCalculate.AwayPointsHT - matchReference.AwayPointsHT) +
                (matchToCalculate.AwayDifferenceGoals - matchReference.AwayDifferenceGoals) +
                (matchToCalculate.AwayDifferenceGoalsHT - matchReference.AwayDifferenceGoalsHT) +
                (matchToCalculate.AwayCVDifferenceGoals - matchReference.AwayCVDifferenceGoals) +
                (matchToCalculate.AwayCVDifferenceGoalsHT - matchReference.AwayCVDifferenceGoalsHT) +
                (matchToCalculate.AwayPoisson - matchReference.AwayPoisson) +
                (matchToCalculate.AwayPoissonHT - matchReference.AwayPoissonHT) +
                (matchToCalculate.AwayGoalsScored - matchReference.AwayGoalsScored) +
                (matchToCalculate.AwayGoalsScoredValue - matchReference.AwayGoalsScoredValue) +
                (matchToCalculate.AwayGoalsScoredCost - matchReference.AwayGoalsScoredCost) +
                (matchToCalculate.AwayGoalsScoredCV - matchReference.AwayGoalsScoredCV) +
                (matchToCalculate.AwayGoalsScoredHT - matchReference.AwayGoalsScoredHT) +
                (matchToCalculate.AwayGoalsScoredValueHT - matchReference.AwayGoalsScoredValueHT) +
                (matchToCalculate.AwayGoalsScoredCostHT - matchReference.AwayGoalsScoredCostHT) +
                (matchToCalculate.AwayGoalsScoredCVHT - matchReference.AwayGoalsScoredCVHT) +
                (matchToCalculate.AwayGoalsConceded - matchReference.AwayGoalsConceded) +
                (matchToCalculate.AwayGoalsConcededValue - matchReference.AwayGoalsConcededValue) +
                (matchToCalculate.AwayGoalsConcededCost - matchReference.AwayGoalsConcededCost) +
                (matchToCalculate.AwayGoalsConcededCV - matchReference.AwayGoalsConcededCV) +
                (matchToCalculate.AwayGoalsConcededHT - matchReference.AwayGoalsConcededHT) +
                (matchToCalculate.AwayGoalsConcededValueHT - matchReference.AwayGoalsConcededValueHT) +
                (matchToCalculate.AwayGoalsConcededCostHT - matchReference.AwayGoalsConcededCostHT) +
                (matchToCalculate.AwayGoalsConcededCVHT - matchReference.AwayGoalsConcededCVHT) +
                (matchToCalculate.AwayOddsCV - matchReference.AwayOddsCV) +
                (matchToCalculate.AwayMatchOddsRPS - matchReference.AwayMatchOddsRPS) +
                (matchToCalculate.AwayMatchOddsHTRPS - matchReference.AwayMatchOddsHTRPS) +
                (matchToCalculate.AwayGoalsRPS - matchReference.AwayGoalsRPS) +
                (matchToCalculate.AwayBTTSRPS - matchReference.AwayBTTSRPS));

            _listKNN.Add(matchReference, difference);
        }

        public void GetTop3()
        {
            _listTop3 = _listKNN.OrderBy(knn => knn.Value).Take(3).Select(knn => knn.Key).ToList();
        }

        public string GetMatchOddsClassification()
        {
            string result = "";

            foreach (var item in _listTop3)
                result += item.MatchOddsResult;

            return GetNormalizedMatchOddsClassification(result);
        }

        public string GetMatchOddsHTClassification()
        {
            string result = "";

            foreach (var item in _listTop3)
                result += item.MatchOddsHTResult;

            return GetNormalizedMatchOddsHTClassification(result);
        }

        public string GetGoalsClassification()
        {
            string result = "";

            foreach (var item in _listTop3)
                result += item.GoalsResult;

            return GetNormalizedGoalsClassification(result);
        }

        public string GetBttsClassification()
        {
            string result = "";

            foreach (var item in _listTop3)
                result += item.BttsResult;

            return GetNormalizedBttsClassification(result);
        }

        #region Private methods

        private string GetNormalizedMatchOddsClassification(string classification)
        {
            string result = "";

            switch (classification)
            {
                case "HHH":
                    result = "BackH";
                    break;
                case "HHA":
                case "HAH":
                case "AHH":
                    result = "LayD";
                    break;
                case "HHD":
                case "HDH":
                case "DHH":
                    result = "LayA";
                    break;
                case "AAA":
                    result = "BackA";
                    break;
                case "AAH":
                case "AHA":
                case "HAA":
                    result = "LayD";
                    break;
                case "AAD":
                case "ADA":
                case "DAA":
                    result = "LayH";
                    break;
                case "DDD":
                    result = "BackD";
                    break;
                case "DDH":
                case "DHD":
                case "HDD":
                    result = "LayA";
                    break;
                case "DDA":
                case "DAD":
                case "ADD":
                    result = "LayH";
                    break;
                case "ADH":
                case "AHD":
                case "DHA":
                case "DAH":
                case "HAD":
                case "HDA":
                    result = "Misto";
                    break;
                default:
                    result = "Invalid classification";
                    break;
            }

            return result;
        }

        private string GetNormalizedMatchOddsHTClassification(string classification)
        {
            string result = "";

            switch (classification)
            {
                case "HHH":
                    result = "BackH-HT";
                    break;
                case "HHA":
                case "HAH":
                case "AHH":
                    result = "LayD-HT";
                    break;
                case "HHD":
                case "HDH":
                case "DHH":
                    result = "LayA-HT";
                    break;
                case "AAA":
                    result = "BackA-HT";
                    break;
                case "AAH":
                case "AHA":
                case "HAA":
                    result = "LayD-HT";
                    break;
                case "AAD":
                case "ADA":
                case "DAA":
                    result = "LayH-HT";
                    break;
                case "DDD":
                    result = "BackD-HT";
                    break;
                case "DDH":
                case "DHD":
                case "HDD":
                    result = "LayA-HT";
                    break;
                case "DDA":
                case "DAD":
                case "ADD":
                    result = "LayH-HT";
                    break;
                case "ADH":
                case "AHD":
                case "DHA":
                case "DAH":
                case "HAD":
                case "HDA":
                    result = "Misto-HT";
                    break;
                default:
                    result = "Invalid classification";
                    break;
            }

            return result;
        }

        private string GetNormalizedGoalsClassification(string classification)
        {
            string result = "";

            switch (classification)
            {
                case "OVOVOV":
                case "OVOVSO":
                case "OVOVSU":
                case "OVOVUN":
                case "OVSOOV":
                case "OVSOSU":
                case "OVSOUN":
                case "OVSUOV":
                case "OVSUSO":
                case "OVUNOV":
                case "OVUNSO":
                case "SOOVOV":
                case "SOOVSU":
                case "SOOVUN":
                case "SOSOSU":
                case "SOSOUN":
                case "SOSUOV":
                case "SOSUSO":
                case "SOUNOV":
                case "SOUNSO":
                case "SUOVOV":
                case "SUOVSO":
                case "SUSOOV":
                case "SUSOSO":
                case "UNOVOV":
                case "UNOVSO":
                case "UNSOOV":
                case "UNSOSO":
                    result = "Over";
                    break;

                case "OVSOSO":
                case "SOOVSO":
                case "SOSOOV":
                case "SOSOSO":
                    result = "Sover";
                    break;

                case "OVSUSU":
                case "OVSUUN":
                case "OVUNSU":
                case "OVUNUN":
                case "SOSUSU":
                case "SOSUUN":
                case "SOUNSU":
                case "SOUNUN":
                case "SUOVSU":
                case "SUOVUN":
                case "SUSOSU":
                case "SUSOUN":
                case "SUSUOV":
                case "SUSUSO":
                case "SUUNOV":
                case "SUUNSO":
                case "SUUNUN":
                case "UNOVSU":
                case "UNOVUN":
                case "UNSOSU":
                case "UNSOUN":
                case "UNSUOV":
                case "UNSUSO":
                case "UNSUUN":
                case "UNUNOV":
                case "UNUNSO":
                case "UNUNSU":
                case "UNUNUN":
                    result = "Und";
                    break;

                case "SUSUSU":
                case "SUSUUN":
                case "SUUNSU":
                case "UNSUSU":
                    result = "Sund";
                    break;

                default:
                    result = "Invalid classification";
                    break;
            }

            return result;
        }

        private string GetNormalizedBttsClassification(string classification)
        {
            string result = "";

            switch (classification)
            {
                case "SSS":
                    result = "SuperS";
                    break;
                case "SSN":
                case "SNS":
                case "NSS":
                    result = "Sim";
                    break;
                case "NNN":
                    result = "SuperN";
                    break;
                case "NNS":
                case "NSN":
                case "SNN":
                    result = "Não";
                    break;
                default:
                    result = "";
                    break;
            }

            return result;
        }

        #endregion
    }
}
