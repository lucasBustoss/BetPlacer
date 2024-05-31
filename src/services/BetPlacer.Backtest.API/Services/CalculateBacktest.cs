using BetPlacer.Backtest.API.Models;
using BetPlacer.Backtest.API.Models.Enums;
using BetPlacer.Backtest.API.Models.Filters;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures;
using System.Linq;

namespace BetPlacer.Backtest.API.Services
{
    public class CalculateBacktest
    {
        public void Calculate(BacktestParameters parameters, List<FixturesApiResponseModel> fixtures)
        {
            List<FixturesApiResponseModel> filteredFixtures = ApplyFixtureFilters(fixtures, parameters.Filters);
            
            int countFixtures = filteredFixtures.Count;
            int countMatchedFixtures = 0;

            foreach (FixturesApiResponseModel fixture in filteredFixtures)
            {
                if (GetResult(fixture.Goals, fixture.HomeTeamCode, fixture.AwayTeamCode, parameters.ResultTeamType, parameters.ResultType))
                    countMatchedFixtures++;
            }
        }

        #region Private methods

        private List<FixturesApiResponseModel> ApplyFixtureFilters(List<FixturesApiResponseModel> fixtures, BacktestFilters filters)
        {
            List<FixturesApiResponseModel> filteredFixtures = new List<FixturesApiResponseModel>();
            
            #region FTS

            Func<FixturesApiResponseModel, bool> ftsPredicate;

            if (filters.FirstToScorePercentType == FirstToScorePercent.Greater)
                ftsPredicate = n => n.Stats.HomeFirstToScorePercentTotal > filters.FirstToScorePercentInitial && n.Stats.HomeFirstToScorePercentTotal < filters.FirstToScorePercentFinal;
            else
                ftsPredicate = n => n.Stats.HomeFirstToScorePercentTotal >= filters.FirstToScorePercentInitial && n.Stats.HomeFirstToScorePercentTotal <= filters.FirstToScorePercentFinal;

            filteredFixtures = fixtures.Where(ftsPredicate).ToList();

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

        #endregion
    }
}
