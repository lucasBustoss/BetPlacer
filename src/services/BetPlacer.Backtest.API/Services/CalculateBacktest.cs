using BetPlacer.Backtest.API.Models;
using BetPlacer.Backtest.API.Models.Entities;
using BetPlacer.Backtest.API.Models.Enums;
using BetPlacer.Backtest.API.Models.Filters;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures;

namespace BetPlacer.Backtest.API.Services
{
    public class CalculateBacktest
    {
        public BacktestModel Calculate(BacktestParameters parameters, List<FixturesApiResponseModel> fixtures)
        {
            List<FixturesApiResponseModel> filteredFixtures = ApplyFixtureFilters(fixtures, parameters.Filters);

            int countMatchedFixtures = 0;

            int goodRun = 0;
            int badRun = 0;
            int maxGoodRun = 0;
            int maxBadRun = 0;

            foreach (FixturesApiResponseModel fixture in filteredFixtures)
            {
                if (GetResult(fixture.Goals, fixture.HomeTeamCode, fixture.AwayTeamCode, parameters.ResultTeamType, parameters.ResultType))
                {
                    countMatchedFixtures++;
                    goodRun++;

                    if (badRun > maxBadRun)
                        maxBadRun = badRun;

                    badRun = 0;
                }

                badRun++;

                if (goodRun > maxGoodRun)
                    maxGoodRun = goodRun;

                goodRun = 0;
            }

            BacktestModel backtest = GenerateBacktestResult(filteredFixtures, countMatchedFixtures, maxGoodRun, maxBadRun);

            return backtest;
        }

        #region Private methods

        private List<FixturesApiResponseModel> ApplyFixtureFilters(List<FixturesApiResponseModel> fixtures, BacktestFilters filters)
        {
            List<FixturesApiResponseModel> filteredFixtures = new List<FixturesApiResponseModel>();

            #region FTS

            if (filters != null && filters.ftsFilter != null)
            {
                Func<FixturesApiResponseModel, bool> ftsPredicate;
                FirstToScorePercentageFilter ftsFilter = filters.ftsFilter;


            if (ftsFilter.Type == FirstToScorePercent.Greater)
                    ftsPredicate = n => n.Stats.HomeFirstToScorePercentTotal > ftsFilter.InitialValue && n.Stats.HomeFirstToScorePercentTotal < ftsFilter.FinalValue;
                else
                    ftsPredicate = n => n.Stats.HomeFirstToScorePercentTotal >= ftsFilter.InitialValue && n.Stats.HomeFirstToScorePercentTotal <= ftsFilter.FinalValue;

                filteredFixtures = fixtures.Where(ftsPredicate).ToList();
            }

            #endregion

            return filteredFixtures;
        }

        private bool GetResult(List<FixtureGoalsApiResponseModel> goals, int homeTeamCode, int awayTeamCode, ResultTeamType resultTeamType, ResultType resultType)
        {
            List<double> homeTeamGoals = goals.Where(g => g.TeamId == homeTeamCode).OrderBy(g => g.Minute).Select(g => Convert.ToDouble(g.Minute)).ToList();
            List<double> awayTeamGoals = goals.Where(g => g.TeamId == awayTeamCode).OrderBy(g => g.Minute).Select(g => Convert.ToDouble(g.Minute)).ToList();

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

            if (resultType == ResultType.ToWinHT)
            {
                int homeTeamHTGoalsCount = homeTeamGoals.Where(htg => htg < 46).ToList().Count;
                int awayTeamHTGoalsCount = awayTeamGoals.Where(htg => htg < 46).ToList().Count;

                if (resultTeamType == ResultTeamType.HomeTeam)
                    return homeTeamHTGoalsCount > awayTeamHTGoalsCount;

                return awayTeamHTGoalsCount > homeTeamHTGoalsCount;
            }

            if (resultType == ResultType.ToWinFT)
            {
                if (resultTeamType == ResultTeamType.HomeTeam)
                    return homeTeamGoals.Count > awayTeamGoals.Count;

                return awayTeamGoals.Count > homeTeamGoals.Count;
            }

            return false;
        }

        private BacktestModel GenerateBacktestResult(List<FixturesApiResponseModel> fixtures, int countMatchedFixtures, int maxGoodRun, int maxBadRun)
        {
            BacktestModel backtest = new BacktestModel();

            return backtest;
        }

        #endregion
    }
}
