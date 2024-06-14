using BetPlacer.Backtest.API.Models;
using BetPlacer.Backtest.API.Models.Enums;
using BetPlacer.Backtest.API.Models.Filters;
using BetPlacer.Backtest.API.Models.ValueObjects;
using BetPlacer.Core.Models.Response.Microservice.Leagues;
using BetPlacer.Core.Models.Response.Microservice.Teams;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures;

namespace BetPlacer.Backtest.API.Services
{
    class PrimeiroGol
    {
        public int FixtureCode { get; set; }
        public double HomeFirstGoal { get; set; }
        public double AwayFirstGoal { get; set; }
        public double HomeFirstToScore { get; set; }
        public double AwayFirstToScore { get; set; }
        public int HomeCount { get; set; }
        public int AwayCount { get; set; }
    }


    public class CalculateBacktest : ICalculateBacktest
    {
        BacktestParameters _parameters;
        List<LeaguesApiResponseModel> _leagues;
        List<TeamsApiResponseModel> _teams;

        private int _totalFixtures;
        private int _matchedFixtures;
        private int _noGoalFixtures;
        private int _goalConcededFixtures;
        private int _filteredFixtures;
        private int _goodRun;
        private int _badRun;
        private int _maxGoodRun;
        private int _maxBadRun;
        private Dictionary<int, int> _leagueCounts;
        private Dictionary<int, int> _leagueSeasonCounts;
        private Dictionary<int, int> _teamCount;
        private List<PrimeiroGol> primeiros = new List<PrimeiroGol>();
        private List<PrimeiroGol> nao_filtrados = new List<PrimeiroGol>();
        private List<PrimeiroGol> nao_deu_match = new List<PrimeiroGol>();

        public CalculateBacktest(BacktestParameters parameters, List<LeaguesApiResponseModel> leagues, List<TeamsApiResponseModel> teams)
        {
            _parameters = parameters;
            _leagues = leagues;
            _teams = teams;

            _totalFixtures = 0;
            _matchedFixtures = 0;
            _filteredFixtures = 0;
            _noGoalFixtures = 0;
            _goalConcededFixtures = 0;
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
            bool matchedResultFixture = GetResultFixture(fixture, matchedFilterFixture, goalsFixtures, fixture.HomeTeamCode, fixture.AwayTeamCode, _parameters.ResultTeamType, _parameters.ResultType);

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
                    AdditionalInformationFixture(goalsFixtures, fixture.HomeTeamCode, fixture.AwayTeamCode, _parameters.ResultTeamType, _parameters.ResultType);

                    _badRun++;

                    if (_goodRun > _maxGoodRun)
                        _maxGoodRun = _goodRun;

                    _goodRun = 0;
                }
            }
        }

        public BacktestVO GenerateResult()
        {
            var aaa = primeiros.OrderBy(p => p.FixtureCode).DistinctBy(p => p.FixtureCode).ToList();
            var bbb = nao_deu_match.OrderBy(p => p.HomeFirstToScore).ThenBy(p => p.AwayFirstToScore).ThenBy(p => p.HomeFirstGoal).ThenBy(p => p.AwayFirstGoal).ThenBy(p => p.HomeCount).ThenBy(p => p.AwayCount).DistinctBy(p => p.FixtureCode).ToList();
            var ccc = nao_filtrados.Where(p => p.HomeCount > 3 && p.AwayCount > 3 && p.HomeFirstGoal > -1).OrderBy(p => p.HomeFirstToScore).ThenBy(p => p.AwayFirstToScore).ThenBy(p => p.HomeFirstGoal).ThenBy(p => p.AwayFirstGoal).ThenBy(p => p.HomeCount).ThenBy(p => p.AwayCount).DistinctBy(p => p.FixtureCode).ToList();
            var ddd = nao_filtrados.Where(p => p.HomeCount > 3 && p.AwayCount > 3).ToList();
            var eee = nao_filtrados.Where(p => p.HomeCount > 3 && p.AwayCount > 3 && p.HomeFirstGoal > -1 && p.HomeFirstToScore > (p.AwayFirstToScore * 1.5)).OrderBy(p => p.HomeFirstToScore).ThenBy(p => p.AwayFirstToScore).ThenBy(p => p.HomeFirstGoal).ThenBy(p => p.AwayFirstGoal).ThenBy(p => p.HomeCount).ThenBy(p => p.AwayCount).DistinctBy(p => p.FixtureCode).ToList();
            var fff = nao_deu_match.Where(p => p.HomeFirstGoal > -1).OrderBy(p => p.HomeFirstToScore).ThenBy(p => p.AwayFirstToScore).ThenBy(p => p.HomeFirstGoal).ThenBy(p => p.AwayFirstGoal).ThenBy(p => p.HomeCount).ThenBy(p => p.AwayCount).DistinctBy(p => p.FixtureCode).ToList();

            BacktestVO backtest = new BacktestVO();
            backtest.Name = _parameters.Name;
            backtest.UserId = 1;
            backtest.CreationDate = DateTime.UtcNow;
            backtest.FilteredFixtures = _totalFixtures > 0 ? Math.Round((double)_filteredFixtures / _totalFixtures, 4) : 0;
            backtest.MatchedFixtures = _filteredFixtures > 0 ? Math.Round((double)_matchedFixtures / _filteredFixtures, 4) : 0;
            backtest.TeamType = (int)_parameters.ResultTeamType;
            backtest.Type = (int)_parameters.ResultType;
            backtest.MaxGoodRun = _maxGoodRun;
            backtest.MaxBadRun = _maxBadRun;
            backtest.UsesInFixture = false;

            foreach (var filter in _parameters.Filters)
                backtest.Filters.Add(filter);

            if (_parameters.ResultType == ResultType.FirstScoreHT)
            {
                double noGoalPercentage = _filteredFixtures > 0 ? Math.Round((double)_noGoalFixtures / _filteredFixtures, 4) : 0;
                backtest.AdditionalInformation.Add(new BacktestAdditionalInformation($"De todos os jogos filtrados, {Math.Round(noGoalPercentage * 100, 4)}% dos jogos terminou em 0x0 no primeiro tempo."));

                double goalConcededPercentage = _filteredFixtures > 0 ? Math.Round((double)_goalConcededFixtures / _filteredFixtures, 4) : 0;
                backtest.AdditionalInformation.Add(new BacktestAdditionalInformation($"De todos os jogos filtrados, {Math.Round(goalConcededPercentage * 100, 4)}% dos jogos, o time adversário marcou primeiro."));
            }

            return backtest;
        }


        #region Private methods

        private bool GetResultFilterFixture(FixturesApiResponseModel fixture, List<BacktestFilter> filters)
        {
            bool matchedFilterFixture = true;
            bool countFilter = true;

            if (filters != null && filters.Count > 0)
            {
                foreach (BacktestFilter filter in filters)
                {
                    if ((FilterTeamType)filter.TeamType == FilterTeamType.HomeTeam)
                    {
                        if ((FilterPropType)filter.PropType == FilterPropType.Overall && fixture.Stats.HomeMatchesCountOverall < filter.MinCountMatches)
                            countFilter = false;

                        if ((FilterPropType)filter.PropType == FilterPropType.HomeAway && fixture.Stats.HomeMatchesCountAtHome < filter.MinCountMatches)
                            countFilter = false;
                    }
                    else
                    {
                        if ((FilterPropType)filter.PropType == FilterPropType.Overall && fixture.Stats.AwayMatchesCountOverall < filter.MinCountMatches)
                            countFilter = false;

                        if ((FilterPropType)filter.PropType == FilterPropType.HomeAway && fixture.Stats.AwayMatchesCountAtAway < filter.MinCountMatches)
                            countFilter = false;
                    }

                    if (!countFilter)
                        return false;

                    Dictionary<(FilterTeamType, FilterPropType), string> mapping = new Dictionary<(FilterTeamType, FilterPropType), string>();

                    switch (filter.FilterCode)
                    {
                        case 1:
                            mapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                            {
                                {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeFirstToScorePercentTotal"},
                                {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeFirstToScorePercentAtHome"},
                                {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayFirstToScorePercentTotal"},
                                {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayFirstToScorePercentAtAway"}
                            };

                            break;
                        case 2:
                            mapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                            {
                                {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeToScoreTwoZeroPercentTotal"},
                                {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeToScoreTwoZeroPercentAtHome"},
                                {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayToScoreTwoZeroPercentTotal"},
                                {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayToScoreTwoZeroPercentAtAway"}
                            };

                            break;
                        case 3:
                            mapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                            {
                                {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeCleanSheetsPercentTotal"},
                                {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeCleanSheetsPercentAtHome"},
                                {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayCleanSheetsPercentTotal"},
                                {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayCleanSheetsPercentAtAway"}
                            };

                            break;
                        case 4:
                            mapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                            {
                                {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeFailedToScorePercentTotal"},
                                {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeFailedToScorePercentAtHome"},
                                {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayFailedToScorePercentTotal"},
                                {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayFailedToScorePercentAtAway"}
                            };

                            break;
                        case 5:
                            mapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                            {
                                {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeBothToScorePercentTotal"},
                                {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeBothToScorePercentAtHome"},
                                {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayBothToScorePercentTotal"},
                                {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayBothToScorePercentAtAway"}
                            };

                            break;
                        case 6:
                            mapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                            {
                                {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeAverageGoalsScoredTotal"},
                                {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeAverageGoalsScoredAtHome"},
                                {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayAverageGoalsScoredTotal"},
                                {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayAverageGoalsScoredAtAway"}
                            };

                            break;
                        case 7:
                            mapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                            {
                                {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeAverageGoalsConcededTotal"},
                                {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeAverageGoalsConcededAtHome"},
                                {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayAverageGoalsConcededTotal"},
                                {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayAverageGoalsConcededAtAway"}
                            };

                            break;
                        case 8:
                            mapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                            {
                                {(FilterTeamType.HomeTeam, FilterPropType.Overall), "HomeFirstToScorePercentHTTotal"},
                                {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "HomeFirstToScorePercentHTAtHome"},
                                {(FilterTeamType.AwayTeam, FilterPropType.Overall), "AwayFirstToScorePercentHTTotal"},
                                {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "AwayFirstToScorePercentHTAtAway"}
                            };

                            break;
                        case 9:
                            mapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                            {
                                {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeToScoreTwoZeroPercentHTTotal"},
                                {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeToScoreTwoZeroPercentHTAtHome"},
                                {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayToScoreTwoZeroPercentHTTotal"},
                                {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayToScoreTwoZeroPercentHTAtAway"}
                            };

                            break;
                        case 10:
                            mapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                            {
                                {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeCleanSheetsPercentHTTotal"},
                                {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeCleanSheetsPercentAtHTHome"},
                                {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayCleanSheetsPercentHTTotal"},
                                {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayCleanSheetsPercentHTAtAway"}
                            };

                            break;
                        case 11:
                            mapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                            {
                                {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeFailedToScorePercentHTTotal"},
                                {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeFailedToScorePercentHTAtHome"},
                                {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayFailedToScorePercentHTTotal"},
                                {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayFailedToScorePercentHTAtAway"}
                            };

                            break;
                        case 12:
                            mapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                            {
                                {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeBothToScorePercentHTTotal"},
                                {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeBothToScorePercentHTAtHome"},
                                {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayBothToScorePercentHTTotal"},
                                {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayBothToScorePercentHTAtAway"}
                            };

                            break;
                        case 13:
                            mapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                            {
                                {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeAverageGoalsScoredHTTotal"},
                                {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeAverageGoalsScoredHTAtHome"},
                                {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayAverageGoalsScoredHTTotal"},
                                {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayAverageGoalsScoredHTAtAway"}
                            };

                            break;
                        case 14:
                            mapping = new Dictionary<(FilterTeamType, FilterPropType), string>
                            {
                                {(FilterTeamType.HomeTeam, FilterPropType.Overall), "Stats.HomeAverageGoalsConcededHTTotal"},
                                {(FilterTeamType.HomeTeam, FilterPropType.HomeAway), "Stats.HomeAverageGoalsConcededHTAtHome"},
                                {(FilterTeamType.AwayTeam, FilterPropType.Overall), "Stats.AwayAverageGoalsConcededHTTotal"},
                                {(FilterTeamType.AwayTeam, FilterPropType.HomeAway), "Stats.AwayAverageGoalsConcededHTAtAway"}
                            };

                            break;
                        default:
                            break;
                    }

                    string propPath = mapping[((FilterTeamType)filter.TeamType, (FilterPropType)filter.PropType)];

                    FilterTeamType inverseTeamType = (FilterTeamType)filter.TeamType == FilterTeamType.HomeTeam ? FilterTeamType.AwayTeam : FilterTeamType.HomeTeam;
                    string inversePath = mapping[(inverseTeamType, (FilterPropType)filter.PropType)];
                    
                    matchedFilterFixture = GetFilterFixtureResult(fixture, (FilterCalculateType)filter.CalculateType, (FilterCalculateOperation)filter.CalculateOperation, (FilterCompareType)filter.CompareType, propPath, inversePath, filter.InitialValue, filter.FinalValue, filter.RelativeValue);

                    if (!matchedFilterFixture)
                        return false;
                }

            }

            return matchedFilterFixture && countFilter;
        }

        private bool GetResultFixture(FixturesApiResponseModel fixture, bool filterFixture, List<FixtureGoalsApiResponseModel> goals, int homeTeamCode, int awayTeamCode, ResultTeamType resultTeamType, ResultType resultType)
        {
            List<double> homeTeamGoals = goals.Where(g => g.TeamId == homeTeamCode).OrderBy(g => g.Minute).Select(g => Convert.ToDouble(g.Minute)).ToList();
            List<double> awayTeamGoals = goals.Where(g => g.TeamId == awayTeamCode).OrderBy(g => g.Minute).Select(g => Convert.ToDouble(g.Minute)).ToList();
            List<double> homeTeamHTGoals = goals.Where(g => g.TeamId == homeTeamCode && Convert.ToDouble(g.Minute) < 46).OrderBy(g => Convert.ToDouble(g.Minute)).Select(g => Convert.ToDouble(g.Minute)).ToList();
            List<double> awayTeamHTGoals = goals.Where(g => g.TeamId == awayTeamCode && Convert.ToDouble(g.Minute) < 46).OrderBy(g => Convert.ToDouble(g.Minute)).Select(g => Convert.ToDouble(g.Minute)).ToList();

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
                bool result = false;
                double homeTeamFirstGoalHT = homeTeamHTGoals.Count > 0 ? homeTeamHTGoals[0] : -1;
                double awayTeamFirstGoalHT = awayTeamHTGoals.Count > 0 ? awayTeamHTGoals[0] : -1;

                if (homeTeamFirstGoalHT == -1 && awayTeamFirstGoalHT == -1)
                    result = false;
                else if (resultTeamType == ResultTeamType.HomeTeam)
                    result = awayTeamFirstGoalHT == -1 || (homeTeamFirstGoalHT > -1 && homeTeamFirstGoalHT < awayTeamFirstGoalHT);
                else
                    result = homeTeamFirstGoalHT == -1 || (awayTeamFirstGoalHT > -1 && awayTeamFirstGoalHT < homeTeamFirstGoalHT);

                if (result && filterFixture)
                    primeiros.Add(new PrimeiroGol() { FixtureCode = fixture.Code, HomeFirstGoal = homeTeamFirstGoalHT, AwayFirstGoal = awayTeamFirstGoalHT, HomeFirstToScore = fixture.Stats.HomeFirstToScorePercentHTAtHome, AwayFirstToScore = fixture.Stats.AwayFirstToScorePercentHTAtAway, HomeCount = fixture.Stats.HomeMatchesCountAtHome, AwayCount = fixture.Stats.AwayMatchesCountAtAway });

                if (!result && filterFixture)
                    nao_deu_match.Add(new PrimeiroGol() { FixtureCode = fixture.Code, HomeFirstGoal = homeTeamFirstGoalHT, AwayFirstGoal = awayTeamFirstGoalHT, HomeFirstToScore = fixture.Stats.HomeFirstToScorePercentHTAtHome, AwayFirstToScore = fixture.Stats.AwayFirstToScorePercentHTAtAway, HomeCount = fixture.Stats.HomeMatchesCountAtHome, AwayCount = fixture.Stats.AwayMatchesCountAtAway });

                if (!filterFixture)
                    nao_filtrados.Add(new PrimeiroGol() { FixtureCode = fixture.Code, HomeFirstGoal = homeTeamFirstGoalHT, AwayFirstGoal = awayTeamFirstGoalHT, HomeFirstToScore = fixture.Stats.HomeFirstToScorePercentHTAtHome, AwayFirstToScore = fixture.Stats.AwayFirstToScorePercentHTAtAway, HomeCount = fixture.Stats.HomeMatchesCountAtHome, AwayCount = fixture.Stats.AwayMatchesCountAtAway });

                return result;
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
        private bool AdditionalInformationFixture(List<FixtureGoalsApiResponseModel> goals, int homeTeamCode, int awayTeamCode, ResultTeamType resultTeamType, ResultType resultType)
        {
            List<double> homeTeamHTGoals = goals.Where(g => g.TeamId == homeTeamCode && Convert.ToDouble(g.Minute) < 46).OrderBy(g => Convert.ToDouble(g.Minute)).Select(g => Convert.ToDouble(g.Minute)).ToList();
            List<double> awayTeamHTGoals = goals.Where(g => g.TeamId == awayTeamCode && Convert.ToDouble(g.Minute) < 46).OrderBy(g => Convert.ToDouble(g.Minute)).Select(g => Convert.ToDouble(g.Minute)).ToList();

            #region First Score in HT

            if (resultType == ResultType.FirstScoreHT)
            {
                double homeTeamFirstGoalHT = homeTeamHTGoals.Count > 0 ? homeTeamHTGoals[0] : -1;
                double awayTeamFirstGoalHT = awayTeamHTGoals.Count > 0 ? awayTeamHTGoals[0] : -1;

                if (homeTeamFirstGoalHT == -1 && awayTeamFirstGoalHT == -1)
                    _noGoalFixtures++;
                else if (resultTeamType == ResultTeamType.HomeTeam && (homeTeamFirstGoalHT == -1 || (awayTeamFirstGoalHT > -1 && awayTeamFirstGoalHT < homeTeamFirstGoalHT)))
                    _goalConcededFixtures++;
                else if (resultTeamType == ResultTeamType.AwayTeam && (awayTeamFirstGoalHT == -1 || (homeTeamFirstGoalHT > -1 && homeTeamFirstGoalHT < awayTeamFirstGoalHT)))
                    _goalConcededFixtures++;
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

        private bool GetFilterFixtureResult(FixturesApiResponseModel fixture, FilterCalculateType calculationType, FilterCalculateOperation calculateOperation, FilterCompareType compareType, string principalProp, string inverseProperty, double initialValue, double finalValue, double relativeValue)
        {
            double propValue = GetPropertyValue(principalProp, fixture);
            
            if (calculationType == FilterCalculateType.Absolute)
            {
                if (compareType == FilterCompareType.EqualOrGreater)
                    return propValue >= initialValue && propValue <= finalValue;

                return propValue > initialValue && propValue < finalValue;
            }
            else
            {
                double inversePropValue = GetPropertyValue(inverseProperty, fixture);
                double inverseWithMathApplied = ApplyPropMathInRelativeCalc(calculateOperation, relativeValue, inversePropValue);

                if (compareType == FilterCompareType.EqualOrGreater)
                    return propValue >= inverseWithMathApplied;

                return propValue > inverseWithMathApplied;
            }
        }

        private double GetPropertyValue(string propertyPath, FixturesApiResponseModel fixture)
        {
            var propertyInfo = fixture.Stats.GetType().GetProperty(propertyPath);
            
            if (propertyInfo != null)
                return (double)propertyInfo.GetValue(fixture.Stats);
            
            throw new Exception($"Propriedade {propertyPath} não encontrada no objeto fixture.");
        }

        private double ApplyPropMathInRelativeCalc(FilterCalculateOperation calculateOperation, double relativeValue, double propValue)
        {
            double finalValue = 0;

            switch (calculateOperation)
            {
                case FilterCalculateOperation.Multiplication:
                    finalValue = propValue * relativeValue;
                    break;
                case FilterCalculateOperation.Division:
                    finalValue = propValue / relativeValue;
                    break;
                case FilterCalculateOperation.Subtraction:
                    finalValue = propValue - relativeValue;
                    break;
                case FilterCalculateOperation.Sum:
                    finalValue = propValue + relativeValue;
                    break;
                default:
                    break;
            }

            return finalValue;
        }

        #endregion
    }
}
