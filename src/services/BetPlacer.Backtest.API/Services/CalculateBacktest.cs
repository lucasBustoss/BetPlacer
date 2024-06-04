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
    public class CalculateBacktest
    {
        public BacktestModel Calculate(BacktestParameters parameters, List<FixturesApiResponseModel> fixtures, List<LeaguesApiResponseModel> leagues, List<TeamsApiResponseModel> teams)
        {
            List<FixturesApiResponseModel> filteredFixtures = ApplyFixtureFilters(fixtures, parameters.Filters);
            List<FixturesApiResponseModel> matchedFixtures = new List<FixturesApiResponseModel>();

            int goodRun = 0;
            int badRun = 0;
            int maxGoodRun = 0;
            int maxBadRun = 0;

            foreach (FixturesApiResponseModel fixture in filteredFixtures)
            {
                if (GetResult(fixture.Goals, fixture.HomeTeamCode, fixture.AwayTeamCode, parameters.ResultTeamType, parameters.ResultType))
                {
                    matchedFixtures.Add(fixture);
                    goodRun++;

                    if (badRun > maxBadRun)
                        maxBadRun = badRun;

                    badRun = 0;
                }
                else
                {
                    badRun++;

                    if (goodRun > maxGoodRun)
                        maxGoodRun = goodRun;

                    goodRun = 0;
                }
            }

            BacktestModel backtest = GenerateBacktestResult(parameters, fixtures, matchedFixtures, leagues, teams, filteredFixtures.Count, maxGoodRun, maxBadRun);

            return backtest;
        }

        #region Private methods

        private List<FixturesApiResponseModel> ApplyFixtureFilters(List<FixturesApiResponseModel> fixtures, BacktestFilters filters)
        {
            List<FixturesApiResponseModel> filteredFixtures = fixtures;

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
                    filteredFixtures = filteredFixtures.Where(ftsPredicate).ToList();
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
                    filteredFixtures = filteredFixtures.Where(twoZeroPredicate).ToList();
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
                    filteredFixtures = filteredFixtures.Where(cleanSheetPredicate).ToList();
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
                    filteredFixtures = filteredFixtures.Where(failedToScorePredicate).ToList();
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
                    filteredFixtures = filteredFixtures.Where(bothToScorePredicate).ToList();
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
                    filteredFixtures = filteredFixtures.Where(averageGoalsScoredPredicate).ToList();
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
                    filteredFixtures = filteredFixtures.Where(averageGoalsConcededPredicate).ToList();
                }

                #endregion
            }

            return filteredFixtures.OrderBy(ff => ff.Date).ToList();
        }

        private bool GetResult(List<FixtureGoalsApiResponseModel> goals, int homeTeamCode, int awayTeamCode, ResultTeamType resultTeamType, ResultType resultType)
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
                    return homeTeamFirstGoal < awayTeamFirstGoal;

                return awayTeamFirstGoal < homeTeamFirstGoal;
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
                    return homeTeamFirstGoalHT < awayTeamFirstGoalHT;

                return awayTeamFirstGoalHT < homeTeamFirstGoalHT;
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
                    return homeTeamSecondGoal > -1 && homeTeamSecondGoal < awayTeamFirstGoal;

                return awayTeamSecondGoal > -1 && awayTeamSecondGoal < homeTeamFirstGoal;
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
                    return homeTeamSecondGoalHT > -1 && homeTeamSecondGoalHT < awayTeamFirstGoalHT;

                return awayTeamSecondGoalHT > -1 && awayTeamSecondGoalHT < homeTeamFirstGoalHT;
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

        private BacktestModel GenerateBacktestResult(BacktestParameters parameters,
            List<FixturesApiResponseModel> fixtures, List<FixturesApiResponseModel> matchedFixtures, List<LeaguesApiResponseModel> leagues, List<TeamsApiResponseModel> teams, int countFilteredFixtures, int maxGoodRun, int maxBadRun)
        {
            BacktestModel backtest = new BacktestModel(true);
            backtest.Name = parameters.Name;
            backtest.FilteredFixtures = fixtures.Count > 0 ? Math.Round((double)countFilteredFixtures / fixtures.Count, 4) : 0;
            backtest.MatchedFixtures = countFilteredFixtures > 0 ? Math.Round((double)matchedFixtures.Count / countFilteredFixtures, 4) : 0;
            backtest.TeamType = (int)parameters.ResultTeamType;
            backtest.Type = (int)parameters.ResultType;
            backtest.MaxGoodRun = maxGoodRun;
            backtest.MaxBadRun = maxBadRun;

            #region Filters

            Type type = parameters.Filters.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                object propertyValue = property.GetValue(parameters.Filters);

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

            #region Leagues

            var groupLeagues = matchedFixtures.GroupBy(f => f.LeagueCode).OrderByDescending(g => g.Count()).ToList();

            foreach (var groupLeague in groupLeagues)
            {
                LeaguesApiResponseModel league = leagues.Where(l => l.Code == groupLeague.Key).FirstOrDefault();

                if (league != null)
                {
                    double ratio = Math.Round((double)groupLeague.Count() / matchedFixtures.Count, 4);
                    LeagueBacktestModel leagueBacktest = new LeagueBacktestModel(league, ratio);

                    backtest.Leagues.Add(leagueBacktest);
                }
            }

            #endregion

            #region LeagueSeason

            var groupLeagueSeasons = matchedFixtures.GroupBy(f => f.LeagueSeasonCode).OrderByDescending(g => g.Count()).ToList();

            foreach (var groupLeagueSeason in groupLeagueSeasons)
            {
                LeaguesApiResponseModel league = leagues.FirstOrDefault(l => l.Season.Any(s => s.Code == groupLeagueSeason.Key));
                LeagueSeasonApiResponseModel season = league.Season.FirstOrDefault(s => s.Code == groupLeagueSeason.Key);

                if (league != null && season != null)
                {
                    double ratio = Math.Round((double)groupLeagueSeason.Count() / matchedFixtures.Count, 4);
                    LeagueSeasonBacktestModel leagueSeasonBacktest = new LeagueSeasonBacktestModel(league, season, ratio);

                    backtest.LeagueSeasons.Add(leagueSeasonBacktest);
                }
            }

            #endregion

            #region Team

            var groupHomeTeams = matchedFixtures.GroupBy(f => f.HomeTeamCode).ToList();
            var groupAwayTeams = matchedFixtures.GroupBy(f => f.AwayTeamCode).ToList();

            Dictionary<int, List<FixturesApiResponseModel>> groupTeams = new Dictionary<int, List<FixturesApiResponseModel>>();

            foreach (var group in groupHomeTeams)
            {
                if (!groupTeams.ContainsKey(group.Key))
                {
                    groupTeams[group.Key] = new List<FixturesApiResponseModel>();
                }

                groupTeams[group.Key].AddRange(group);
            }

            foreach (var group in groupAwayTeams)
            {
                if (!groupTeams.ContainsKey(group.Key))
                    groupTeams[group.Key] = new List<FixturesApiResponseModel>();

                groupTeams[group.Key].AddRange(group);
            }

            var sortedTeams = groupTeams.OrderByDescending(kv => kv.Value.Count).ToDictionary(kv => kv.Key, kv => kv.Value);

            foreach (var groupTeam in sortedTeams)
            {
                TeamsApiResponseModel team = teams.Where(l => l.Code == groupTeam.Key).FirstOrDefault();

                if (team != null)
                {
                    double ratio = Math.Round((double)groupTeam.Value.Count() / matchedFixtures.Count, 4);
                    TeamBacktestModel teamBacktest = new TeamBacktestModel(team, ratio);

                    backtest.Teams.Add(teamBacktest);
                }
            }

            #endregion

            return backtest;
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
                default:
                    name = "";
                    break;
            }

            return name;
        }

        #endregion
    }
}
