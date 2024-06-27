using BetPlacer.Punter.API.Utils.Normalization;

namespace BetPlacer.Punter.API.Models.Match
{
    public class MatchBarCode
    {
        private bool _valuesNormalized = false;
        private int _index = -1;

        public MatchBarCode(int index, int matchCode, double homeOdd, double drawOdd, double awayOdd, double over25Odd, double under25Odd, double bttsYesOdd, double bttsNoOdd, string matchOddsResult, string matchOddsHTResult, string bttsResult, int homeGoals, int awayGoals)
        {
            _index = index;
            
            MatchCode = matchCode;

            HomeOdd = 1 / homeOdd;
            DrawOdd = 1 / drawOdd;
            AwayOdd = 1 / awayOdd;

            Over25Odd = 1 / over25Odd;
            Under25Odd = 1 / under25Odd;

            BttsYesOdd = 1 / bttsYesOdd;
            BttsNoOdd = 1 / bttsNoOdd;

            MatchOddsResult = matchOddsResult;
            MatchOddsHTResult = matchOddsHTResult;
            BttsResult = bttsResult;

            string goalsResult = "";
            int totalGoals = homeGoals + awayGoals;

            if (totalGoals == 2)
                goalsResult = "UN";
            else if (totalGoals == 3)
                goalsResult = "OV";
            else if (totalGoals < 2)
                goalsResult = "SU";
            else
                goalsResult = "SO";

            GoalsResult = goalsResult;
        }

        public void NormalizeValues()
        {
            if (_valuesNormalized)
                return;

            HomeOdd = NormalizeHomeOdd(HomeOdd);
            DrawOdd = NormalizeDrawOdd(DrawOdd);
            AwayOdd = NormalizeAwayOdd(AwayOdd);

            Over25Odd = NormalizeOver25Odd(Over25Odd);
            Under25Odd = NormalizeUnder25Odd(Under25Odd);

            BttsYesOdd = NormalizeBttsYesOdd(BttsYesOdd);
            BttsNoOdd = NormalizeBttsNoOdd(AwayOdd);

            PowerPoint = PowerPoint != null ? NormalizePowerPoint(PowerPoint.Value) : 0;
            PowerPointHT = PowerPointHT != null ? NormalizePowerPointHT(PowerPointHT.Value) : 0;
            CVMatchOdds = NormalizeCVMatchOdds(CVMatchOdds);

            HomeCVPoints = NormalizeHomeCVPoints(HomeCVPoints);
            HomeCVPointsHT = NormalizeHomeCVPointsHT(HomeCVPointsHT);
            HomePoints = NormalizeHomePoints(HomePoints);
            HomePointsHT = NormalizeHomePointsHT(HomePointsHT);
            HomeDifferenceGoals = NormalizeHomeDifferenceGoals(HomeDifferenceGoals);
            HomeDifferenceGoalsHT = NormalizeHomeDifferenceGoalsHT(HomeDifferenceGoalsHT);
            HomeCVDifferenceGoals = NormalizeHomeCVDifferenceGoals(HomeCVDifferenceGoals);
            HomeCVDifferenceGoalsHT = NormalizeHomeCVDifferenceGoalsHT(HomeCVDifferenceGoalsHT);
            HomePoisson = NormalizeHomePoisson(HomePoisson);
            HomePoissonHT = NormalizeHomePoissonHT(HomePoissonHT);
            HomeGoalsScored = NormalizeHomeGoalsScored(HomeGoalsScored);
            HomeGoalsScoredValue = NormalizeHomeGoalsScoredValue(HomeGoalsScoredValue);
            HomeGoalsScoredCost = NormalizeHomeGoalsScoredCost(HomeGoalsScoredCost);
            HomeGoalsScoredCV = NormalizeHomeGoalsScoredCV(HomeGoalsScoredCV);
            HomeGoalsScoredHT = NormalizeHomeGoalsScoredHT(HomeGoalsScoredHT);
            HomeGoalsScoredValueHT = NormalizeHomeGoalsScoredValueHT(HomeGoalsScoredValueHT);
            HomeGoalsScoredCostHT = NormalizeHomeGoalsScoredCostHT(HomeGoalsScoredCostHT);
            HomeGoalsScoredCVHT = NormalizeHomeGoalsScoredCVHT(HomeGoalsScoredCVHT);
            HomeGoalsConceded = NormalizeHomeGoalsConceded(HomeGoalsConceded);
            HomeGoalsConcededValue = NormalizeHomeGoalsConcededValue(HomeGoalsConcededValue);
            HomeGoalsConcededCost = NormalizeHomeGoalsConcededCost(HomeGoalsConcededCost);
            HomeGoalsConcededCV = NormalizeHomeGoalsConcededCV(HomeGoalsConcededCV);
            HomeGoalsConcededHT = NormalizeHomeGoalsConcededHT(HomeGoalsConcededHT);
            HomeGoalsConcededValueHT = NormalizeHomeGoalsConcededValueHT(HomeGoalsConcededValueHT);
            HomeGoalsConcededCostHT = NormalizeHomeGoalsConcededCostHT(HomeGoalsConcededCostHT);
            HomeGoalsConcededCVHT = NormalizeHomeGoalsConcededCVHT(HomeGoalsConcededCVHT);
            HomeOddsCV = NormalizeHomeOddsCV(HomeOddsCV);
            HomeMatchOddsRPS = NormalizeHomeMatchOddsRPS(HomeMatchOddsRPS);
            HomeMatchOddsHTRPS = NormalizeHomeMatchOddsHTRPS(HomeMatchOddsHTRPS);
            HomeGoalsRPS = NormalizeHomeGoalsRPS(HomeGoalsRPS);
            HomeBTTSRPS = NormalizeHomeBTTSRPS(HomeBTTSRPS);

            AwayCVPoints = NormalizeAwayCVPoints(AwayCVPoints);
            AwayCVPointsHT = NormalizeAwayCVPoints(AwayCVPoints);
            AwayPoints = NormalizeAwayCVPoints(AwayCVPoints);
            AwayPointsHT = NormalizeAwayCVPoints(AwayCVPoints);
            AwayDifferenceGoals = NormalizeAwayCVPoints(AwayCVPoints);
            AwayDifferenceGoalsHT = NormalizeAwayCVPoints(AwayCVPoints);
            AwayCVDifferenceGoals = NormalizeAwayCVPoints(AwayCVPoints);
            AwayCVDifferenceGoalsHT = NormalizeAwayCVPoints(AwayCVPoints);
            AwayPoisson = NormalizeAwayCVPoints(AwayCVPoints);
            AwayPoissonHT = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsScored = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsScoredValue = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsScoredCost = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsScoredCV = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsScoredHT = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsScoredValueHT = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsScoredCostHT = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsScoredCVHT = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsConceded = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsConcededValue = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsConcededCost = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsConcededCV = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsConcededHT = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsConcededValueHT = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsConcededCostHT = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsConcededCVHT = NormalizeAwayCVPoints(AwayCVPoints);
            AwayOddsCV = NormalizeAwayCVPoints(AwayCVPoints);
            AwayMatchOddsRPS = NormalizeAwayCVPoints(AwayCVPoints);
            AwayMatchOddsHTRPS = NormalizeAwayCVPoints(AwayCVPoints);
            AwayGoalsRPS = NormalizeAwayCVPoints(AwayCVPoints);
            AwayBTTSRPS = NormalizeAwayCVPoints(AwayCVPoints);

            _valuesNormalized = true;
        }

        public int Code { get; set; }
        public int MatchCode { get; set; }

        public string MatchOddsResult { get; set; }
        public string MatchOddsHTResult { get; set; }
        public string GoalsResult { get; set; }
        public string BttsResult { get; set; }

        public double? PowerPoint { get; set; }
        public double? PowerPointHT { get; set; }
        public double CVMatchOdds { get; set; }
        public double HomeOdd { get; set; }
        public double DrawOdd { get; set; }
        public double AwayOdd { get; set; }
        public double Over25Odd { get; set; }
        public double Under25Odd { get; set; }
        public double BttsYesOdd { get; set; }
        public double BttsNoOdd { get; set; }

        #region Values

        #region Home

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

        #endregion

        #region Away

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

        #endregion

        #endregion


        #region Methods

        private double NormalizeHomeOdd(double value) => NormalizationManager.Instance.HomeOddNormalizer.Normalize(value, _index);
        private double NormalizeDrawOdd(double value) => NormalizationManager.Instance.DrawOddNormalizer.Normalize(value, _index);
        private double NormalizeAwayOdd(double value) => NormalizationManager.Instance.AwayOddNormalizer.Normalize(value, _index);

        private double NormalizeOver25Odd(double value) => NormalizationManager.Instance.Over25OddNormalizer.Normalize(value, _index);
        private double NormalizeUnder25Odd(double value) => NormalizationManager.Instance.Under25OddNormalizer.Normalize(value, _index);

        private double NormalizeBttsYesOdd(double value) => NormalizationManager.Instance.BttsYesOddNormalizer.Normalize(value, _index);
        private double NormalizeBttsNoOdd(double value) => NormalizationManager.Instance.BttsNoOddNormalizer.Normalize(value, _index);

        private double NormalizePowerPoint(double value) => NormalizationManager.Instance.PowerPointNormalizer.Normalize(value, _index);
        private double NormalizePowerPointHT(double value) => NormalizationManager.Instance.PowerPointHTNormalizer.Normalize(value, _index);
        private double NormalizeCVMatchOdds(double value) => NormalizationManager.Instance.CVMatchOddsNormalizer.Normalize(value, _index);

        private double NormalizeHomeCVPoints(double value) => NormalizationManager.Instance.HomeCVPointsNormalizer.Normalize(value, _index);
        private double NormalizeHomeCVPointsHT(double value) => NormalizationManager.Instance.HomeCVPointsHTNormalizer.Normalize(value, _index);
        private double NormalizeHomePoints(double value) => NormalizationManager.Instance.HomePointsNormalizer.Normalize(value, _index);
        private double NormalizeHomePointsHT(double value) => NormalizationManager.Instance.HomePointsHTNormalizer.Normalize(value, _index);
        private double NormalizeHomeDifferenceGoals(double value) => NormalizationManager.Instance.HomeDifferenceGoalsNormalizer.Normalize(value, _index);
        private double NormalizeHomeDifferenceGoalsHT(double value) => NormalizationManager.Instance.HomeDifferenceGoalsHTNormalizer.Normalize(value, _index);
        private double NormalizeHomeCVDifferenceGoals(double value) => NormalizationManager.Instance.HomeCVDifferenceGoalsNormalizer.Normalize(value, _index);
        private double NormalizeHomeCVDifferenceGoalsHT(double value) => NormalizationManager.Instance.HomeCVDifferenceGoalsHTNormalizer.Normalize(value, _index);
        private double NormalizeHomePoisson(double value) => NormalizationManager.Instance.HomePoissonNormalizer.Normalize(value, _index);
        private double NormalizeHomePoissonHT(double value) => NormalizationManager.Instance.HomePoissonHTNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsScored(double value) => NormalizationManager.Instance.HomeGoalsScoredNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsScoredValue(double value) => NormalizationManager.Instance.HomeGoalsScoredValueNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsScoredCost(double value) => NormalizationManager.Instance.HomeGoalsScoredCostNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsScoredCV(double value) => NormalizationManager.Instance.HomeGoalsScoredCVNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsScoredHT(double value) => NormalizationManager.Instance.HomeGoalsScoredHTNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsScoredValueHT(double value) => NormalizationManager.Instance.HomeGoalsScoredValueHTNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsScoredCostHT(double value) => NormalizationManager.Instance.HomeGoalsScoredCostHTNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsScoredCVHT(double value) => NormalizationManager.Instance.HomeGoalsScoredCVHTNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsConceded(double value) => NormalizationManager.Instance.HomeGoalsConcededNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsConcededValue(double value) => NormalizationManager.Instance.HomeGoalsConcededValueNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsConcededCost(double value) => NormalizationManager.Instance.HomeGoalsConcededCostNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsConcededCV(double value) => NormalizationManager.Instance.HomeGoalsConcededCVNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsConcededHT(double value) => NormalizationManager.Instance.HomeGoalsConcededHTNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsConcededValueHT(double value) => NormalizationManager.Instance.HomeGoalsConcededValueHTNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsConcededCostHT(double value) => NormalizationManager.Instance.HomeGoalsConcededCostHTNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsConcededCVHT(double value) => NormalizationManager.Instance.HomeGoalsConcededCVHTNormalizer.Normalize(value, _index);
        private double NormalizeHomeOddsCV(double value) => NormalizationManager.Instance.HomeOddsCVNormalizer.Normalize(value, _index);
        private double NormalizeHomeMatchOddsRPS(double value) => NormalizationManager.Instance.HomeMatchOddsRPSNormalizer.Normalize(value, _index);
        private double NormalizeHomeMatchOddsHTRPS(double value) => NormalizationManager.Instance.HomeMatchOddsHTRPSNormalizer.Normalize(value, _index);
        private double NormalizeHomeGoalsRPS(double value) => NormalizationManager.Instance.HomeGoalsRPSNormalizer.Normalize(value, _index);
        private double NormalizeHomeBTTSRPS(double value) => NormalizationManager.Instance.HomeBTTSRPSNormalizer.Normalize(value, _index);

        private double NormalizeAwayCVPoints(double value) => NormalizationManager.Instance.AwayCVPointsNormalizer.Normalize(value, _index);
        private double NormalizeAwayCVPointsHT(double value) => NormalizationManager.Instance.AwayCVPointsHTNormalizer.Normalize(value, _index);
        private double NormalizeAwayPoints(double value) => NormalizationManager.Instance.AwayPointsNormalizer.Normalize(value, _index);
        private double NormalizeAwayPointsHT(double value) => NormalizationManager.Instance.AwayPointsHTNormalizer.Normalize(value, _index);
        private double NormalizeAwayDifferenceGoals(double value) => NormalizationManager.Instance.AwayDifferenceGoalsNormalizer.Normalize(value, _index);
        private double NormalizeAwayDifferenceGoalsHT(double value) => NormalizationManager.Instance.AwayDifferenceGoalsHTNormalizer.Normalize(value, _index);
        private double NormalizeAwayCVDifferenceGoals(double value) => NormalizationManager.Instance.AwayCVDifferenceGoalsNormalizer.Normalize(value, _index);
        private double NormalizeAwayCVDifferenceGoalsHT(double value) => NormalizationManager.Instance.AwayCVDifferenceGoalsHTNormalizer.Normalize(value, _index);
        private double NormalizeAwayPoisson(double value) => NormalizationManager.Instance.AwayPoissonNormalizer.Normalize(value, _index);
        private double NormalizeAwayPoissonHT(double value) => NormalizationManager.Instance.AwayPoissonHTNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsScored(double value) => NormalizationManager.Instance.AwayGoalsScoredNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsScoredValue(double value) => NormalizationManager.Instance.AwayGoalsScoredValueNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsScoredCost(double value) => NormalizationManager.Instance.AwayGoalsScoredCostNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsScoredCV(double value) => NormalizationManager.Instance.AwayGoalsScoredCVNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsScoredHT(double value) => NormalizationManager.Instance.AwayGoalsScoredHTNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsScoredValueHT(double value) => NormalizationManager.Instance.AwayGoalsScoredValueHTNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsScoredCostHT(double value) => NormalizationManager.Instance.AwayGoalsScoredCostHTNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsScoredCVHT(double value) => NormalizationManager.Instance.AwayGoalsScoredCVHTNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsConceded(double value) => NormalizationManager.Instance.AwayGoalsConcededNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsConcededValue(double value) => NormalizationManager.Instance.AwayGoalsConcededValueNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsConcededCost(double value) => NormalizationManager.Instance.AwayGoalsConcededCostNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsConcededCV(double value) => NormalizationManager.Instance.AwayGoalsConcededCVNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsConcededHT(double value) => NormalizationManager.Instance.AwayGoalsConcededHTNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsConcededValueHT(double value) => NormalizationManager.Instance.AwayGoalsConcededValueHTNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsConcededCostHT(double value) => NormalizationManager.Instance.AwayGoalsConcededCostHTNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsConcededCVHT(double value) => NormalizationManager.Instance.AwayGoalsConcededCVHTNormalizer.Normalize(value, _index);
        private double NormalizeAwayOddsCV(double value) => NormalizationManager.Instance.AwayOddsCVNormalizer.Normalize(value, _index);
        private double NormalizeAwayMatchOddsRPS(double value) => NormalizationManager.Instance.AwayMatchOddsRPSNormalizer.Normalize(value, _index);
        private double NormalizeAwayMatchOddsHTRPS(double value) => NormalizationManager.Instance.AwayMatchOddsHTRPSNormalizer.Normalize(value, _index);
        private double NormalizeAwayGoalsRPS(double value) => NormalizationManager.Instance.AwayGoalsRPSNormalizer.Normalize(value, _index);
        private double NormalizeAwayBTTSRPS(double value) => NormalizationManager.Instance.AwayBTTSRPSNormalizer.Normalize(value, _index);

        #endregion
    }
}
