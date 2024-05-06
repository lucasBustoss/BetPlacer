using BetPlacer.Fixtures.API.Models.Entities;
using BetPlacer.Fixtures.API.Models.Entities.Trade;
using BetPlacer.Fixtures.API.Models.Enums;
using BetPlacer.Fixtures.API.Models.ValueObjects;
using BetPlacer.Fixtures.API.Services.Models;
using System.Collections.Generic;

namespace BetPlacer.Fixtures.API.Services
{
    public class CalculateFixtureStatsService
    {
        public List<FixtureStatsTradeModel> Calculate(IEnumerable<FixtureModel> fixtures, IEnumerable<FixtureGoalsModel> fixtureGoals)
        {
            List<FixtureStatsTradeModel> statsList = new List<FixtureStatsTradeModel>();

            foreach (FixtureModel fixture in fixtures.ToList())
            {
                List<FixtureModel> homePastFixturesAtHome =
                        fixtures
                        .Where(f => f.HomeTeamId == fixture.HomeTeamId && f.StartDate < fixture.StartDate)
                        .Distinct()
                        .ToList();

                List<FixtureModel> homePastFixturesAtAway =
                        fixtures
                        .Where(f => f.AwayTeamId == fixture.HomeTeamId && f.StartDate < fixture.StartDate)
                        .Distinct()
                        .ToList();

                List<FixtureModel> awayPastFixturesAtHome =
                        fixtures
                        .Where(f => f.HomeTeamId == fixture.AwayTeamId && f.StartDate < fixture.StartDate)
                        .Distinct()
                        .ToList();

                List<FixtureModel> awayPastFixturesAtAway =
                        fixtures
                        .Where(f => f.AwayTeamId == fixture.AwayTeamId && f.StartDate < fixture.StartDate)
                        .Distinct()
                        .ToList();

                FixtureStatsTradeModel stats = new FixtureStatsTradeModel();
                stats.FixtureCode = fixture.Code;

                GetPPG(stats, fixture, homePastFixturesAtHome, homePastFixturesAtAway, awayPastFixturesAtHome, awayPastFixturesAtAway);
                GetTotalWins(stats, fixture, homePastFixturesAtHome, homePastFixturesAtAway, awayPastFixturesAtHome, awayPastFixturesAtAway);
                GetGoalsStats(stats, fixture, homePastFixturesAtHome, homePastFixturesAtAway, awayPastFixturesAtHome, awayPastFixturesAtAway, fixtureGoals.ToList());

                statsList.Add(stats);
            }

            return statsList;
        }

        #region GetStats

        private void GetPPG(FixtureStatsTradeModel stats, FixtureModel fixture, List<FixtureModel> homePastFixturesAtHome, List<FixtureModel> homePastFixturesAtAway, List<FixtureModel> awayPastFixturesAtHome, List<FixtureModel> awayPastFixturesAtAway)
        {
            stats.HomePPGTotalAtHome = CalculatePPG(homePastFixturesAtHome, fixture.HomeTeamId);
            stats.HomePPGTotal = CalculatePPG(homePastFixturesAtHome.Concat(homePastFixturesAtAway).ToList(), fixture.HomeTeamId);
            stats.AwayPPGTotalAtAway = CalculatePPG(awayPastFixturesAtAway, fixture.AwayTeamId);
            stats.AwayPPGTotal = CalculatePPG(awayPastFixturesAtAway.Concat(awayPastFixturesAtHome).ToList(), fixture.AwayTeamId);
        }

        private void GetTotalWins(FixtureStatsTradeModel stats, FixtureModel fixture, List<FixtureModel> homePastFixturesAtHome, List<FixtureModel> homePastFixturesAtAway, List<FixtureModel> awayPastFixturesAtHome, List<FixtureModel> awayPastFixturesAtAway)
        {
            Tuple<int, double> homeTotalWins = CalculateTotalWinsAndPercentWins(homePastFixturesAtHome.Concat(homePastFixturesAtAway).ToList(), fixture.HomeTeamId);
            Tuple<int, double> homeTotalWinsAtHome = CalculateTotalWinsAndPercentWins(homePastFixturesAtHome, fixture.HomeTeamId);
            Tuple<int, double> awayTotalWins = CalculateTotalWinsAndPercentWins(awayPastFixturesAtAway.Concat(awayPastFixturesAtHome).ToList(), fixture.AwayTeamId);
            Tuple<int, double> awayTotalWinsAtAway = CalculateTotalWinsAndPercentWins(awayPastFixturesAtAway, fixture.AwayTeamId);

            stats.HomeWinsTotal = homeTotalWins.Item1;
            stats.HomeWinsPercentTotal = homeTotalWins.Item2;
            stats.HomeWinsTotalAtHome = homeTotalWinsAtHome.Item1;
            stats.HomeWinsPercentTotalAtHome = homeTotalWinsAtHome.Item2;

            stats.AwayWinsTotal = awayTotalWins.Item1;
            stats.AwayWinsPercentTotal = awayTotalWins.Item2;
            stats.AwayWinsTotalAtAway = awayTotalWinsAtAway.Item1;
            stats.AwayWinsPercentTotalAtAway = awayTotalWinsAtAway.Item2;
        }

        private void GetGoalsStats(FixtureStatsTradeModel stats, FixtureModel fixture, List<FixtureModel> homePastFixturesAtHome, List<FixtureModel> homePastFixturesAtAway, List<FixtureModel> awayPastFixturesAtHome, List<FixtureModel> awayPastFixturesAtAway, List<FixtureGoalsModel> fixtureGoalsModel)
        {
            #region Home total

            List<FixtureModel> homeFixturesTotal = homePastFixturesAtHome.Concat(homePastFixturesAtAway).ToList();
            List<FixtureGoalsModel> homeFixtureGoalsTotal = fixtureGoalsModel.Where(hfg => homeFixturesTotal.Any(hf => hf.Code == hfg.FixtureCode)).ToList();
            GoalsCalculate homeGoalsTotal = CalculateGoals(homeFixturesTotal, homeFixtureGoalsTotal, fixture.HomeTeamId);

            stats.HomeFirstToScorePercentTotal = homeGoalsTotal.FTSPercent;
            stats.HomeCleanSheetsPercentTotal = homeGoalsTotal.CleanSheetsPercent;
            stats.HomeFailedToScorePercentTotal = homeGoalsTotal.FailedToScorePercent;
            stats.HomeBothToScorePercentTotal = homeGoalsTotal.BothToScorePercent;
            stats.HomeGoalsScoredTotal = homeGoalsTotal.GoalsScored;
            stats.HomeGoalsConcededTotal = homeGoalsTotal.GoalsConceded;
            stats.HomeAverageGoalsScoredTotal = homeGoalsTotal.AverageGoalsScored;
            stats.HomeAverageGoalsConcededTotal = homeGoalsTotal.AverageGoalsConceded;

            #endregion

            #region Home at home

            List<FixtureGoalsModel> homeFixtureGoalsAtHome = fixtureGoalsModel.Where(hfg => homePastFixturesAtHome.Any(hf => hf.Code == hfg.FixtureCode)).ToList();
            GoalsCalculate homeGoalsAtHome = CalculateGoals(homePastFixturesAtHome, homeFixtureGoalsAtHome, fixture.HomeTeamId);

            stats.HomeFirstToScorePercentTotalAtHome = homeGoalsAtHome.FTSPercent;
            stats.HomeCleanSheetsPercentTotalAtHome = homeGoalsAtHome.CleanSheetsPercent;
            stats.HomeFailedToScorePercentTotalAtHome = homeGoalsAtHome.FailedToScorePercent;
            stats.HomeBothToScorePercentTotalAtHome = homeGoalsAtHome.BothToScorePercent;
            stats.HomeGoalsScoredTotalAtHome = homeGoalsAtHome.GoalsScored;
            stats.HomeGoalsConcededTotalAtHome = homeGoalsAtHome.GoalsConceded;
            stats.HomeAverageGoalsScoredTotalAtHome = homeGoalsAtHome.AverageGoalsScored;
            stats.HomeAverageGoalsConcededTotalAtHome = homeGoalsAtHome.AverageGoalsConceded;

            #endregion

            #region Away total

            List<FixtureModel> awayFixturesTotal = awayPastFixturesAtAway.Concat(awayPastFixturesAtHome).ToList();
            List<FixtureGoalsModel> awayFixtureGoalsTotal = fixtureGoalsModel.Where(afg => awayFixturesTotal.Any(af => af.Code == afg.FixtureCode)).ToList();
            GoalsCalculate awayGoalsTotal = CalculateGoals(awayFixturesTotal, awayFixtureGoalsTotal, fixture.AwayTeamId);

            stats.AwayFirstToScorePercentTotal = awayGoalsTotal.FTSPercent;
            stats.AwayCleanSheetsPercentTotal = awayGoalsTotal.CleanSheetsPercent;
            stats.AwayFailedToScorePercentTotal = awayGoalsTotal.FailedToScorePercent;
            stats.AwayBothToScorePercentTotal = awayGoalsTotal.BothToScorePercent;
            stats.AwayGoalsScoredTotal = awayGoalsTotal.GoalsScored;
            stats.AwayGoalsConcededTotal = awayGoalsTotal.GoalsConceded;
            stats.AwayAverageGoalsScoredTotal = awayGoalsTotal.AverageGoalsScored;
            stats.AwayAverageGoalsConcededTotal = awayGoalsTotal.AverageGoalsConceded;

            #endregion

            #region Away at away

            List<FixtureGoalsModel> awayFixtureGoalsAtAway = fixtureGoalsModel.Where(afg => awayPastFixturesAtAway.Any(af => af.Code == afg.FixtureCode)).ToList();
            GoalsCalculate awayGoalsAtAway = CalculateGoals(awayPastFixturesAtAway, awayFixtureGoalsAtAway, fixture.AwayTeamId);

            stats.AwayFirstToScorePercentTotalAtAway = awayGoalsAtAway.FTSPercent;
            stats.AwayCleanSheetsPercentTotalAtAway = awayGoalsAtAway.CleanSheetsPercent;
            stats.AwayFailedToScorePercentTotalAtAway = awayGoalsAtAway.FailedToScorePercent;
            stats.AwayBothToScorePercentTotalAtAway = awayGoalsAtAway.BothToScorePercent;
            stats.AwayGoalsScoredTotalAtAway = awayGoalsAtAway.GoalsScored;
            stats.AwayGoalsConcededTotalAtAway = awayGoalsAtAway.GoalsConceded;
            stats.AwayAverageGoalsScoredTotalAtAway = awayGoalsAtAway.AverageGoalsScored;
            stats.AwayAverageGoalsConcededTotalAtAway = awayGoalsAtAway.AverageGoalsConceded;

            #endregion
        }

        #endregion

        #region Calculations

        private double CalculatePPG(List<FixtureModel> fixtures, int teamId)
        {
            int points = 0;
            int countFixtures = 0;

            foreach (FixtureModel fixture in fixtures)
            {
                bool isHome = fixture.HomeTeamId == teamId;

                if (isHome)
                {
                    points +=
                        fixture.HomeTeamGoals > fixture.AwayTeamGoals ? 3 :
                        fixture.HomeTeamGoals == fixture.AwayTeamGoals ? 1 :
                        0;
                }
                else
                {
                    points +=
                        fixture.AwayTeamGoals > fixture.HomeTeamGoals ? 3 :
                        fixture.AwayTeamGoals == fixture.HomeTeamGoals ? 1 :
                        0;
                }

                countFixtures++;
            }

            double ppg = countFixtures > 0 ? (double)points / countFixtures : 0;
            return Math.Round(ppg, 2);
        }

        private Tuple<int, double> CalculateTotalWinsAndPercentWins(List<FixtureModel> fixtures, int teamId)
        {
            int wins = 0;
            int countFixtures = 0;

            foreach (FixtureModel fixture in fixtures)
            {
                bool isHome = fixture.HomeTeamId == teamId;

                if (isHome)
                    wins += fixture.HomeTeamGoals > fixture.AwayTeamGoals ? 1 : 0;
                else
                    wins += fixture.AwayTeamGoals > fixture.HomeTeamGoals ? 1 : 0;

                countFixtures++;
            }

            double percentWins = countFixtures > 0 ? (double)wins / countFixtures : 0;
            return new Tuple<int, double>(wins, Math.Round(percentWins, 2));
        }

        private GoalsCalculate CalculateGoals(List<FixtureModel> fixtures, List<FixtureGoalsModel> fixtureGoals, int teamId)
        {
            int countFixtures = 0;
            int goalsScored = 0;
            int goalsConceded = 0;
            int firstToScore = 0;
            int fixturesWithoutConceded = 0;
            int failedToScore = 0;
            int bothToScore = 0;

            foreach (FixtureModel fixture in fixtures)
            {
                List<FixtureGoalsModel> referenceTeamGoals = fixtureGoals.Where(g => g.FixtureCode == fixture.Code && g.TeamId == teamId).OrderBy(g => Convert.ToDouble(g.Minute)).ToList();
                List<FixtureGoalsModel> otherTeamGoals = fixtureGoals.Where(g => g.FixtureCode == fixture.Code && g.TeamId != teamId).OrderBy(g => Convert.ToDouble(g.Minute)).ToList();

                countFixtures++;

                goalsScored += referenceTeamGoals.Count;
                goalsConceded += otherTeamGoals.Count;

                if (otherTeamGoals.Count == 0)
                    fixturesWithoutConceded++;

                if (referenceTeamGoals.Count == 0)
                    failedToScore++;

                if (otherTeamGoals.Count > 0 && referenceTeamGoals.Count > 0)
                    bothToScore++;

                FixtureGoalsModel referenceTeamFirstGoal = referenceTeamGoals.FirstOrDefault();
                FixtureGoalsModel otherTeamFirstGoal = otherTeamGoals.FirstOrDefault();

                if (referenceTeamFirstGoal != null && 
                    (otherTeamFirstGoal == null || (Convert.ToDouble(referenceTeamFirstGoal.Minute) < Convert.ToDouble(otherTeamFirstGoal.Minute))))
                {
                    firstToScore++;
                }
            }

            double ftsPercent = countFixtures > 0 ? Math.Round((double)firstToScore / countFixtures, 2) : 0;
            double fixturesWithoutConcededGoalsPercent = countFixtures > 0 ? Math.Round((double)fixturesWithoutConceded / countFixtures, 2) : 0;
            double failedToScoredPercent = countFixtures > 0 ? Math.Round((double)failedToScore / countFixtures, 2) : 0;
            double bothToScorePercent = countFixtures > 0 ? Math.Round((double)bothToScore / countFixtures, 2) : 0;
            double averageGoalsScored = countFixtures > 0 ? Math.Round((double)goalsScored / countFixtures, 2) : 0;
            double averageGoalsConceded = countFixtures > 0 ? Math.Round((double)goalsConceded / countFixtures, 2) : 0;

            GoalsCalculate goalsCalculate = 
                new GoalsCalculate(ftsPercent, fixturesWithoutConcededGoalsPercent, failedToScoredPercent, bothToScorePercent, goalsScored, goalsConceded, averageGoalsScored, averageGoalsConceded);
            
            return goalsCalculate;
        }

        #endregion
    }
}
