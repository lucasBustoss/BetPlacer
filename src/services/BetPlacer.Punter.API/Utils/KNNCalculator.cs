using BetPlacer.Punter.API.Models.ValueObjects.Match;

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
                Math.Abs((matchToCalculate.HomeOdd.Value - matchReference.HomeOdd.Value) +
                (matchToCalculate.DrawOdd.Value - matchReference.DrawOdd.Value) +
                (matchToCalculate.AwayOdd.Value - matchReference.AwayOdd.Value) +
                (matchToCalculate.PowerPoint.Value - matchReference.PowerPoint.Value) +
                (matchToCalculate.Over25Odd.Value - matchReference.Over25Odd.Value) +
                (matchToCalculate.Under25Odd.Value - matchReference.Under25Odd.Value) +
                (matchToCalculate.BttsYesOdd.Value - matchReference.BttsYesOdd.Value) +
                (matchToCalculate.BttsNoOdd.Value - matchReference.BttsNoOdd.Value) +
                (matchToCalculate.CVMatchOdds.Value - matchReference.CVMatchOdds.Value) +
                (matchToCalculate.HomePoints.Value - matchReference.HomePoints.Value) +
                (matchToCalculate.HomeCVPoints.Value - matchReference.HomeCVPoints.Value) +
                (matchToCalculate.HomeDifferenceGoals.Value - matchReference.HomeDifferenceGoals.Value) +
                (matchToCalculate.HomeCVDifferenceGoals.Value - matchReference.HomeCVDifferenceGoals.Value) +
                (matchToCalculate.HomePoisson.Value - matchReference.HomePoisson.Value) +
                (matchToCalculate.HomeGoalsScored.Value - matchReference.HomeGoalsScored.Value) +
                (matchToCalculate.HomeGoalsScoredValue.Value - matchReference.HomeGoalsScoredValue.Value) +
                (matchToCalculate.HomeGoalsScoredCost.Value - matchReference.HomeGoalsScoredCost.Value) +
                (matchToCalculate.HomeGoalsScoredCV.Value - matchReference.HomeGoalsScoredCV.Value) +
                (matchToCalculate.HomeGoalsConceded.Value - matchReference.HomeGoalsConceded.Value) +
                (matchToCalculate.HomeGoalsConcededValue.Value - matchReference.HomeGoalsConcededValue.Value) +
                (matchToCalculate.HomeGoalsConcededCost.Value - matchReference.HomeGoalsConcededCost.Value) +
                (matchToCalculate.HomeGoalsConcededCV.Value - matchReference.HomeGoalsConcededCV.Value) +
                (matchToCalculate.HomeOddsCV.Value - matchReference.HomeOddsCV.Value) +
                (matchToCalculate.HomeMatchOddsRPS.Value - matchReference.HomeMatchOddsRPS.Value) +
                (matchToCalculate.HomeGoalsRPS.Value - matchReference.HomeGoalsRPS.Value) +
                (matchToCalculate.HomeBTTSRPS.Value - matchReference.HomeBTTSRPS.Value) +
                (matchToCalculate.AwayPoints.Value - matchReference.AwayPoints.Value) +
                (matchToCalculate.AwayCVPoints.Value - matchReference.AwayCVPoints.Value) +
                (matchToCalculate.AwayDifferenceGoals.Value - matchReference.AwayDifferenceGoals.Value) +
                (matchToCalculate.AwayCVDifferenceGoals.Value - matchReference.AwayCVDifferenceGoals.Value) +
                (matchToCalculate.AwayPoisson.Value - matchReference.AwayPoisson.Value) +
                (matchToCalculate.AwayGoalsScored.Value - matchReference.AwayGoalsScored.Value) +
                (matchToCalculate.AwayGoalsScoredValue.Value - matchReference.AwayGoalsScoredValue.Value) +
                (matchToCalculate.AwayGoalsScoredCost.Value - matchReference.AwayGoalsScoredCost.Value) +
                (matchToCalculate.AwayGoalsScoredCV.Value - matchReference.AwayGoalsScoredCV.Value) +
                (matchToCalculate.AwayGoalsConceded.Value - matchReference.AwayGoalsConceded.Value) +
                (matchToCalculate.AwayGoalsConcededValue.Value - matchReference.AwayGoalsConcededValue.Value) +
                (matchToCalculate.AwayGoalsConcededCost.Value - matchReference.AwayGoalsConcededCost.Value) +
                (matchToCalculate.AwayGoalsConcededCV.Value - matchReference.AwayGoalsConcededCV.Value) +
                (matchToCalculate.AwayOddsCV.Value - matchReference.AwayOddsCV.Value) +
                (matchToCalculate.AwayMatchOddsRPS.Value - matchReference.AwayMatchOddsRPS.Value) +
                (matchToCalculate.AwayGoalsRPS.Value - matchReference.AwayGoalsRPS.Value) +
                (matchToCalculate.AwayBTTSRPS.Value - matchReference.AwayBTTSRPS.Value));

            _listKNN.Add(matchReference, difference);
        }

        public void GetTop3(int currentMatchCode)
        {
            var teste = _listKNN.Where(l => l.Key.MatchCode != currentMatchCode).OrderBy(knn => knn.Value).Select(f => new {f.Key.MatchCode, f.Value}).ToList();
            _listTop3 = _listKNN.Where(l => l.Key.MatchCode != currentMatchCode).OrderBy(knn => knn.Value).Take(3).Select(knn => knn.Key).ToList();
        }

        public string GetMatchOddsClassification()
        {
            string result = "";

            foreach (var item in _listTop3)
                result += item.MatchOddsResult;

            return GetNormalizedMatchOddsClassification(result);
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
