﻿using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.Entities;
using BetPlacer.Fixtures.API.Models.Entities;
using BetPlacer.Fixtures.API.Models.Enums;
using BetPlacer.Fixtures.API.Models.ValueObjects;
using BetPlacer.Fixtures.API.Services.Models;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Collections.Generic;
using System.Globalization;

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

                GetTotals(stats, fixture, homePastFixturesAtHome, homePastFixturesAtAway, awayPastFixturesAtHome, awayPastFixturesAtAway);
                GetPPG(stats, fixture, homePastFixturesAtHome, homePastFixturesAtAway, awayPastFixturesAtHome, awayPastFixturesAtAway);
                GetTotalWins(stats, fixture, homePastFixturesAtHome, homePastFixturesAtAway, awayPastFixturesAtHome, awayPastFixturesAtAway);
                GetGoalsStats(stats, fixture, homePastFixturesAtHome, homePastFixturesAtAway, awayPastFixturesAtHome, awayPastFixturesAtAway, fixtureGoals.ToList());
                GetHTStats(stats, fixture, homePastFixturesAtHome, homePastFixturesAtAway, awayPastFixturesAtHome, awayPastFixturesAtAway, fixtureGoals.ToList());
                GetFTStats(stats, fixture, homePastFixturesAtHome, homePastFixturesAtAway, awayPastFixturesAtHome, awayPastFixturesAtAway, fixtureGoals.ToList());

                statsList.Add(stats);
            }

            return statsList;
        }

        #region GetStats

        private void GetTotals(FixtureStatsTradeModel stats, FixtureModel fixture, List<FixtureModel> homePastFixturesAtHome, List<FixtureModel> homePastFixturesAtAway, List<FixtureModel> awayPastFixturesAtHome, List<FixtureModel> awayPastFixturesAtAway)
        {
            stats.HomeMatchesCountOverall = CalculateTotals(homePastFixturesAtHome.Concat(homePastFixturesAtAway).ToList(), fixture.HomeTeamId);
            stats.HomeMatchesCountAtHome = CalculateTotals(homePastFixturesAtHome, fixture.HomeTeamId);

            stats.AwayMatchesCountOverall = CalculateTotals(awayPastFixturesAtHome.Concat(awayPastFixturesAtAway).ToList(), fixture.AwayTeamId);
            stats.AwayMatchesCountAtAway = CalculateTotals(awayPastFixturesAtAway, fixture.AwayTeamId);
        }

        private void GetPPG(FixtureStatsTradeModel stats, FixtureModel fixture, List<FixtureModel> homePastFixturesAtHome, List<FixtureModel> homePastFixturesAtAway, List<FixtureModel> awayPastFixturesAtHome, List<FixtureModel> awayPastFixturesAtAway)
        {
            stats.HomePPGAtHome = CalculatePPG(homePastFixturesAtHome, fixture.HomeTeamId);
            stats.HomePPGTotal = CalculatePPG(homePastFixturesAtHome.Concat(homePastFixturesAtAway).ToList(), fixture.HomeTeamId);
            stats.AwayPPGAtAway = CalculatePPG(awayPastFixturesAtAway, fixture.AwayTeamId);
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
            stats.HomeWinsAtHome = homeTotalWinsAtHome.Item1;
            stats.HomeWinsPercentAtHome = homeTotalWinsAtHome.Item2;

            stats.AwayWinsTotal = awayTotalWins.Item1;
            stats.AwayWinsPercentTotal = awayTotalWins.Item2;
            stats.AwayWinsAtAway = awayTotalWinsAtAway.Item1;
            stats.AwayWinsPercentAtAway = awayTotalWinsAtAway.Item2;
        }

        private void GetGoalsStats(FixtureStatsTradeModel stats, FixtureModel fixture, List<FixtureModel> homePastFixturesAtHome, List<FixtureModel> homePastFixturesAtAway, List<FixtureModel> awayPastFixturesAtHome, List<FixtureModel> awayPastFixturesAtAway, List<FixtureGoalsModel> fixtureGoalsModel)
        {
            #region Home total

            List<FixtureModel> homeFixturesTotal = homePastFixturesAtHome.Concat(homePastFixturesAtAway).ToList();
            List<FixtureGoalsModel> homeFixtureGoalsTotal = fixtureGoalsModel.Where(hfg => homeFixturesTotal.Any(hf => hf.Code == hfg.FixtureCode)).ToList();
            GoalsCalculate homeGoalsTotal = CalculateGoals(homeFixturesTotal, homeFixtureGoalsTotal, fixture.HomeTeamId);

            stats.HomeFirstToScorePercentTotal = homeGoalsTotal.FTSPercent;
            stats.HomeToScoreTwoZeroPercentTotal = homeGoalsTotal.TwoZeroPercent;
            stats.HomeCleanSheetsPercentTotal = homeGoalsTotal.CleanSheetsPercent;
            stats.HomeFailedToScorePercentTotal = homeGoalsTotal.FailedToScorePercent;
            stats.HomeBothToScorePercentTotal = homeGoalsTotal.BothToScorePercent;
            stats.HomeGoalsScoredTotal = homeGoalsTotal.GoalsScored;
            stats.HomeGoalsConcededTotal = homeGoalsTotal.GoalsConceded;
            stats.HomeAverageGoalsScoredTotal = homeGoalsTotal.AverageGoalsScored;
            stats.HomeAverageGoalsConcededTotal = homeGoalsTotal.AverageGoalsConceded;

            #region GoalsMoment

            stats.HomeGoalsScoredAt0To15 = homeGoalsTotal.GoalsScoredIn0To15Min;
            stats.HomeGoalsScoredAt0To15Percent = homeGoalsTotal.GoalsScoredIn0To15MinPercent;
            stats.HomeGoalsScoredAt16To30 = homeGoalsTotal.GoalsScoredIn16To30Min;
            stats.HomeGoalsScoredAt16To30Percent = homeGoalsTotal.GoalsScoredIn16To30MinPercent;
            stats.HomeGoalsScoredAt31To45 = homeGoalsTotal.GoalsScoredIn31To45Min;
            stats.HomeGoalsScoredAt31To45Percent = homeGoalsTotal.GoalsScoredIn31To45MinPercent;
            stats.HomeGoalsScoredAt46To60 = homeGoalsTotal.GoalsScoredIn46To60Min;
            stats.HomeGoalsScoredAt46To60Percent = homeGoalsTotal.GoalsScoredIn46To60MinPercent;
            stats.HomeGoalsScoredAt61To75 = homeGoalsTotal.GoalsScoredIn61To75Min;
            stats.HomeGoalsScoredAt61To75Percent = homeGoalsTotal.GoalsScoredIn61To75MinPercent;
            stats.HomeGoalsScoredAt76To90 = homeGoalsTotal.GoalsScoredIn76To90Min;
            stats.HomeGoalsScoredAt76To90Percent = homeGoalsTotal.GoalsScoredIn76To90MinPercent;

            stats.HomeGoalsConcededAt0To15 = homeGoalsTotal.GoalsConcededIn0To15Min;
            stats.HomeGoalsConcededAt0To15Percent = homeGoalsTotal.GoalsConcededIn0To15MinPercent;
            stats.HomeGoalsConcededAt16To30 = homeGoalsTotal.GoalsConcededIn16To30Min;
            stats.HomeGoalsConcededAt16To30Percent = homeGoalsTotal.GoalsConcededIn16To30MinPercent;
            stats.HomeGoalsConcededAt31To45 = homeGoalsTotal.GoalsConcededIn31To45Min;
            stats.HomeGoalsConcededAt31To45Percent = homeGoalsTotal.GoalsConcededIn31To45MinPercent;
            stats.HomeGoalsConcededAt46To60 = homeGoalsTotal.GoalsConcededIn46To60Min;
            stats.HomeGoalsConcededAt46To60Percent = homeGoalsTotal.GoalsConcededIn46To60MinPercent;
            stats.HomeGoalsConcededAt61To75 = homeGoalsTotal.GoalsConcededIn61To75Min;
            stats.HomeGoalsConcededAt61To75Percent = homeGoalsTotal.GoalsConcededIn61To75MinPercent;
            stats.HomeGoalsConcededAt76To90 = homeGoalsTotal.GoalsConcededIn76To90Min;
            stats.HomeGoalsConcededAt76To90Percent = homeGoalsTotal.GoalsConcededIn76To90MinPercent;

            #endregion

            #endregion

            #region Home at home

            List<FixtureGoalsModel> homeFixtureGoalsAtHome = fixtureGoalsModel.Where(hfg => homePastFixturesAtHome.Any(hf => hf.Code == hfg.FixtureCode)).ToList();
            GoalsCalculate homeGoalsAtHome = CalculateGoals(homePastFixturesAtHome, homeFixtureGoalsAtHome, fixture.HomeTeamId);

            stats.HomeFirstToScorePercentAtHome = homeGoalsAtHome.FTSPercent;
            stats.HomeToScoreTwoZeroPercentAtHome = homeGoalsAtHome.TwoZeroPercent;
            stats.HomeCleanSheetsPercentAtHome = homeGoalsAtHome.CleanSheetsPercent;
            stats.HomeFailedToScorePercentAtHome = homeGoalsAtHome.FailedToScorePercent;
            stats.HomeBothToScorePercentAtHome = homeGoalsAtHome.BothToScorePercent;
            stats.HomeGoalsScoredAtHome = homeGoalsAtHome.GoalsScored;
            stats.HomeGoalsConcededAtHome = homeGoalsAtHome.GoalsConceded;
            stats.HomeAverageGoalsScoredAtHome = homeGoalsAtHome.AverageGoalsScored;
            stats.HomeAverageGoalsConcededAtHome = homeGoalsAtHome.AverageGoalsConceded;

            #region GoalsMoment

            stats.HomeGoalsScoredAt0To15AtHome = homeGoalsAtHome.GoalsScoredIn0To15Min;
            stats.HomeGoalsScoredAt0To15PercentAtHome = homeGoalsAtHome.GoalsScoredIn0To15MinPercent;
            stats.HomeGoalsScoredAt16To30AtHome = homeGoalsAtHome.GoalsScoredIn16To30Min;
            stats.HomeGoalsScoredAt16To30PercentAtHome = homeGoalsAtHome.GoalsScoredIn16To30MinPercent;
            stats.HomeGoalsScoredAt31To45AtHome = homeGoalsAtHome.GoalsScoredIn31To45Min;
            stats.HomeGoalsScoredAt31To45PercentAtHome = homeGoalsAtHome.GoalsScoredIn31To45MinPercent;
            stats.HomeGoalsScoredAt46To60AtHome = homeGoalsAtHome.GoalsScoredIn46To60Min;
            stats.HomeGoalsScoredAt46To60PercentAtHome = homeGoalsAtHome.GoalsScoredIn46To60MinPercent;
            stats.HomeGoalsScoredAt61To75AtHome = homeGoalsAtHome.GoalsScoredIn61To75Min;
            stats.HomeGoalsScoredAt61To75PercentAtHome = homeGoalsAtHome.GoalsScoredIn61To75MinPercent;
            stats.HomeGoalsScoredAt76To90AtHome = homeGoalsAtHome.GoalsScoredIn76To90Min;
            stats.HomeGoalsScoredAt76To90PercentAtHome = homeGoalsAtHome.GoalsScoredIn76To90MinPercent;

            stats.HomeGoalsConcededAt0To15AtHome = homeGoalsAtHome.GoalsConcededIn0To15Min;
            stats.HomeGoalsConcededAt0To15PercentAtHome = homeGoalsAtHome.GoalsConcededIn0To15MinPercent;
            stats.HomeGoalsConcededAt16To30AtHome = homeGoalsAtHome.GoalsConcededIn16To30Min;
            stats.HomeGoalsConcededAt16To30PercentAtHome = homeGoalsAtHome.GoalsConcededIn16To30MinPercent;
            stats.HomeGoalsConcededAt31To45AtHome = homeGoalsAtHome.GoalsConcededIn31To45Min;
            stats.HomeGoalsConcededAt31To45PercentAtHome = homeGoalsAtHome.GoalsConcededIn31To45MinPercent;
            stats.HomeGoalsConcededAt46To60AtHome = homeGoalsAtHome.GoalsConcededIn46To60Min;
            stats.HomeGoalsConcededAt46To60PercentAtHome = homeGoalsAtHome.GoalsConcededIn46To60MinPercent;
            stats.HomeGoalsConcededAt61To75AtHome = homeGoalsAtHome.GoalsConcededIn61To75Min;
            stats.HomeGoalsConcededAt61To75PercentAtHome = homeGoalsAtHome.GoalsConcededIn61To75MinPercent;
            stats.HomeGoalsConcededAt76To90AtHome = homeGoalsAtHome.GoalsConcededIn76To90Min;
            stats.HomeGoalsConcededAt76To90PercentAtHome = homeGoalsAtHome.GoalsConcededIn76To90MinPercent;

            #endregion

            #endregion

            #region Away total

            List<FixtureModel> awayFixturesTotal = awayPastFixturesAtAway.Concat(awayPastFixturesAtHome).ToList();
            List<FixtureGoalsModel> awayFixtureGoalsTotal = fixtureGoalsModel.Where(afg => awayFixturesTotal.Any(af => af.Code == afg.FixtureCode)).ToList();
            GoalsCalculate awayGoalsTotal = CalculateGoals(awayFixturesTotal, awayFixtureGoalsTotal, fixture.AwayTeamId);

            stats.AwayFirstToScorePercentTotal = awayGoalsTotal.FTSPercent;
            stats.AwayToScoreTwoZeroPercentTotal = awayGoalsTotal.TwoZeroPercent;
            stats.AwayCleanSheetsPercentTotal = awayGoalsTotal.CleanSheetsPercent;
            stats.AwayFailedToScorePercentTotal = awayGoalsTotal.FailedToScorePercent;
            stats.AwayBothToScorePercentTotal = awayGoalsTotal.BothToScorePercent;
            stats.AwayGoalsScoredTotal = awayGoalsTotal.GoalsScored;
            stats.AwayGoalsConcededTotal = awayGoalsTotal.GoalsConceded;
            stats.AwayAverageGoalsScoredTotal = awayGoalsTotal.AverageGoalsScored;
            stats.AwayAverageGoalsConcededTotal = awayGoalsTotal.AverageGoalsConceded;

            #region GoalsMoment

            stats.AwayGoalsScoredAt0To15 = awayGoalsTotal.GoalsScoredIn0To15Min;
            stats.AwayGoalsScoredAt0To15Percent = awayGoalsTotal.GoalsScoredIn0To15MinPercent;
            stats.AwayGoalsScoredAt16To30 = awayGoalsTotal.GoalsScoredIn16To30Min;
            stats.AwayGoalsScoredAt16To30Percent = awayGoalsTotal.GoalsScoredIn16To30MinPercent;
            stats.AwayGoalsScoredAt31To45 = awayGoalsTotal.GoalsScoredIn31To45Min;
            stats.AwayGoalsScoredAt31To45Percent = awayGoalsTotal.GoalsScoredIn31To45MinPercent;
            stats.AwayGoalsScoredAt46To60 = awayGoalsTotal.GoalsScoredIn46To60Min;
            stats.AwayGoalsScoredAt46To60Percent = awayGoalsTotal.GoalsScoredIn46To60MinPercent;
            stats.AwayGoalsScoredAt61To75 = awayGoalsTotal.GoalsScoredIn61To75Min;
            stats.AwayGoalsScoredAt61To75Percent = awayGoalsTotal.GoalsScoredIn61To75MinPercent;
            stats.AwayGoalsScoredAt76To90 = awayGoalsTotal.GoalsScoredIn76To90Min;
            stats.AwayGoalsScoredAt76To90Percent = awayGoalsTotal.GoalsScoredIn76To90MinPercent;

            stats.AwayGoalsConcededAt0To15 = awayGoalsTotal.GoalsConcededIn0To15Min;
            stats.AwayGoalsConcededAt0To15Percent = awayGoalsTotal.GoalsConcededIn0To15MinPercent;
            stats.AwayGoalsConcededAt16To30 = awayGoalsTotal.GoalsConcededIn16To30Min;
            stats.AwayGoalsConcededAt16To30Percent = awayGoalsTotal.GoalsConcededIn16To30MinPercent;
            stats.AwayGoalsConcededAt31To45 = awayGoalsTotal.GoalsConcededIn31To45Min;
            stats.AwayGoalsConcededAt31To45Percent = awayGoalsTotal.GoalsConcededIn31To45MinPercent;
            stats.AwayGoalsConcededAt46To60 = awayGoalsTotal.GoalsConcededIn46To60Min;
            stats.AwayGoalsConcededAt46To60Percent = awayGoalsTotal.GoalsConcededIn46To60MinPercent;
            stats.AwayGoalsConcededAt61To75 = awayGoalsTotal.GoalsConcededIn61To75Min;
            stats.AwayGoalsConcededAt61To75Percent = awayGoalsTotal.GoalsConcededIn61To75MinPercent;
            stats.AwayGoalsConcededAt76To90 = awayGoalsTotal.GoalsConcededIn76To90Min;
            stats.AwayGoalsConcededAt76To90Percent = awayGoalsTotal.GoalsConcededIn76To90MinPercent;

            #endregion

            #endregion

            #region Away at away

            List<FixtureGoalsModel> awayFixtureGoalsAtAway = fixtureGoalsModel.Where(afg => awayPastFixturesAtAway.Any(af => af.Code == afg.FixtureCode)).ToList();
            GoalsCalculate awayGoalsAtAway = CalculateGoals(awayPastFixturesAtAway, awayFixtureGoalsAtAway, fixture.AwayTeamId);

            stats.AwayFirstToScorePercentAtAway = awayGoalsAtAway.FTSPercent;
            stats.AwayToScoreTwoZeroPercentAtAway = awayGoalsAtAway.TwoZeroPercent;
            stats.AwayCleanSheetsPercentAtAway = awayGoalsAtAway.CleanSheetsPercent;
            stats.AwayFailedToScorePercentAtAway = awayGoalsAtAway.FailedToScorePercent;
            stats.AwayBothToScorePercentAtAway = awayGoalsAtAway.BothToScorePercent;
            stats.AwayGoalsScoredAtAway = awayGoalsAtAway.GoalsScored;
            stats.AwayGoalsConcededAtAway = awayGoalsAtAway.GoalsConceded;
            stats.AwayAverageGoalsScoredAtAway = awayGoalsAtAway.AverageGoalsScored;
            stats.AwayAverageGoalsConcededAtAway = awayGoalsAtAway.AverageGoalsConceded;

            #region GoalsMoment

            stats.AwayGoalsScoredAt0To15AtAway = awayGoalsAtAway.GoalsScoredIn0To15Min;
            stats.AwayGoalsScoredAt0To15PercentAtAway = awayGoalsAtAway.GoalsScoredIn0To15MinPercent;
            stats.AwayGoalsScoredAt16To30AtAway = awayGoalsAtAway.GoalsScoredIn16To30Min;
            stats.AwayGoalsScoredAt16To30PercentAtAway = awayGoalsAtAway.GoalsScoredIn16To30MinPercent;
            stats.AwayGoalsScoredAt31To45AtAway = awayGoalsAtAway.GoalsScoredIn31To45Min;
            stats.AwayGoalsScoredAt31To45PercentAtAway = awayGoalsAtAway.GoalsScoredIn31To45MinPercent;
            stats.AwayGoalsScoredAt46To60AtAway = awayGoalsAtAway.GoalsScoredIn46To60Min;
            stats.AwayGoalsScoredAt46To60PercentAtAway = awayGoalsAtAway.GoalsScoredIn46To60MinPercent;
            stats.AwayGoalsScoredAt61To75AtAway = awayGoalsAtAway.GoalsScoredIn61To75Min;
            stats.AwayGoalsScoredAt61To75PercentAtAway = awayGoalsAtAway.GoalsScoredIn61To75MinPercent;
            stats.AwayGoalsScoredAt76To90AtAway = awayGoalsAtAway.GoalsScoredIn76To90Min;
            stats.AwayGoalsScoredAt76To90PercentAtAway = awayGoalsAtAway.GoalsScoredIn76To90MinPercent;

            stats.AwayGoalsConcededAt0To15AtAway = awayGoalsAtAway.GoalsConcededIn0To15Min;
            stats.AwayGoalsConcededAt0To15PercentAtAway = awayGoalsAtAway.GoalsConcededIn0To15MinPercent;
            stats.AwayGoalsConcededAt16To30AtAway = awayGoalsAtAway.GoalsConcededIn16To30Min;
            stats.AwayGoalsConcededAt16To30PercentAtAway = awayGoalsAtAway.GoalsConcededIn16To30MinPercent;
            stats.AwayGoalsConcededAt31To45AtAway = awayGoalsAtAway.GoalsConcededIn31To45Min;
            stats.AwayGoalsConcededAt31To45PercentAtAway = awayGoalsAtAway.GoalsConcededIn31To45MinPercent;
            stats.AwayGoalsConcededAt46To60AtAway = awayGoalsAtAway.GoalsConcededIn46To60Min;
            stats.AwayGoalsConcededAt46To60PercentAtAway = awayGoalsAtAway.GoalsConcededIn46To60MinPercent;
            stats.AwayGoalsConcededAt61To75AtAway = awayGoalsAtAway.GoalsConcededIn61To75Min;
            stats.AwayGoalsConcededAt61To75PercentAtAway = awayGoalsAtAway.GoalsConcededIn61To75MinPercent;
            stats.AwayGoalsConcededAt76To90AtAway = awayGoalsAtAway.GoalsConcededIn76To90Min;
            stats.AwayGoalsConcededAt76To90PercentAtAway = awayGoalsAtAway.GoalsConcededIn76To90MinPercent;

            #endregion

            #endregion
        }

        private void GetHTStats(FixtureStatsTradeModel stats, FixtureModel fixture, List<FixtureModel> homePastFixturesAtHome, List<FixtureModel> homePastFixturesAtAway, List<FixtureModel> awayPastFixturesAtHome, List<FixtureModel> awayPastFixturesAtAway, List<FixtureGoalsModel> fixtureGoalsModel)
        {
            #region Home total

            List<FixtureModel> homeFixturesTotal = homePastFixturesAtHome.Concat(homePastFixturesAtAway).ToList();
            List<FixtureGoalsModel> homeFixtureGoalsTotal = fixtureGoalsModel.Where(hfg => homeFixturesTotal.Any(hf => hf.Code == hfg.FixtureCode)).ToList();
            HTFTCalculate homeHTTotal = CalculateHTFTStats(homeFixturesTotal, homeFixtureGoalsTotal, fixture.HomeTeamId, false);

            stats.HomeWinsPercentHTTotal = homeHTTotal.WinsPercent;
            stats.HomeDrawsPercentHTTotal = homeHTTotal.DrawsPercent;
            stats.HomeLossesPercentHTTotal = homeHTTotal.LossesPercent;
            stats.HomeFirstToScorePercentHTTotal = homeHTTotal.FTSPercent;
            stats.HomeToScoreTwoZeroPercentHTTotal = homeHTTotal.TwoZeroPercent;
            stats.HomeFailedToScorePercentHTTotal = homeHTTotal.FailedToScorePercent;
            stats.HomeBothToScorePercentHTTotal = homeHTTotal.BothToScorePercent;
            stats.HomeCleanSheetsPercentHTTotal = homeHTTotal.CleanSheetsPercent;
            stats.HomeGoalsScoredHTTotal = homeHTTotal.GoalsScored;
            stats.HomeGoalsConcededHTTotal = homeHTTotal.GoalsConceded;
            stats.HomeAverageGoalsScoredHTTotal = homeHTTotal.AverageGoalsScored;
            stats.HomeAverageGoalsConcededHTTotal = homeHTTotal.AverageGoalsConceded;

            #endregion

            #region Home at home

            List<FixtureGoalsModel> homeFixtureGoalsAtHome = fixtureGoalsModel.Where(hfg => homePastFixturesAtHome.Any(hf => hf.Code == hfg.FixtureCode)).ToList();
            HTFTCalculate homeHTAtHome = CalculateHTFTStats(homePastFixturesAtHome, homeFixtureGoalsAtHome, fixture.HomeTeamId, false);

            stats.HomeWinsPercentHTAtHome = homeHTAtHome.WinsPercent;
            stats.HomeDrawsPercentHTAtHome = homeHTAtHome.DrawsPercent;
            stats.HomeLossesPercentHTAtHome = homeHTAtHome.LossesPercent;
            stats.HomeFirstToScorePercentHTAtHome = homeHTAtHome.FTSPercent;
            stats.HomeToScoreTwoZeroPercentHTAtHome = homeHTAtHome.TwoZeroPercent;
            stats.HomeFailedToScorePercentHTAtHome = homeHTAtHome.FailedToScorePercent;
            stats.HomeBothToScorePercentHTAtHome = homeHTAtHome.BothToScorePercent;
            stats.HomeCleanSheetsPercentHTAtHome = homeHTAtHome.CleanSheetsPercent;
            stats.HomeGoalsScoredHTAtHome = homeHTAtHome.GoalsScored;
            stats.HomeGoalsConcededHTAtHome = homeHTAtHome.GoalsConceded;
            stats.HomeAverageGoalsScoredHTAtHome = homeHTAtHome.AverageGoalsScored;
            stats.HomeAverageGoalsConcededHTAtHome = homeHTAtHome.AverageGoalsConceded;

            #endregion

            #region Away total

            List<FixtureModel> awayFixturesTotal = awayPastFixturesAtAway.Concat(awayPastFixturesAtHome).ToList();
            List<FixtureGoalsModel> awayFixtureGoalsTotal = fixtureGoalsModel.Where(hfg => awayFixturesTotal.Any(hf => hf.Code == hfg.FixtureCode)).ToList();
            HTFTCalculate awayHTTotal = CalculateHTFTStats(awayFixturesTotal, awayFixtureGoalsTotal, fixture.AwayTeamId, false);

            stats.AwayWinsPercentHTTotal = awayHTTotal.WinsPercent;
            stats.AwayDrawsPercentHTTotal = awayHTTotal.DrawsPercent;
            stats.AwayLossesPercentHTTotal = awayHTTotal.LossesPercent;
            stats.AwayFirstToScorePercentHTTotal = awayHTTotal.FTSPercent;
            stats.AwayToScoreTwoZeroPercentHTTotal = awayHTTotal.TwoZeroPercent;
            stats.AwayFailedToScorePercentHTTotal = awayHTTotal.FailedToScorePercent;
            stats.AwayBothToScorePercentHTTotal = awayHTTotal.BothToScorePercent;
            stats.AwayCleanSheetsPercentHTTotal = awayHTTotal.CleanSheetsPercent;
            stats.AwayGoalsScoredHTTotal = awayHTTotal.GoalsScored;
            stats.AwayGoalsConcededHTTotal = awayHTTotal.GoalsConceded;
            stats.AwayAverageGoalsScoredHTTotal = awayHTTotal.AverageGoalsScored;
            stats.AwayAverageGoalsConcededHTTotal = awayHTTotal.AverageGoalsConceded;

            #endregion

            #region Away at away

            List<FixtureGoalsModel> awayFixtureGoalsAtAway = fixtureGoalsModel.Where(hfg => awayPastFixturesAtAway.Any(hf => hf.Code == hfg.FixtureCode)).ToList();
            HTFTCalculate awayHTAtAway = CalculateHTFTStats(awayPastFixturesAtAway, awayFixtureGoalsAtAway, fixture.AwayTeamId, false);

            stats.AwayWinsPercentHTAtAway = awayHTAtAway.WinsPercent;
            stats.AwayDrawsPercentHTAtAway = awayHTAtAway.DrawsPercent;
            stats.AwayLossesPercentHTAtAway = awayHTAtAway.LossesPercent;
            stats.AwayFirstToScorePercentHTAtAway = awayHTAtAway.FTSPercent;
            stats.AwayToScoreTwoZeroPercentHTAtAway = awayHTAtAway.TwoZeroPercent;
            stats.AwayFailedToScorePercentHTAtAway = awayHTAtAway.FailedToScorePercent;
            stats.AwayBothToScorePercentHTAtAway = awayHTAtAway.BothToScorePercent;
            stats.AwayCleanSheetsPercentHTAtAway = awayHTAtAway.CleanSheetsPercent;
            stats.AwayGoalsScoredHTAtAway = awayHTAtAway.GoalsScored;
            stats.AwayGoalsConcededHTAtAway = awayHTAtAway.GoalsConceded;
            stats.AwayAverageGoalsScoredHTAtAway = awayHTAtAway.AverageGoalsScored;
            stats.AwayAverageGoalsConcededHTAtAway = awayHTAtAway.AverageGoalsConceded;

            #endregion
        }

        private void GetFTStats(FixtureStatsTradeModel stats, FixtureModel fixture, List<FixtureModel> homePastFixturesAtHome, List<FixtureModel> homePastFixturesAtAway, List<FixtureModel> awayPastFixturesAtHome, List<FixtureModel> awayPastFixturesAtAway, List<FixtureGoalsModel> fixtureGoalsModel)
        {
            #region Home total

            List<FixtureModel> homeFixturesTotal = homePastFixturesAtHome.Concat(homePastFixturesAtAway).ToList();
            List<FixtureGoalsModel> homeFixtureGoalsTotal = fixtureGoalsModel.Where(hfg => homeFixturesTotal.Any(hf => hf.Code == hfg.FixtureCode)).ToList();
            HTFTCalculate homeFTTotal = CalculateHTFTStats(homeFixturesTotal, homeFixtureGoalsTotal, fixture.HomeTeamId, true);

            stats.HomeWinsPercentFTTotal = homeFTTotal.WinsPercent;
            stats.HomeDrawsPercentFTTotal = homeFTTotal.DrawsPercent;
            stats.HomeLossesPercentFTTotal = homeFTTotal.LossesPercent;
            stats.HomeFirstToScorePercentFTTotal = homeFTTotal.FTSPercent;
            stats.HomeToScoreTwoZeroPercentFTTotal = homeFTTotal.TwoZeroPercent;
            stats.HomeFailedToScorePercentFTTotal = homeFTTotal.FailedToScorePercent;
            stats.HomeBothToScorePercentFTTotal = homeFTTotal.BothToScorePercent;
            stats.HomeCleanSheetsPercentFTTotal = homeFTTotal.CleanSheetsPercent;
            stats.HomeGoalsScoredFTTotal = homeFTTotal.GoalsScored;
            stats.HomeGoalsConcededFTTotal = homeFTTotal.GoalsConceded;
            stats.HomeAverageGoalsScoredFTTotal = homeFTTotal.AverageGoalsScored;
            stats.HomeAverageGoalsConcededFTTotal = homeFTTotal.AverageGoalsConceded;

            #endregion

            #region Home at home

            List<FixtureGoalsModel> homeFixtureGoalsAtHome = fixtureGoalsModel.Where(hfg => homePastFixturesAtHome.Any(hf => hf.Code == hfg.FixtureCode)).ToList();
            HTFTCalculate homeFTAtHome = CalculateHTFTStats(homePastFixturesAtHome, homeFixtureGoalsAtHome, fixture.HomeTeamId, true);

            stats.HomeWinsPercentFTAtHome = homeFTAtHome.WinsPercent;
            stats.HomeDrawsPercentFTAtHome = homeFTAtHome.DrawsPercent;
            stats.HomeLossesPercentFTAtHome = homeFTAtHome.LossesPercent;
            stats.HomeFirstToScorePercentFTAtHome = homeFTAtHome.FTSPercent;
            stats.HomeToScoreTwoZeroPercentFTAtHome = homeFTAtHome.TwoZeroPercent;
            stats.HomeFailedToScorePercentFTAtHome = homeFTAtHome.FailedToScorePercent;
            stats.HomeBothToScorePercentFTAtHome = homeFTAtHome.BothToScorePercent;
            stats.HomeCleanSheetsPercentFTAtHome = homeFTAtHome.CleanSheetsPercent;
            stats.HomeGoalsScoredFTAtHome = homeFTAtHome.GoalsScored;
            stats.HomeGoalsConcededFTAtHome = homeFTAtHome.GoalsConceded;
            stats.HomeAverageGoalsScoredFTAtHome = homeFTAtHome.AverageGoalsScored;
            stats.HomeAverageGoalsConcededFTAtHome = homeFTAtHome.AverageGoalsConceded;

            #endregion

            #region Away total

            List<FixtureModel> awayFixturesTotal = awayPastFixturesAtAway.Concat(awayPastFixturesAtHome).ToList();
            List<FixtureGoalsModel> awayFixtureGoalsTotal = fixtureGoalsModel.Where(hfg => awayFixturesTotal.Any(hf => hf.Code == hfg.FixtureCode)).ToList();
            HTFTCalculate awayFTTotal = CalculateHTFTStats(awayFixturesTotal, awayFixtureGoalsTotal, fixture.AwayTeamId, true);

            stats.AwayWinsPercentFTTotal = awayFTTotal.WinsPercent;
            stats.AwayDrawsPercentFTTotal = awayFTTotal.DrawsPercent;
            stats.AwayLossesPercentFTTotal = awayFTTotal.LossesPercent;
            stats.AwayFirstToScorePercentFTTotal = awayFTTotal.FTSPercent;
            stats.AwayToScoreTwoZeroPercentFTTotal = awayFTTotal.TwoZeroPercent;
            stats.AwayFailedToScorePercentFTTotal = awayFTTotal.FailedToScorePercent;
            stats.AwayBothToScorePercentFTTotal = awayFTTotal.BothToScorePercent;
            stats.AwayCleanSheetsPercentFTTotal = awayFTTotal.CleanSheetsPercent;
            stats.AwayGoalsScoredFTTotal = awayFTTotal.GoalsScored;
            stats.AwayGoalsConcededFTTotal = awayFTTotal.GoalsConceded;
            stats.AwayAverageGoalsScoredFTTotal = awayFTTotal.AverageGoalsScored;
            stats.AwayAverageGoalsConcededFTTotal = awayFTTotal.AverageGoalsConceded;

            #endregion

            #region Away at away

            List<FixtureGoalsModel> awayFixtureGoalsAtAway = fixtureGoalsModel.Where(hfg => awayPastFixturesAtAway.Any(hf => hf.Code == hfg.FixtureCode)).ToList();
            HTFTCalculate awayFTAtAway = CalculateHTFTStats(awayPastFixturesAtAway, awayFixtureGoalsAtAway, fixture.AwayTeamId, true);

            stats.AwayWinsPercentFTAtAway = awayFTAtAway.WinsPercent;
            stats.AwayDrawsPercentFTAtAway = awayFTAtAway.DrawsPercent;
            stats.AwayLossesPercentFTAtAway = awayFTAtAway.LossesPercent;
            stats.AwayFirstToScorePercentFTAtAway = awayFTAtAway.FTSPercent;
            stats.AwayToScoreTwoZeroPercentFTAtAway = awayFTAtAway.TwoZeroPercent;
            stats.AwayFailedToScorePercentFTAtAway = awayFTAtAway.FailedToScorePercent;
            stats.AwayBothToScorePercentFTAtAway = awayFTAtAway.BothToScorePercent;
            stats.AwayCleanSheetsPercentFTAtAway = awayFTAtAway.CleanSheetsPercent;
            stats.AwayGoalsScoredFTAtAway = awayFTAtAway.GoalsScored;
            stats.AwayGoalsConcededFTAtAway = awayFTAtAway.GoalsConceded;
            stats.AwayAverageGoalsScoredFTAtAway = awayFTAtAway.AverageGoalsScored;
            stats.AwayAverageGoalsConcededFTAtAway = awayFTAtAway.AverageGoalsConceded;

            #endregion
        }

        #endregion

        #region Calculations

        private int CalculateTotals(List<FixtureModel> fixtures, int teamId)
        {
            var count = fixtures.Where(f => f.HomeTeamId == teamId || f.AwayTeamId == f.AwayTeamId).Count();
            return count;
        }


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
            int toScoreTwoZero = 0;
            int fixturesWithoutConceded = 0;
            int failedToScore = 0;
            int bothToScore = 0;

            int goalsScoredIn0To15Min = 0;
            double goalsScoredIn0To15MinPercent = 0;
            int goalsScoredIn16To30Min = 0;
            double goalsScoredIn16To30MinPercent = 0;
            int goalsScoredIn31To45Min = 0;
            double goalsScoredIn31To45MinPercent = 0;
            int goalsScoredIn46To60Min = 0;
            double goalsScoredIn46To60MinPercent = 0;
            int goalsScoredIn61To75Min = 0;
            double goalsScoredIn61To75MinPercent = 0;
            int goalsScoredIn76To90Min = 0;
            double goalsScoredIn76To90MinPercent = 0;

            int goalsConcededIn0To15Min = 0;
            double goalsConcededIn0To15MinPercent = 0;
            int goalsConcededIn16To30Min = 0;
            double goalsConcededIn16To30MinPercent = 0;
            int goalsConcededIn31To45Min = 0;
            double goalsConcededIn31To45MinPercent = 0;
            int goalsConcededIn46To60Min = 0;
            double goalsConcededIn46To60MinPercent = 0;
            int goalsConcededIn61To75Min = 0;
            double goalsConcededIn61To75MinPercent = 0;
            int goalsConcededIn76To90Min = 0;
            double goalsConcededIn76To90MinPercent = 0;

            foreach (FixtureModel fixture in fixtures)
            {
                List<FixtureGoalsModel> referenceTeamGoals = fixtureGoals
                    .Where(g => g.FixtureCode == fixture.Code && g.TeamId == teamId)
                    .OrderBy(g => Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture))
                    .ToList();

                List<FixtureGoalsModel> otherTeamGoals = fixtureGoals
                    .Where(g => g.FixtureCode == fixture.Code && g.TeamId != teamId)
                    .OrderBy(g => Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture))
                    .ToList();

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

                #region FTS

                if (referenceTeamFirstGoal != null &&
                    (otherTeamFirstGoal == null || (Convert.ToDouble(referenceTeamFirstGoal.Minute, CultureInfo.InvariantCulture) < Convert.ToDouble(otherTeamFirstGoal.Minute, CultureInfo.InvariantCulture))))
                {
                    firstToScore++;
                }

                #endregion

                #region TwoZero

                if (referenceTeamGoals.Count > 1)
                {
                    double referenceSecondGoalMinute = Convert.ToDouble(referenceTeamGoals[1].Minute, CultureInfo.InvariantCulture);

                    if (otherTeamGoals == null || otherTeamGoals.Count == 0)
                        toScoreTwoZero++;
                    else
                    {
                        double otherFirstGoalMinute = Convert.ToDouble(otherTeamGoals[0].Minute, CultureInfo.InvariantCulture);

                        if (referenceSecondGoalMinute < otherFirstGoalMinute)
                            toScoreTwoZero++;
                    }
                }

                #endregion

                #region GoalsMoment

                #region 0 to 15

                goalsScoredIn0To15Min += referenceTeamGoals.Where(g => Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) >= 0 && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) <= 15).Count();
                goalsConcededIn0To15Min += otherTeamGoals.Where(g => Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) >= 0 && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) <= 15).Count();

                #endregion

                #region 16 to 30

                goalsScoredIn16To30Min += referenceTeamGoals.Where(g => Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) >= 16 && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) <= 30).Count();
                goalsConcededIn16To30Min += otherTeamGoals.Where(g => Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) >= 16 && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) <= 30).Count();

                #endregion

                #region 31 to 45

                goalsScoredIn31To45Min += referenceTeamGoals.Where(g => Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) >= 31 && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) < 46).Count();
                goalsConcededIn31To45Min += otherTeamGoals.Where(g => Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) >= 31 && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) < 46).Count();

                #endregion

                #region 46 to 60

                goalsScoredIn46To60Min += referenceTeamGoals.Where(g => Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) >= 46 && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) <= 60).Count();
                goalsConcededIn46To60Min += otherTeamGoals.Where(g => Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) >= 46 && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) <= 60).Count();

                #endregion

                #region 61 to 75

                goalsScoredIn61To75Min += referenceTeamGoals.Where(g => Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) >= 61 && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) <= 75).Count();
                goalsConcededIn61To75Min += otherTeamGoals.Where(g => Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) >= 61 && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) <= 75).Count();

                #endregion

                #region 76 to 90

                goalsScoredIn76To90Min += referenceTeamGoals.Where(g => Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) >= 76 && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) < 91).Count();
                goalsConcededIn76To90Min += otherTeamGoals.Where(g => Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) >= 76 && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) < 91).Count();

                #endregion

                #endregion
            }

            double ftsPercent = countFixtures > 0 ? Math.Round((double)firstToScore / countFixtures, 2) : 0;
            double twoZeroPercent = firstToScore > 0 ? Math.Round((double)toScoreTwoZero / firstToScore, 2) : 0;
            double fixturesWithoutConcededGoalsPercent = countFixtures > 0 ? Math.Round((double)fixturesWithoutConceded / countFixtures, 2) : 0;
            double failedToScoredPercent = countFixtures > 0 ? Math.Round((double)failedToScore / countFixtures, 2) : 0;
            double bothToScorePercent = countFixtures > 0 ? Math.Round((double)bothToScore / countFixtures, 2) : 0;
            double averageGoalsScored = countFixtures > 0 ? Math.Round((double)goalsScored / countFixtures, 2) : 0;
            double averageGoalsConceded = countFixtures > 0 ? Math.Round((double)goalsConceded / countFixtures, 2) : 0;

            goalsScoredIn0To15MinPercent = goalsScored > 0 ? Math.Round((double)goalsScoredIn0To15Min / goalsScored, 2) : 0;
            goalsConcededIn0To15MinPercent = goalsConceded > 0 ? Math.Round((double)goalsConcededIn0To15Min / goalsConceded, 2) : 0;
            goalsScoredIn16To30MinPercent = goalsScored > 0 ? Math.Round((double)goalsScoredIn16To30Min / goalsScored, 2) : 0;
            goalsConcededIn16To30MinPercent = goalsConceded > 0 ? Math.Round((double)goalsConcededIn16To30Min / goalsConceded, 2) : 0;
            goalsScoredIn31To45MinPercent = goalsScored > 0 ? Math.Round((double)goalsScoredIn31To45Min / goalsScored, 2) : 0;
            goalsConcededIn31To45MinPercent = goalsConceded > 0 ? Math.Round((double)goalsConcededIn31To45Min / goalsConceded, 2) : 0;
            goalsScoredIn46To60MinPercent = goalsScored > 0 ? Math.Round((double)goalsScoredIn46To60Min / goalsScored, 2) : 0;
            goalsConcededIn46To60MinPercent = goalsConceded > 0 ? Math.Round((double)goalsConcededIn46To60Min / goalsConceded, 2) : 0;
            goalsScoredIn61To75MinPercent = goalsScored > 0 ? Math.Round((double)goalsScoredIn61To75Min / goalsScored, 2) : 0;
            goalsConcededIn61To75MinPercent = goalsConceded > 0 ? Math.Round((double)goalsConcededIn61To75Min / goalsConceded, 2) : 0;
            goalsScoredIn76To90MinPercent = goalsScored > 0 ? Math.Round((double)goalsScoredIn76To90Min / goalsScored, 2) : 0;
            goalsConcededIn76To90MinPercent = goalsConceded > 0 ? Math.Round((double)goalsConcededIn76To90Min / goalsConceded, 2) : 0;

            GoalsCalculate goalsCalculate =
                new GoalsCalculate(
                    ftsPercent,
                    twoZeroPercent,
                    fixturesWithoutConcededGoalsPercent,
                    failedToScoredPercent,
                    bothToScorePercent,
                    goalsScored,
                    goalsConceded,
                    averageGoalsScored,
                    averageGoalsConceded,
                    goalsScoredIn0To15Min,
                    goalsScoredIn0To15MinPercent,
                    goalsScoredIn16To30Min,
                    goalsScoredIn16To30MinPercent,
                    goalsScoredIn31To45Min,
                    goalsScoredIn31To45MinPercent,
                    goalsScoredIn46To60Min,
                    goalsScoredIn46To60MinPercent,
                    goalsScoredIn61To75Min,
                    goalsScoredIn61To75MinPercent,
                    goalsScoredIn76To90Min,
                    goalsScoredIn76To90MinPercent,
                    goalsConcededIn0To15Min,
                    goalsConcededIn0To15MinPercent,
                    goalsConcededIn16To30Min,
                    goalsConcededIn16To30MinPercent,
                    goalsConcededIn31To45Min,
                    goalsConcededIn31To45MinPercent,
                    goalsConcededIn46To60Min,
                    goalsConcededIn46To60MinPercent,
                    goalsConcededIn61To75Min,
                    goalsConcededIn61To75MinPercent,
                    goalsConcededIn76To90Min,
                    goalsConcededIn76To90MinPercent);

            return goalsCalculate;
        }

        private HTFTCalculate CalculateHTFTStats(List<FixtureModel> fixtures, List<FixtureGoalsModel> fixtureGoals, int teamId, bool isFT)
        {
            double winsPercent = 0;
            double drawsPercent = 0;
            double lossesPercent = 0;
            double ftsPercent = 0;
            double twoZeroPercent = 0;
            double failedToScorePercent = 0;
            double bothToScorePercent = 0;
            double cleanSheetsPercent = 0;
            int goalsScoredTotal = 0;
            int goalsConcededTotal = 0;
            double averageGoalsScored = 0;
            double averageGoalsConceded = 0;

            int countFixtures = 0;
            int wins = 0;
            int draws = 0;
            int losses = 0;
            int firstToScore = 0;
            int toScoreTwoZero = 0;
            int fts = 0;
            int bothToScore = 0;
            int cs = 0;

            foreach (FixtureModel fixture in fixtures)
            {
                Func<FixtureGoalsModel, bool> referenceCondition;
                Func<FixtureGoalsModel, bool> otherCondition;

                if (!isFT)
                {
                    referenceCondition = g => g.FixtureCode == fixture.Code
                            && g.TeamId == teamId
                            && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) >= 0
                            && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) < 46;

                    otherCondition = g => g.FixtureCode == fixture.Code
                                && g.TeamId != teamId
                                && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) >= 0
                                && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) < 46;
                } else
                {
                    referenceCondition = g => g.FixtureCode == fixture.Code
                            && g.TeamId == teamId
                            && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) >= 46
                            && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) < 91;

                    otherCondition = g => g.FixtureCode == fixture.Code
                                && g.TeamId != teamId
                                && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) >= 46
                                && Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture) < 91;
                }


                List<FixtureGoalsModel> referenceTeamGoals = fixtureGoals
                    .Where(referenceCondition)
                    .OrderBy(g => Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture))
                    .ToList();

                List<FixtureGoalsModel> otherTeamGoals = fixtureGoals
                    .Where(otherCondition)
                    .OrderBy(g => Convert.ToDouble(g.Minute, CultureInfo.InvariantCulture))
                    .ToList();

                countFixtures++;

                int goalsScored = referenceTeamGoals.Count;
                int goalsConceded = otherTeamGoals.Count;

                goalsScoredTotal += goalsScored;
                goalsConcededTotal += goalsConceded;

                if (goalsScored > goalsConceded)
                    wins++;
                else if (goalsScored == goalsConceded)
                    draws++;
                else
                    losses++;

                if (goalsScored == 0)
                    fts++;

                if (goalsConceded == 0)
                    cs++; 
                
                if (otherTeamGoals.Count > 0 && referenceTeamGoals.Count > 0)
                    bothToScore++;

                FixtureGoalsModel referenceTeamFirstGoal = referenceTeamGoals.FirstOrDefault();
                FixtureGoalsModel otherTeamFirstGoal = otherTeamGoals.FirstOrDefault();

                #region FTS

                if (referenceTeamFirstGoal != null &&
                    (otherTeamFirstGoal == null || (Convert.ToDouble(referenceTeamFirstGoal.Minute, CultureInfo.InvariantCulture) < Convert.ToDouble(otherTeamFirstGoal.Minute, CultureInfo.InvariantCulture))))
                {
                    firstToScore++;
                }

                #endregion

                #region TwoZero

                if (referenceTeamGoals.Count > 1)
                {
                    double referenceSecondGoalMinute = Convert.ToDouble(referenceTeamGoals[1].Minute, CultureInfo.InvariantCulture);

                    if (otherTeamGoals == null || otherTeamGoals.Count == 0)
                        toScoreTwoZero++;
                    else
                    {
                        double otherFirstGoalMinute = Convert.ToDouble(otherTeamGoals[0].Minute, CultureInfo.InvariantCulture);

                        if (referenceSecondGoalMinute < otherFirstGoalMinute)
                            toScoreTwoZero++;
                    }
                }

                #endregion
            }

            winsPercent = countFixtures > 0 ? Math.Round((double)wins / countFixtures, 2) : 0;
            drawsPercent = countFixtures > 0 ? Math.Round((double)draws / countFixtures, 2) : 0;
            lossesPercent = countFixtures > 0 ? Math.Round((double)losses / countFixtures, 2) : 0;
            ftsPercent = countFixtures > 0 ? Math.Round((double)firstToScore / countFixtures, 2) : 0;
            twoZeroPercent = firstToScore > 0 ? Math.Round((double)toScoreTwoZero / firstToScore, 2) : 0;
            failedToScorePercent = countFixtures > 0 ? Math.Round((double)fts / countFixtures, 2) : 0;
            bothToScorePercent = countFixtures > 0 ? Math.Round((double)bothToScore / countFixtures, 2) : 0; 
            cleanSheetsPercent = countFixtures > 0 ? Math.Round((double)cs / countFixtures, 2) : 0;
            averageGoalsScored = countFixtures > 0 ? Math.Round((double)goalsScoredTotal / countFixtures, 2) : 0;
            averageGoalsConceded = countFixtures > 0 ? Math.Round((double)goalsConcededTotal / countFixtures, 2) : 0;

            HTFTCalculate htftCalculate = new HTFTCalculate(
                winsPercent,
                drawsPercent,
                lossesPercent,
                ftsPercent,
                twoZeroPercent,
                failedToScorePercent,
                bothToScorePercent,
                cleanSheetsPercent,
                goalsScoredTotal,
                goalsConcededTotal,
                averageGoalsScored,
                averageGoalsConceded);

            return htftCalculate;
        }

        #endregion
    }
}
