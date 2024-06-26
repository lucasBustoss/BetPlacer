namespace BetPlacer.Punter.API.Utils.Normalization
{
    public class NormalizationManager
    {
        private static NormalizationManager _instance;
        public static NormalizationManager Instance => _instance ?? (_instance = new NormalizationManager());

        public NormalizationManager()
        {
            HomeOddNormalizer = new ValueNormalizer();
            DrawOddNormalizer = new ValueNormalizer();
            AwayOddNormalizer = new ValueNormalizer();

            Over25OddNormalizer = new ValueNormalizer();
            Under25OddNormalizer = new ValueNormalizer();

            BttsYesOddNormalizer = new ValueNormalizer();
            BttsNoOddNormalizer = new ValueNormalizer();

            PowerPointNormalizer = new ValueNormalizer();
            PowerPointHTNormalizer = new ValueNormalizer();
            CVMatchOddsNormalizer = new ValueNormalizer();

            HomeCVPointsNormalizer = new ValueNormalizer();
            HomeCVPointsHTNormalizer = new ValueNormalizer();
            HomePointsNormalizer = new ValueNormalizer();
            HomePointsHTNormalizer = new ValueNormalizer();
            HomeDifferenceGoalsNormalizer = new ValueNormalizer();
            HomeDifferenceGoalsHTNormalizer = new ValueNormalizer();
            HomeCVDifferenceGoalsNormalizer = new ValueNormalizer();
            HomeCVDifferenceGoalsHTNormalizer = new ValueNormalizer();
            HomePoissonNormalizer = new ValueNormalizer();
            HomePoissonHTNormalizer = new ValueNormalizer();
            HomeGoalsScoredNormalizer = new ValueNormalizer();
            HomeGoalsScoredValueNormalizer = new ValueNormalizer();
            HomeGoalsScoredCostNormalizer = new ValueNormalizer();
            HomeGoalsScoredCVNormalizer = new ValueNormalizer();
            HomeGoalsScoredHTNormalizer = new ValueNormalizer();
            HomeGoalsScoredValueHTNormalizer = new ValueNormalizer();
            HomeGoalsScoredCostHTNormalizer = new ValueNormalizer();
            HomeGoalsScoredCVHTNormalizer = new ValueNormalizer();
            HomeGoalsConcededNormalizer = new ValueNormalizer();
            HomeGoalsConcededValueNormalizer = new ValueNormalizer();
            HomeGoalsConcededCostNormalizer = new ValueNormalizer();
            HomeGoalsConcededCVNormalizer = new ValueNormalizer();
            HomeGoalsConcededHTNormalizer = new ValueNormalizer();
            HomeGoalsConcededValueHTNormalizer = new ValueNormalizer();
            HomeGoalsConcededCostHTNormalizer = new ValueNormalizer();
            HomeGoalsConcededCVHTNormalizer = new ValueNormalizer();
            HomeOddsCVNormalizer = new ValueNormalizer();
            HomeMatchOddsRPSNormalizer = new ValueNormalizer();
            HomeMatchOddsHTRPSNormalizer = new ValueNormalizer();
            HomeGoalsRPSNormalizer = new ValueNormalizer();
            HomeBTTSRPSNormalizer = new ValueNormalizer();

            AwayCVPointsNormalizer = new ValueNormalizer();
            AwayCVPointsHTNormalizer = new ValueNormalizer();
            AwayPointsNormalizer = new ValueNormalizer();
            AwayPointsHTNormalizer = new ValueNormalizer();
            AwayDifferenceGoalsNormalizer = new ValueNormalizer();
            AwayDifferenceGoalsHTNormalizer = new ValueNormalizer();
            AwayCVDifferenceGoalsNormalizer = new ValueNormalizer();
            AwayCVDifferenceGoalsHTNormalizer = new ValueNormalizer();
            AwayPoissonNormalizer = new ValueNormalizer();
            AwayPoissonHTNormalizer = new ValueNormalizer();
            AwayGoalsScoredNormalizer = new ValueNormalizer();
            AwayGoalsScoredValueNormalizer = new ValueNormalizer();
            AwayGoalsScoredCostNormalizer = new ValueNormalizer();
            AwayGoalsScoredCVNormalizer = new ValueNormalizer();
            AwayGoalsScoredHTNormalizer = new ValueNormalizer();
            AwayGoalsScoredValueHTNormalizer = new ValueNormalizer();
            AwayGoalsScoredCostHTNormalizer = new ValueNormalizer();
            AwayGoalsScoredCVHTNormalizer = new ValueNormalizer();
            AwayGoalsConcededNormalizer = new ValueNormalizer();
            AwayGoalsConcededValueNormalizer = new ValueNormalizer();
            AwayGoalsConcededCostNormalizer = new ValueNormalizer();
            AwayGoalsConcededCVNormalizer = new ValueNormalizer();
            AwayGoalsConcededHTNormalizer = new ValueNormalizer();
            AwayGoalsConcededValueHTNormalizer = new ValueNormalizer();
            AwayGoalsConcededCostHTNormalizer = new ValueNormalizer();
            AwayGoalsConcededCVHTNormalizer = new ValueNormalizer();
            AwayOddsCVNormalizer = new ValueNormalizer();
            AwayMatchOddsRPSNormalizer = new ValueNormalizer();
            AwayMatchOddsHTRPSNormalizer = new ValueNormalizer();
            AwayGoalsRPSNormalizer = new ValueNormalizer();
            AwayBTTSRPSNormalizer = new ValueNormalizer();
        }

        public ValueNormalizer HomeOddNormalizer { get; }
        public ValueNormalizer DrawOddNormalizer { get; }
        public ValueNormalizer AwayOddNormalizer { get; }

        public ValueNormalizer Over25OddNormalizer { get; }
        public ValueNormalizer Under25OddNormalizer { get; }

        public ValueNormalizer BttsYesOddNormalizer { get; }
        public ValueNormalizer BttsNoOddNormalizer { get; }

        public ValueNormalizer PowerPointNormalizer { get; }
        public ValueNormalizer PowerPointHTNormalizer { get; }
        public ValueNormalizer CVMatchOddsNormalizer { get; }

        public ValueNormalizer HomeCVPointsNormalizer { get; }
        public ValueNormalizer HomeCVPointsHTNormalizer { get; }
        public ValueNormalizer HomePointsNormalizer { get; }
        public ValueNormalizer HomePointsHTNormalizer { get; }
        public ValueNormalizer HomeDifferenceGoalsNormalizer { get; }
        public ValueNormalizer HomeDifferenceGoalsHTNormalizer { get; }
        public ValueNormalizer HomeCVDifferenceGoalsNormalizer { get; }
        public ValueNormalizer HomeCVDifferenceGoalsHTNormalizer { get; }
        public ValueNormalizer HomePoissonNormalizer { get; }
        public ValueNormalizer HomePoissonHTNormalizer { get; }
        public ValueNormalizer HomeGoalsScoredNormalizer { get; }
        public ValueNormalizer HomeGoalsScoredValueNormalizer { get; }
        public ValueNormalizer HomeGoalsScoredCostNormalizer { get; }
        public ValueNormalizer HomeGoalsScoredCVNormalizer { get; }
        public ValueNormalizer HomeGoalsScoredHTNormalizer { get; }
        public ValueNormalizer HomeGoalsScoredValueHTNormalizer { get; }
        public ValueNormalizer HomeGoalsScoredCostHTNormalizer { get; }
        public ValueNormalizer HomeGoalsScoredCVHTNormalizer { get; }
        public ValueNormalizer HomeGoalsConcededNormalizer { get; }
        public ValueNormalizer HomeGoalsConcededValueNormalizer { get; }
        public ValueNormalizer HomeGoalsConcededCostNormalizer { get; }
        public ValueNormalizer HomeGoalsConcededCVNormalizer { get; }
        public ValueNormalizer HomeGoalsConcededHTNormalizer { get; }
        public ValueNormalizer HomeGoalsConcededValueHTNormalizer { get; }
        public ValueNormalizer HomeGoalsConcededCostHTNormalizer { get; }
        public ValueNormalizer HomeGoalsConcededCVHTNormalizer { get; }
        public ValueNormalizer HomeOddsCVNormalizer { get; }
        public ValueNormalizer HomeMatchOddsRPSNormalizer { get; }
        public ValueNormalizer HomeMatchOddsHTRPSNormalizer { get; }
        public ValueNormalizer HomeGoalsRPSNormalizer { get; }
        public ValueNormalizer HomeBTTSRPSNormalizer { get; }

        public ValueNormalizer AwayCVPointsNormalizer { get; }
        public ValueNormalizer AwayCVPointsHTNormalizer { get; }
        public ValueNormalizer AwayPointsNormalizer { get; }
        public ValueNormalizer AwayPointsHTNormalizer { get; }
        public ValueNormalizer AwayDifferenceGoalsNormalizer { get; }
        public ValueNormalizer AwayDifferenceGoalsHTNormalizer { get; }
        public ValueNormalizer AwayCVDifferenceGoalsNormalizer { get; }
        public ValueNormalizer AwayCVDifferenceGoalsHTNormalizer { get; }
        public ValueNormalizer AwayPoissonNormalizer { get; }
        public ValueNormalizer AwayPoissonHTNormalizer { get; }
        public ValueNormalizer AwayGoalsScoredNormalizer { get; }
        public ValueNormalizer AwayGoalsScoredValueNormalizer { get; }
        public ValueNormalizer AwayGoalsScoredCostNormalizer { get; }
        public ValueNormalizer AwayGoalsScoredCVNormalizer { get; }
        public ValueNormalizer AwayGoalsScoredHTNormalizer { get; }
        public ValueNormalizer AwayGoalsScoredValueHTNormalizer { get; }
        public ValueNormalizer AwayGoalsScoredCostHTNormalizer { get; }
        public ValueNormalizer AwayGoalsScoredCVHTNormalizer { get; }
        public ValueNormalizer AwayGoalsConcededNormalizer { get; }
        public ValueNormalizer AwayGoalsConcededValueNormalizer { get; }
        public ValueNormalizer AwayGoalsConcededCostNormalizer { get; }
        public ValueNormalizer AwayGoalsConcededCVNormalizer { get; }
        public ValueNormalizer AwayGoalsConcededHTNormalizer { get; }
        public ValueNormalizer AwayGoalsConcededValueHTNormalizer { get; }
        public ValueNormalizer AwayGoalsConcededCostHTNormalizer { get; }
        public ValueNormalizer AwayGoalsConcededCVHTNormalizer { get; }
        public ValueNormalizer AwayOddsCVNormalizer { get; }
        public ValueNormalizer AwayMatchOddsRPSNormalizer { get; }
        public ValueNormalizer AwayMatchOddsHTRPSNormalizer { get; }
        public ValueNormalizer AwayGoalsRPSNormalizer { get; }
        public ValueNormalizer AwayBTTSRPSNormalizer { get; }

    }
}
