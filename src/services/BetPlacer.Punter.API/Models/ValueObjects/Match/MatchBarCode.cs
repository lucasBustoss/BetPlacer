using BetPlacer.Punter.API.Utils.Normalization;

namespace BetPlacer.Punter.API.Models.ValueObjects.Match
{
    public class MatchBarCode
    {
        private bool _valuesNormalized = false;
        private int _index = -1;
        private int _matchCode;

        public MatchBarCode()
        {
            
        }

        // Ctor para NextMatch
        public MatchBarCode(int matchCode, double? homeOdd, double? drawOdd, double? awayOdd, double? over25Odd, double? under25Odd, double? bttsYesOdd, double? bttsNoOdd, int index)
        {
            MatchCode = matchCode;

            HomeOdd = 1 / homeOdd;
            DrawOdd = 1 / drawOdd;
            AwayOdd = 1 / awayOdd;

            Over25Odd = 1 / over25Odd;
            Under25Odd = 1 / under25Odd;

            BttsYesOdd = 1 / bttsYesOdd;
            BttsNoOdd = 1 / bttsNoOdd;

            _index = index;
            _matchCode = matchCode;
        }

        // Ctor para MatchBaseData (pastMatch)
        public MatchBarCode(int index, int matchCode, double? homeOdd, double? drawOdd, double? awayOdd, double? over25Odd, double? under25Odd, double? bttsYesOdd, double? bttsNoOdd, string matchOddsResult, string bttsResult, int homeGoals, int awayGoals)
        {
            _index = index;
            _matchCode = matchCode;

            MatchCode = matchCode;

            HomeOdd = 1 / homeOdd;
            DrawOdd = 1 / drawOdd;
            AwayOdd = 1 / awayOdd;

            Over25Odd = 1 / over25Odd;
            Under25Odd = 1 / under25Odd;

            BttsYesOdd = 1 / bttsYesOdd;
            BttsNoOdd = 1 / bttsNoOdd;

            MatchOddsResult = matchOddsResult;
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
            BttsNoOdd = NormalizeBttsNoOdd(BttsNoOdd);

            PowerPoint = NormalizePowerPoint(PowerPoint);
            CVMatchOdds = NormalizeCVMatchOdds(CVMatchOdds);

            HomeCVPoints = NormalizeHomeCVPoints(HomeCVPoints);
            HomePoints = NormalizeHomePoints(HomePoints);
            HomeDifferenceGoals = NormalizeHomeDifferenceGoals(HomeDifferenceGoals);
            HomeCVDifferenceGoals = NormalizeHomeCVDifferenceGoals(HomeCVDifferenceGoals);
            HomePoisson = NormalizeHomePoisson(HomePoisson);
            HomeGoalsScored = NormalizeHomeGoalsScored(HomeGoalsScored);
            HomeGoalsScoredValue = NormalizeHomeGoalsScoredValue(HomeGoalsScoredValue);
            HomeGoalsScoredCost = NormalizeHomeGoalsScoredCost(HomeGoalsScoredCost);
            HomeGoalsScoredCV = NormalizeHomeGoalsScoredCV(HomeGoalsScoredCV);
            HomeGoalsConceded = NormalizeHomeGoalsConceded(HomeGoalsConceded);
            HomeGoalsConcededValue = NormalizeHomeGoalsConcededValue(HomeGoalsConcededValue);
            HomeGoalsConcededCost = NormalizeHomeGoalsConcededCost(HomeGoalsConcededCost);
            HomeGoalsConcededCV = NormalizeHomeGoalsConcededCV(HomeGoalsConcededCV);
            HomeOddsCV = NormalizeHomeOddsCV(HomeOddsCV);
            HomeMatchOddsRPS = NormalizeHomeMatchOddsRPS(HomeMatchOddsRPS);
            HomeGoalsRPS = NormalizeHomeGoalsRPS(HomeGoalsRPS);
            HomeBTTSRPS = NormalizeHomeBTTSRPS(HomeBTTSRPS);

            AwayCVPoints = NormalizeAwayCVPoints(AwayCVPoints);
            AwayPoints = NormalizeAwayPoints(AwayPoints);
            AwayDifferenceGoals = NormalizeAwayDifferenceGoals(AwayDifferenceGoals);
            AwayCVDifferenceGoals = NormalizeAwayCVDifferenceGoals(AwayCVDifferenceGoals);
            AwayPoisson = NormalizeAwayPoisson(AwayPoisson);
            AwayGoalsScored = NormalizeAwayGoalsScored(AwayGoalsScored);
            AwayGoalsScoredValue = NormalizeAwayGoalsScoredValue(AwayGoalsScoredValue);
            AwayGoalsScoredCost = NormalizeAwayGoalsScoredCost(AwayGoalsScoredCost);
            AwayGoalsScoredCV = NormalizeAwayGoalsScoredCV(AwayGoalsScoredCV);
            AwayGoalsConceded = NormalizeAwayGoalsConceded(AwayGoalsConceded);
            AwayGoalsConcededValue = NormalizeAwayGoalsConcededValue(AwayGoalsConcededValue);
            AwayGoalsConcededCost = NormalizeAwayGoalsConcededCost(AwayGoalsConcededCost);
            AwayGoalsConcededCV = NormalizeAwayGoalsConcededCV(AwayGoalsConcededCV);
            AwayOddsCV = NormalizeAwayOddsCV(AwayOddsCV);
            AwayMatchOddsRPS = NormalizeAwayMatchOddsRPS(AwayMatchOddsRPS);
            AwayGoalsRPS = NormalizeAwayGoalsRPS(AwayGoalsRPS);
            AwayBTTSRPS = NormalizeAwayBTTSRPS(AwayBTTSRPS);

            _valuesNormalized = true;
        }

        public int MatchCode { get; set; }

        public string MatchOddsResult { get; set; }
        public string GoalsResult { get; set; }
        public string BttsResult { get; set; }

        public double? PowerPoint { get; set; }
        public double? CVMatchOdds { get; set; }
        public double? HomeOdd { get; set; }
        public double? DrawOdd { get; set; }
        public double? AwayOdd { get; set; }
        public double? Over25Odd { get; set; }
        public double? Under25Odd { get; set; }
        public double? BttsYesOdd { get; set; }
        public double? BttsNoOdd { get; set; }

        #region Values

        #region Home

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

        #endregion

        #region Away

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

        #endregion

        #endregion

        #region DeepCopy

        public MatchBarCode DeepCopy()
        {
            return new MatchBarCode
            {
                MatchCode = this.MatchCode,
                MatchOddsResult = this.MatchOddsResult,
                GoalsResult = this.GoalsResult,
                BttsResult = this.BttsResult,
                PowerPoint = this.PowerPoint,
                CVMatchOdds = this.CVMatchOdds,
                HomeOdd = this.HomeOdd,
                DrawOdd = this.DrawOdd,
                AwayOdd = this.AwayOdd,
                Over25Odd = this.Over25Odd,
                Under25Odd = this.Under25Odd,
                BttsYesOdd = this.BttsYesOdd,
                BttsNoOdd = this.BttsNoOdd,
                HomeCVPoints = this.HomeCVPoints,
                HomePoints = this.HomePoints,
                HomeDifferenceGoals = this.HomeDifferenceGoals,
                HomeCVDifferenceGoals = this.HomeCVDifferenceGoals,
                HomePoisson = this.HomePoisson,
                HomeGoalsScored = this.HomeGoalsScored,
                HomeGoalsScoredValue = this.HomeGoalsScoredValue,
                HomeGoalsScoredCost = this.HomeGoalsScoredCost,
                HomeGoalsScoredCV = this.HomeGoalsScoredCV,
                HomeGoalsConceded = this.HomeGoalsConceded,
                HomeGoalsConcededValue = this.HomeGoalsConcededValue,
                HomeGoalsConcededCost = this.HomeGoalsConcededCost,
                HomeGoalsConcededCV = this.HomeGoalsConcededCV,
                HomeOddsCV = this.HomeOddsCV,
                HomeMatchOddsRPS = this.HomeMatchOddsRPS,
                HomeGoalsRPS = this.HomeGoalsRPS,
                HomeBTTSRPS = this.HomeBTTSRPS,
                AwayCVPoints = this.AwayCVPoints,
                AwayPoints = this.AwayPoints,
                AwayDifferenceGoals = this.AwayDifferenceGoals,
                AwayCVDifferenceGoals = this.AwayCVDifferenceGoals,
                AwayPoisson = this.AwayPoisson,
                AwayGoalsScored = this.AwayGoalsScored,
                AwayGoalsScoredValue = this.AwayGoalsScoredValue,
                AwayGoalsScoredCost = this.AwayGoalsScoredCost,
                AwayGoalsScoredCV = this.AwayGoalsScoredCV,
                AwayGoalsConceded = this.AwayGoalsConceded,
                AwayGoalsConcededValue = this.AwayGoalsConcededValue,
                AwayGoalsConcededCost = this.AwayGoalsConcededCost,
                AwayGoalsConcededCV = this.AwayGoalsConcededCV,
                AwayOddsCV = this.AwayOddsCV,
                AwayMatchOddsRPS = this.AwayMatchOddsRPS,
                AwayGoalsRPS = this.AwayGoalsRPS,
                AwayBTTSRPS = this.AwayBTTSRPS,
                _index = this._index,
                _matchCode = this._matchCode,
                _valuesNormalized = this._valuesNormalized
            };
        }

        #endregion

        #region Methods

        private double? NormalizeHomeOdd(double? value) => NormalizationManager.Instance.HomeOddNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeDrawOdd(double? value) => NormalizationManager.Instance.DrawOddNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayOdd(double? value) => NormalizationManager.Instance.AwayOddNormalizer.Normalize(_matchCode, value, _index);

        private double? NormalizeOver25Odd(double? value) => NormalizationManager.Instance.Over25OddNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeUnder25Odd(double? value) => NormalizationManager.Instance.Under25OddNormalizer.Normalize(_matchCode, value, _index);

        private double? NormalizeBttsYesOdd(double? value) => NormalizationManager.Instance.BttsYesOddNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeBttsNoOdd(double? value) => NormalizationManager.Instance.BttsNoOddNormalizer.Normalize(_matchCode, value, _index);

        private double? NormalizePowerPoint(double? value) => NormalizationManager.Instance.PowerPointNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeCVMatchOdds(double? value) => NormalizationManager.Instance.CVMatchOddsNormalizer.Normalize(_matchCode, value, _index);

        private double? NormalizeHomeCVPoints(double? value) => NormalizationManager.Instance.HomeCVPointsNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeHomePoints(double? value) => NormalizationManager.Instance.HomePointsNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeHomeDifferenceGoals(double? value) => NormalizationManager.Instance.HomeDifferenceGoalsNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeHomeCVDifferenceGoals(double? value) => NormalizationManager.Instance.HomeCVDifferenceGoalsNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeHomePoisson(double? value) => NormalizationManager.Instance.HomePoissonNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeHomeGoalsScored(double? value) => NormalizationManager.Instance.HomeGoalsScoredNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeHomeGoalsScoredValue(double? value) => NormalizationManager.Instance.HomeGoalsScoredValueNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeHomeGoalsScoredCost(double? value) => NormalizationManager.Instance.HomeGoalsScoredCostNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeHomeGoalsScoredCV(double? value) => NormalizationManager.Instance.HomeGoalsScoredCVNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeHomeGoalsConceded(double? value) => NormalizationManager.Instance.HomeGoalsConcededNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeHomeGoalsConcededValue(double? value) => NormalizationManager.Instance.HomeGoalsConcededValueNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeHomeGoalsConcededCost(double? value) => NormalizationManager.Instance.HomeGoalsConcededCostNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeHomeGoalsConcededCV(double? value) => NormalizationManager.Instance.HomeGoalsConcededCVNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeHomeOddsCV(double? value) => NormalizationManager.Instance.HomeOddsCVNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeHomeMatchOddsRPS(double? value) => NormalizationManager.Instance.HomeMatchOddsRPSNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeHomeGoalsRPS(double? value) => NormalizationManager.Instance.HomeGoalsRPSNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeHomeBTTSRPS(double? value) => NormalizationManager.Instance.HomeBTTSRPSNormalizer.Normalize(_matchCode, value, _index);

        private double? NormalizeAwayCVPoints(double? value) => NormalizationManager.Instance.AwayCVPointsNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayPoints(double? value) => NormalizationManager.Instance.AwayPointsNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayDifferenceGoals(double? value) => NormalizationManager.Instance.AwayDifferenceGoalsNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayCVDifferenceGoals(double? value) => NormalizationManager.Instance.AwayCVDifferenceGoalsNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayPoisson(double? value) => NormalizationManager.Instance.AwayPoissonNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayGoalsScored(double? value) => NormalizationManager.Instance.AwayGoalsScoredNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayGoalsScoredValue(double? value) => NormalizationManager.Instance.AwayGoalsScoredValueNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayGoalsScoredCost(double? value) => NormalizationManager.Instance.AwayGoalsScoredCostNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayGoalsScoredCV(double? value) => NormalizationManager.Instance.AwayGoalsScoredCVNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayGoalsConceded(double? value) => NormalizationManager.Instance.AwayGoalsConcededNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayGoalsConcededValue(double? value) => NormalizationManager.Instance.AwayGoalsConcededValueNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayGoalsConcededCost(double? value) => NormalizationManager.Instance.AwayGoalsConcededCostNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayGoalsConcededCV(double? value) => NormalizationManager.Instance.AwayGoalsConcededCVNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayOddsCV(double? value) => NormalizationManager.Instance.AwayOddsCVNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayMatchOddsRPS(double? value) => NormalizationManager.Instance.AwayMatchOddsRPSNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayGoalsRPS(double? value) => NormalizationManager.Instance.AwayGoalsRPSNormalizer.Normalize(_matchCode, value, _index);
        private double? NormalizeAwayBTTSRPS(double? value) => NormalizationManager.Instance.AwayBTTSRPSNormalizer.Normalize(_matchCode, value, _index);

        #endregion
    }
}
