using BetPlacer.Backtest.API.Models;
using BetPlacer.Backtest.API.Models.Entities;
using BetPlacer.Backtest.API.Models.Entities.Odds;
using BetPlacer.Backtest.API.Models.Enums;
using BetPlacer.Backtest.API.Models.Filters;
using BetPlacer.Backtest.API.Utils;
using BetPlacer.Core.Models.Response.Microservice.Leagues;
using BetPlacer.Core.Models.Response.Microservice.Teams;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures;
using System.Reflection;

namespace BetPlacer.Backtest.API.Services
{
    public class CalculateBacktest : ICalculateBacktest
    {
        BacktestParameters _parameters;
        List<LeaguesApiResponseModel> _leagues;
        List<TeamsApiResponseModel> _teams;

        private int _totalFixtures;
        private int _matchedFixtures;
        private int _filteredFixtures;
        private int _goodRun;
        private int _badRun;
        private int _maxGoodRun;
        private int _maxBadRun;
        private Dictionary<int, int> _leagueCounts;
        private Dictionary<int, int> _leagueSeasonCounts;
        private Dictionary<int, int> _teamCount;

        public CalculateBacktest(BacktestParameters parameters, List<LeaguesApiResponseModel> leagues, List<TeamsApiResponseModel> teams)
        {
            _parameters = parameters;
            _leagues = leagues;
            _teams = teams;

            _totalFixtures = 0;
            _matchedFixtures = 0;
            _filteredFixtures = 0;
            _goodRun = 0;
            _badRun = 0;
            _maxGoodRun = 0;
            _maxBadRun = 0;

            _leagueCounts = new Dictionary<int, int>();
            _leagueSeasonCounts = new Dictionary<int, int>();
            _teamCount = new Dictionary<int, int>();
        }

        public void CalculateFixture(FixturesApiResponseModel fixture)
        {
            _totalFixtures++;

            List<FixtureGoalsApiResponseModel> goalsFixtures = fixture.Goals != null ? fixture.Goals : new List<FixtureGoalsApiResponseModel>();

            bool matchedFilterFixture = GetResultFilterFixture(fixture, _parameters.Filters);
            bool matchedResultFixture = GetResultFixture(goalsFixtures, fixture.HomeTeamCode, fixture.AwayTeamCode, _parameters.ResultTeamType, _parameters.ResultType);

            if (matchedFilterFixture)
            {
                 _filteredFixtures++;

                if (matchedResultFixture)
                {
                    _matchedFixtures++;

                    int leagueCode = fixture.LeagueCode;
                    int leagueSeasonCode = fixture.LeagueSeasonCode;
                    int homeTeamCode = fixture.HomeTeamCode;
                    int awayTeamCode = fixture.AwayTeamCode;

                    IncrementCounts(_leagueCounts, leagueCode);
                    IncrementCounts(_leagueSeasonCounts, leagueSeasonCode);
                    IncrementCounts(_teamCount, homeTeamCode);
                    IncrementCounts(_teamCount, awayTeamCode);

                    _goodRun++;

                    if (_badRun > _maxBadRun)
                        _maxBadRun = _badRun;

                    _badRun = 0;
                }
                else
                {
                    _badRun++;

                    if (_goodRun > _maxGoodRun)
                        _maxGoodRun = _goodRun;

                    _goodRun = 0;
                }
            }
        }

        public BacktestModel GenerateResult()
        {
            BacktestModel backtest = new BacktestModel(true);
            backtest.Name = _parameters.Name;
            backtest.FilteredFixtures = _totalFixtures > 0 ? Math.Round((double)_filteredFixtures / _totalFixtures, 4) : 0;
            backtest.MatchedFixtures = _filteredFixtures > 0 ? Math.Round((double)_matchedFixtures / _filteredFixtures, 4) : 0;
            backtest.TeamType = (int)_parameters.ResultTeamType;
            backtest.Type = (int)_parameters.ResultType;
            backtest.MaxGoodRun = _maxGoodRun;
            backtest.MaxBadRun = _maxBadRun;
            backtest.UsesInFixture = false;

            #region Filters

            Type type = _parameters.Filters.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                object propertyValue = property.GetValue(_parameters.Filters);

                if (propertyValue == null)
                    continue;

                PropertyInfo[] subProperties = propertyValue.GetType().GetProperties();
                BacktestFilterModel backtestFilter = new BacktestFilterModel
                {
                    Name = GetPropertyName(propertyName)
                };

                foreach (PropertyInfo subProperty in subProperties)
                {
                    string subPropertyName = subProperty.Name;
                    object subPropertyValue = subProperty.GetValue(propertyValue);

                    switch (subPropertyName)
                    {
                        case "CompareType":
                            backtestFilter.CompareType = (int)subPropertyValue;
                            break;
                        case "TeamType":
                            backtestFilter.TeamType = (int)subPropertyValue;
                            break;
                        case "PropType":
                            backtestFilter.PropType = (int)subPropertyValue;
                            break;
                        case "InitialValue":
                            backtestFilter.InitialValue = (double)subPropertyValue;
                            break;
                        case "FinalValue":
                            backtestFilter.FinalValue = (double)subPropertyValue;
                            break;
                    }
                }

                backtest.Filters.Add(backtestFilter);
            }

            #endregion

            return backtest;
        }


        #region Private methods

        private bool GetResultFilterFixture(FixturesApiResponseModel fixture, BacktestFilters filters)
        {
            bool matchedFilterFixture = true;

            if (filters != null)
            {
                #region FTS

                if (filters.FtsFilter != null)
                {
                    FilterValue ftsFilter = filters.FtsFilter;
                    var ftsPropertyMapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                    {
                        {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeFirstToScorePercentTotal"},
                        {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeFirstToScorePercentAtHome"},
                        {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayFirstToScorePercentTotal"},
                        {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayFirstToScorePercentAtAway"}
                    };

                    string ftsPath = ftsPropertyMapping[(ftsFilter.TeamType, ftsFilter.PropType)];

                    var ftsPredicate = FilterPredicateBuilding.BuildPredicate(ftsPath, ftsFilter.InitialValue, ftsFilter.FinalValue, ftsFilter.CompareType);
                    matchedFilterFixture = ftsPredicate(fixture);

                    if (!matchedFilterFixture)
                        return false;
                }

                #endregion

                #region ToScoreTwoZero

                if (filters.TwoZeroFilter != null)
                {
                    FilterValue twoZeroFilter = filters.TwoZeroFilter;
                    var twoZeroPropertyMapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                    {
                        {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeToScoreTwoZeroPercentTotal"},
                        {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeToScoreTwoZeroPercentAtHome"},
                        {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayToScoreTwoZeroPercentTotal"},
                        {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayToScoreTwoZeroPercentAtAway"}
                    };

                    string twoZeroPath = twoZeroPropertyMapping[(twoZeroFilter.TeamType, twoZeroFilter.PropType)];

                    var twoZeroPredicate = FilterPredicateBuilding.BuildPredicate(twoZeroPath, twoZeroFilter.InitialValue, twoZeroFilter.FinalValue, twoZeroFilter.CompareType);
                    matchedFilterFixture = twoZeroPredicate(fixture);

                    if (!matchedFilterFixture)
                        return false;
                }

                #endregion

                #region CleanSheets

                if (filters.CleanSheetsFilter != null)
                {
                    FilterValue cleanSheetFilter = filters.CleanSheetsFilter;
                    var cleanSheetPropertyMapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                    {
                        {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeCleanSheetsPercentTotal"},
                        {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeCleanSheetsPercentAtHome"},
                        {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayCleanSheetsPercentTotal"},
                        {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayCleanSheetsPercentAtAway"}
                    };

                    string cleanSheetPath = cleanSheetPropertyMapping[(cleanSheetFilter.TeamType, cleanSheetFilter.PropType)];

                    var cleanSheetPredicate = FilterPredicateBuilding.BuildPredicate(cleanSheetPath, cleanSheetFilter.InitialValue, cleanSheetFilter.FinalValue, cleanSheetFilter.CompareType);
                    matchedFilterFixture = cleanSheetPredicate(fixture);

                    if (!matchedFilterFixture)
                        return false;
                }

                #endregion

                #region FailedToScore

                if (filters.FailedToScoreFilter != null)
                {
                    FilterValue failedToScoreFilter = filters.FailedToScoreFilter;
                    var failedToScorePropertyMapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                    {
                        {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeFailedToScorePercentTotal"},
                        {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeFailedToScorePercentAtHome"},
                        {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayFailedToScorePercentTotal"},
                        {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayFailedToScorePercentAtAway"}
                    };

                    string failedToScorePath = failedToScorePropertyMapping[(failedToScoreFilter.TeamType, failedToScoreFilter.PropType)];

                    var failedToScorePredicate = FilterPredicateBuilding.BuildPredicate(failedToScorePath, failedToScoreFilter.InitialValue, failedToScoreFilter.FinalValue, failedToScoreFilter.CompareType);
                    matchedFilterFixture = failedToScorePredicate(fixture);

                    if (!matchedFilterFixture)
                        return false;
                }

                #endregion

                #region BothToScore

                if (filters.BothToScoreFilter != null)
                {
                    FilterValue bothToScoreFilter = filters.BothToScoreFilter;
                    var bothToScorePropertyMapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                    {
                        {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeBothToScorePercentTotal"},
                        {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeBothToScorePercentAtHome"},
                        {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayBothToScorePercentTotal"},
                        {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayBothToScorePercentAtAway"}
                    };

                    string bothToScorePath = bothToScorePropertyMapping[(bothToScoreFilter.TeamType, bothToScoreFilter.PropType)];

                    var bothToScorePredicate = FilterPredicateBuilding.BuildPredicate(bothToScorePath, bothToScoreFilter.InitialValue, bothToScoreFilter.FinalValue, bothToScoreFilter.CompareType);
                    matchedFilterFixture = bothToScorePredicate(fixture);

                    if (!matchedFilterFixture)
                        return false;
                }

                #endregion

                #region AverageGoalsScored

                if (filters.AverageGoalsScoredFilter != null)
                {
                    FilterValue averageGoalsScoredFilter = filters.AverageGoalsScoredFilter;
                    var averageGoalsScoredPropertyMapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                    {
                        {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeAverageGoalsScoredTotal"},
                        {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeAverageGoalsScoredAtHome"},
                        {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayAverageGoalsScoredTotal"},
                        {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayAverageGoalsScoredAtAway"}
                    };

                    string averageGoalsScoredPath = averageGoalsScoredPropertyMapping[(averageGoalsScoredFilter.TeamType, averageGoalsScoredFilter.PropType)];

                    var averageGoalsScoredPredicate = FilterPredicateBuilding.BuildPredicate(averageGoalsScoredPath, averageGoalsScoredFilter.InitialValue, averageGoalsScoredFilter.FinalValue, averageGoalsScoredFilter.CompareType);
                    matchedFilterFixture = averageGoalsScoredPredicate(fixture);

                    if (!matchedFilterFixture)
                        return false;
                }

                #endregion

                #region AverageGoalsConceded

                if (filters.AverageGoalsConcededFilter != null)
                {
                    FilterValue averageGoalsConcededFilter = filters.AverageGoalsConcededFilter;
                    var averageGoalsConcededPropertyMapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                    {
                        {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeAverageGoalsConcededTotal"},
                        {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeAverageGoalsConcededAtHome"},
                        {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayAverageGoalsConcededTotal"},
                        {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayAverageGoalsConcededAtAway"}
                    };

                    string averageGoalsConcededPath = averageGoalsConcededPropertyMapping[(averageGoalsConcededFilter.TeamType, averageGoalsConcededFilter.PropType)];

                    var averageGoalsConcededPredicate = FilterPredicateBuilding.BuildPredicate(averageGoalsConcededPath, averageGoalsConcededFilter.InitialValue, averageGoalsConcededFilter.FinalValue, averageGoalsConcededFilter.CompareType);
                    matchedFilterFixture = averageGoalsConcededPredicate(fixture);

                    if (!matchedFilterFixture)
                        return false;
                }

                #endregion

                #region FTSHT

                if (filters.FtsHTFilter != null)
                {
                    FilterValue ftsHTFilter = filters.FtsHTFilter;
                    var ftsHTPropertyMapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                {
                    {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeFirstToScorePercentHTTotal"},
                    {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeFirstToScorePercentHTAtHome"},
                    {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayFirstToScorePercentHTTotal"},
                    {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayFirstToScorePercentHTAtAway"}
                };

                    string ftsHTPath = ftsHTPropertyMapping[(ftsHTFilter.TeamType, ftsHTFilter.PropType)];

                    var ftsPredicate = FilterPredicateBuilding.BuildPredicate(ftsHTPath, ftsHTFilter.InitialValue, ftsHTFilter.FinalValue, ftsHTFilter.CompareType);
                    matchedFilterFixture = ftsPredicate(fixture);

                    if (!matchedFilterFixture)
                        return false;
                }

                #endregion

                #region ToScoreTwoZeroHT

                if (filters.TwoZeroHTFilter != null)
                {
                    FilterValue twoZeroHTFilter = filters.TwoZeroHTFilter;
                    var twoZeroHTPropertyMapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                    {
                        {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeToScoreTwoZeroPercentHTTotal"},
                        {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeToScoreTwoZeroPercentHTAtHome"},
                        {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayToScoreTwoZeroPercentHTTotal"},
                        {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayToScoreTwoZeroPercentHTAtAway"}
                    };

                    string twoZeroHTPath = twoZeroHTPropertyMapping[(twoZeroHTFilter.TeamType, twoZeroHTFilter.PropType)];

                    var twoZeroHTPredicate = FilterPredicateBuilding.BuildPredicate(twoZeroHTPath, twoZeroHTFilter.InitialValue, twoZeroHTFilter.FinalValue, twoZeroHTFilter.CompareType);
                    matchedFilterFixture = twoZeroHTPredicate(fixture);

                    if (!matchedFilterFixture)
                        return false;
                }

                #endregion

                #region CleanSheetsHT

                if (filters.CleanSheetsHTFilter != null)
                {
                    FilterValue cleanSheetHTFilter = filters.CleanSheetsHTFilter;
                    var cleanSheetHTPropertyMapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                    {
                        {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeCleanSheetsPercentHTTotal"},
                        {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeCleanSheetsPercentAtHTHome"},
                        {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayCleanSheetsPercentHTTotal"},
                        {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayCleanSheetsPercentHTAtAway"}
                    };

                    string cleanSheetHTPath = cleanSheetHTPropertyMapping[(cleanSheetHTFilter.TeamType, cleanSheetHTFilter.PropType)];

                    var cleanSheetHTPredicate = FilterPredicateBuilding.BuildPredicate(cleanSheetHTPath, cleanSheetHTFilter.InitialValue, cleanSheetHTFilter.FinalValue, cleanSheetHTFilter.CompareType);
                    matchedFilterFixture = cleanSheetHTPredicate(fixture);

                    if (!matchedFilterFixture)
                        return false;
                }

                #endregion

                #region FailedToScoreHT

                if (filters.FailedToScoreHTFilter != null)
                {
                    FilterValue failedToScoreHTFilter = filters.FailedToScoreHTFilter;
                    var failedToScoreHTPropertyMapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                    {
                        {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeFailedToScorePercentHTTotal"},
                        {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeFailedToScorePercentHTAtHome"},
                        {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayFailedToScorePercentHTTotal"},
                        {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayFailedToScorePercentHTAtAway"}
                    };

                    string failedToScoreHTPath = failedToScoreHTPropertyMapping[(failedToScoreHTFilter.TeamType, failedToScoreHTFilter.PropType)];

                    var failedToScoreHTPredicate = FilterPredicateBuilding.BuildPredicate(failedToScoreHTPath, failedToScoreHTFilter.InitialValue, failedToScoreHTFilter.FinalValue, failedToScoreHTFilter.CompareType);
                    matchedFilterFixture = failedToScoreHTPredicate(fixture);

                    if (!matchedFilterFixture)
                        return false;
                }

                #endregion

                #region BothToScoreHT

                if (filters.BothToScoreHTFilter != null)
                {
                    FilterValue bothToScoreHTFilter = filters.BothToScoreHTFilter;
                    var bothToScoreHTPropertyMapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                    {
                        {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeBothToScorePercentHTTotal"},
                        {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeBothToScorePercentHTAtHome"},
                        {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayBothToScorePercentHTTotal"},
                        {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayBothToScorePercentHTAtAway"}
                    };

                    string bothToScoreHTPath = bothToScoreHTPropertyMapping[(bothToScoreHTFilter.TeamType, bothToScoreHTFilter.PropType)];

                    var bothToScoreHTPredicate = FilterPredicateBuilding.BuildPredicate(bothToScoreHTPath, bothToScoreHTFilter.InitialValue, bothToScoreHTFilter.FinalValue, bothToScoreHTFilter.CompareType);
                    matchedFilterFixture = bothToScoreHTPredicate(fixture);

                    if (!matchedFilterFixture)
                        return false;
                }


                #endregion

                #region AverageGoalsScoredHT

                if (filters.AverageGoalsScoredFilter != null)
                {
                    FilterValue averageGoalsScoredHTFilter = filters.AverageGoalsScoredHTFilter;
                    var averageGoalsScoredHTPropertyMapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                    {
                        {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeAverageGoalsScoredHTTotal"},
                        {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeAverageGoalsScoredHTAtHome"},
                        {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayAverageGoalsScoredHTTotal"},
                        {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayAverageGoalsScoredHTAtAway"}
                    };

                    string averageGoalsScoredHTPath = averageGoalsScoredHTPropertyMapping[(averageGoalsScoredHTFilter.TeamType, averageGoalsScoredHTFilter.PropType)];

                    var averageGoalsScoredHTPredicate = FilterPredicateBuilding.BuildPredicate(averageGoalsScoredHTPath, averageGoalsScoredHTFilter.InitialValue, averageGoalsScoredHTFilter.FinalValue, averageGoalsScoredHTFilter.CompareType);
                    matchedFilterFixture = averageGoalsScoredHTPredicate(fixture);

                    if (!matchedFilterFixture)
                        return false;
                }

                #endregion

                #region AverageGoalsConcededHT

                if (filters.AverageGoalsConcededHTFilter != null)
                {
                    FilterValue averageGoalsConcededHTFilter = filters.AverageGoalsConcededHTFilter;
                    var averageGoalsConcededHTPropertyMapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                    {
                        {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeAverageGoalsConcededHTTotal"},
                        {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeAverageGoalsConcededHTAtHome"},
                        {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayAverageGoalsConcededHTTotal"},
                        {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayAverageGoalsConcededHTAtAway"}
                    };

                    string averageGoalsConcededHTPath = averageGoalsConcededHTPropertyMapping[(averageGoalsConcededHTFilter.TeamType, averageGoalsConcededHTFilter.PropType)];

                    var averageGoalsConcededHTPredicate = FilterPredicateBuilding.BuildPredicate(averageGoalsConcededHTPath, averageGoalsConcededHTFilter.InitialValue, averageGoalsConcededHTFilter.FinalValue, averageGoalsConcededHTFilter.CompareType);
                    matchedFilterFixture = averageGoalsConcededHTPredicate(fixture);

                    if (!matchedFilterFixture)
                        return false;
                }

                #endregion


            }

            return matchedFilterFixture;
        }

        private bool GetResultFixture(List<FixtureGoalsApiResponseModel> goals, int homeTeamCode, int awayTeamCode, ResultTeamType resultTeamType, ResultType resultType)
        {
            List<double> homeTeamGoals = goals.Where(g => g.TeamId == homeTeamCode).OrderBy(g => g.Minute).Select(g => Convert.ToDouble(g.Minute)).ToList();
            List<double> awayTeamGoals = goals.Where(g => g.TeamId == awayTeamCode).OrderBy(g => g.Minute).Select(g => Convert.ToDouble(g.Minute)).ToList();
            List<double> homeTeamHTGoals = goals.Where(g => g.TeamId == homeTeamCode && Convert.ToDouble(g.Minute) < 46).OrderBy(g => g.Minute).Select(g => Convert.ToDouble(g.Minute)).ToList();
            List<double> awayTeamHTGoals = goals.Where(g => g.TeamId == awayTeamCode && Convert.ToDouble(g.Minute) < 46).OrderBy(g => g.Minute).Select(g => Convert.ToDouble(g.Minute)).ToList();

            #region First Score

            if (resultType == ResultType.FirstScore)
            {
                double homeTeamFirstGoal = homeTeamGoals.Count > 0 ? homeTeamGoals[0] : -1;
                double awayTeamFirstGoal = awayTeamGoals.Count > 0 ? awayTeamGoals[0] : -1;

                if (homeTeamFirstGoal == -1 && awayTeamFirstGoal == -1)
                    return false;

                if (resultTeamType == ResultTeamType.HomeTeam)
                    return awayTeamFirstGoal == -1 || homeTeamFirstGoal < awayTeamFirstGoal;

                return homeTeamFirstGoal == -1 || awayTeamFirstGoal < homeTeamFirstGoal;
            }

            #endregion

            #region First Score in HT

            if (resultType == ResultType.FirstScoreHT)
            {
                double homeTeamFirstGoalHT = homeTeamHTGoals.Count > 0 ? homeTeamHTGoals[0] : -1;
                double awayTeamFirstGoalHT = awayTeamHTGoals.Count > 0 ? awayTeamHTGoals[0] : -1;

                if (homeTeamFirstGoalHT == -1 && awayTeamFirstGoalHT == -1)
                    return false;

                if (resultTeamType == ResultTeamType.HomeTeam)
                    return awayTeamFirstGoalHT == -1 || homeTeamFirstGoalHT < awayTeamFirstGoalHT;

                return homeTeamFirstGoalHT == -1 || awayTeamFirstGoalHT < homeTeamFirstGoalHT;
            }

            #endregion

            #region FirstToScoreTwoGoals

            if (resultType == ResultType.FirstToScoreTwoGoals)
            {
                double homeTeamFirstGoal = homeTeamGoals.Count > 0 ? homeTeamGoals[0] : -1;
                double homeTeamSecondGoal = homeTeamGoals.Count > 1 ? homeTeamGoals[1] : -1;
                double awayTeamFirstGoal = awayTeamGoals.Count > 0 ? awayTeamGoals[0] : -1;
                double awayTeamSecondGoal = awayTeamGoals.Count > 1 ? awayTeamGoals[1] : -1;

                if (homeTeamFirstGoal == -1 && awayTeamFirstGoal == -1)
                    return false;

                if (resultTeamType == ResultTeamType.HomeTeam)
                    return homeTeamSecondGoal > -1 || homeTeamSecondGoal < awayTeamFirstGoal;

                return awayTeamSecondGoal > -1 || awayTeamSecondGoal < homeTeamFirstGoal;
            }

            #endregion

            #region FirstToScoreTwoGoalsHT

            if (resultType == ResultType.FirstToScoreTwoGoalsHT)
            {
                double homeTeamFirstGoalHT = homeTeamHTGoals.Count > 0 ? homeTeamHTGoals[0] : -1;
                double homeTeamSecondGoalHT = homeTeamHTGoals.Count > 1 ? homeTeamHTGoals[1] : -1;
                double awayTeamFirstGoalHT = awayTeamHTGoals.Count > 0 ? awayTeamHTGoals[0] : -1;
                double awayTeamSecondGoalHT = awayTeamHTGoals.Count > 1 ? awayTeamHTGoals[1] : -1;

                if (homeTeamFirstGoalHT == -1 && awayTeamFirstGoalHT == -1)
                    return false;

                if (resultTeamType == ResultTeamType.HomeTeam)
                    return homeTeamSecondGoalHT > -1 || homeTeamSecondGoalHT < awayTeamFirstGoalHT;

                return awayTeamSecondGoalHT > -1 || awayTeamSecondGoalHT < homeTeamFirstGoalHT;
            }

            #endregion

            #region ToWinHT

            if (resultType == ResultType.ToWinHT)
            {
                if (resultTeamType == ResultTeamType.HomeTeam)
                    return homeTeamHTGoals.Count > awayTeamHTGoals.Count;

                return awayTeamHTGoals.Count > homeTeamHTGoals.Count;
            }

            #endregion

            #region ToWinFT

            if (resultType == ResultType.ToWinFT)
            {
                if (resultTeamType == ResultTeamType.HomeTeam)
                    return homeTeamGoals.Count > awayTeamGoals.Count;

                return awayTeamGoals.Count > homeTeamGoals.Count;
            }

            #endregion

            return false;
        }

        private string GetPropertyName(string property)
        {
            string name;

            switch (property)
            {
                case "FtsFilter":
                    name = "% de jogos sendo primeiro a marcar";
                    break;
                case "TwoZeroFilter":
                    name = "% de jogos sendo primeiro a marcar 2x0";
                    break;
                case "CleanSheetsFilter":
                    name = "% de jogos sem sofrer gols";
                    break;
                case "FailedToScoreFilter":
                    name = "% de jogos em que não marcou gols";
                    break;
                case "BothToScoreFilter":
                    name = "% de jogos em que os dois times marcaram";
                    break;
                case "AverageGoalsScoredFilter":
                    name = "Média de gols marcados";
                    break;
                case "AverageGoalsConcededFilter":
                    name = "Média de gols sofridos";
                    break;
                case "FtsHTFilter":
                    name = "% de jogos sendo primeiro a marcar no HT";
                    break;
                case "TwoZeroHTFilter":
                    name = "% de jogos sendo primeiro a marcar 2x0 no HT";
                    break;
                case "CleanSheetsHTFilter":
                    name = "% de jogos sem sofrer gols no HT";
                    break;
                case "FailedToScoreHTFilter":
                    name = "% de jogos em que não marcou gols no HT";
                    break;
                case "BothToScoreHTFilter":
                    name = "% de jogos em que os dois times marcaram no HT";
                    break;
                case "AverageGoalsScoredHTFilter":
                    name = "Média de gols marcados no HT";
                    break;
                case "AverageGoalsConcededHTFilter":
                    name = "Média de gols sofridos no HT";
                    break;
                default:
                    name = "";
                    break;
            }

            return name;
        }

        private void IncrementCounts(Dictionary<int, int> dictionary, int key)
        {
            if (!dictionary.ContainsKey(key))
                dictionary[key] = 0;

            dictionary[key]++;
        }

        #endregion
    }
}
