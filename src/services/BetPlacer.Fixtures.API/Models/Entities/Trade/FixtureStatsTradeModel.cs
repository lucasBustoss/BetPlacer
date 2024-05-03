using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Fixtures.API.Models.Entities.Trade
{
    public class FixtureStatsTradeModel
    {
        public FixtureModel Fixture { get; set; }

        [Key]
        public int Code { get; set; }
        public int FixtureCode { get; set; }

        #region HomeTeam

        #region StatsOverall

        #region Total

        public double HomePPGTotal { get; set; }
        public int HomeWinsTotal { get; set; }
        public double HomeWinsPercentTotal { get; set; }
        public double HomeFirstToScorePercentTotal { get; set; }
        public double HomeFixturesWithoutConcededGoalsPercentTotal { get; set; }
        public double HomeFailedToScorePercentTotal { get; set; }
        public double HomeBothToScorePercentTotal { get; set; }
        public int HomeGoalsScoredTotal { get; set; }
        public int HomeGoalsConcededTotal { get; set; }
        public double HomeAverageGoalsScoredTotal { get; set; }
        public double HomeAverageGoalsConcededTotal { get; set; }

        #endregion

        #region HT

        public double HomeWinsPercentHTTotal { get; set; }
        public double HomeDrawsPercentHTTotal { get; set; }
        public double HomeLossesPercentHTTotal { get; set; }
        public double HomeFailedToScorePercentHTTotal { get; set; }
        public double HomeFixturesWithoutConcededGoalsPercentHTTotal { get; set; }
        public int HomeGoalsScoredHTTotal { get; set; }
        public int HomeGoalsConcededHTTotal { get; set; }
        public double HomeAverageGoalsScoredHTTotal { get; set; }
        public double HomeAverageGoalsConcededHTTotal { get; set; }

        #endregion

        #region FT

        public double HomeWinsPercentFTTotal { get; set; }
        public double HomeDrawsPercentFTTotal { get; set; }
        public double HomeLossesPercentFTTotal { get; set; }
        public double HomeFailedToScorePercentFTTotal { get; set; }
        public double HomeFixturesWithoutConcededGoalsPercentFTTotal { get; set; }
        public int HomeGoalsScoredFTTotal { get; set; }
        public int HomeGoalsConcededFTTotal { get; set; }
        public double HomeAverageGoalsScoredFTTotal { get; set; }
        public double HomeAverageGoalsConcededFTTotal { get; set; }

        #endregion

        #region GoalsMoment

        #region Scored

        public int HomeGoalsScoredAt00To15 { get; set; }
        public double HomeGoalsScoredAt00To15Percent { get; set; }
        public int HomeGoalsScoredAt16To30 { get; set; }
        public double HomeGoalsScoredAt16To30Percent { get; set; }
        public int HomeGoalsScoredAt31To45 { get; set; }
        public double HomeGoalsScoredAt31To45Percent { get; set; }
        public int HomeGoalsScoredAt46To60 { get; set; }
        public double HomeGoalsScoredAt46To60Percent { get; set; }
        public int HomeGoalsScoredAt61To75 { get; set; }
        public double HomeGoalsScoredAt61To75Percent { get; set; }
        public int HomeGoalsScoredAt76To90 { get; set; }
        public double HomeGoalsScoredAt76To90Percent { get; set; }

        #endregion

        #region Conceded

        public int HomeGoalsConcededAt00To15 { get; set; }
        public double HomeGoalsConcededAt00To15Percent { get; set; }
        public int HomeGoalsConcededAt16To30 { get; set; }
        public double HomeGoalsConcededAt16To30Percent { get; set; }
        public int HomeGoalsConcededAt31To45 { get; set; }
        public double HomeGoalsConcededAt31To45Percent { get; set; }
        public int HomeGoalsConcededAt46To60 { get; set; }
        public double HomeGoalsConcededAt46To60Percent { get; set; }
        public int HomeGoalsConcededAt61To75 { get; set; }
        public double HomeGoalsConcededAt61To75Percent { get; set; }
        public int HomeGoalsConcededAt76To90 { get; set; }
        public double HomeGoalsConcededAt76To90Percent { get; set; }

        #endregion

        #endregion

        #endregion

        #region StatsAtHome

        #region Total

        public double HomePPGTotalAtHome { get; set; }
        public int HomeWinsTotalAtHome { get; set; }
        public double HomeWinsPercentTotalAtHome { get; set; }
        public double HomeFirstToScorePercentTotalAtHome { get; set; }
        public double HomeFixturesWithoutConcededGoalsPercentTotalAtHome { get; set; }
        public double HomeFailedToScorePercentTotalAtHome { get; set; }
        public double HomeBothToScorePercentTotalAtHome { get; set; }
        public int HomeGoalsScoredTotalAtHome { get; set; }
        public int HomeGoalsConcededTotalAtHome { get; set; }
        public double HomeAverageGoalsScoredTotalAtHome { get; set; }
        public double HomeAverageGoalsConcededTotalAtHome { get; set; }

        #endregion

        #region HT

        public double HomeWinsPercentHTTotalAtHome { get; set; }
        public double HomeDrawsPercentHTTotalAtHome { get; set; }
        public double HomeLossesPercentHTTotalAtHome { get; set; }
        public double HomeFailedToScorePercentHTTotalAtHome { get; set; }
        public double HomeFixturesWithoutConcededGoalsPercentHTTotalAtHome { get; set; }
        public int HomeGoalsScoredHTTotalAtHome { get; set; }
        public int HomeGoalsConcededHTTotalAtHome { get; set; }
        public double HomeAverageGoalsScoredHTTotalAtHome { get; set; }
        public double HomeAverageGoalsConcededHTTotalAtHome { get; set; }

        #endregion

        #region FT

        public double HomeWinsPercentFTTotalAtHome { get; set; }
        public double HomeDrawsPercentFTTotalAtHome { get; set; }
        public double HomeLossesPercentFTTotalAtHome { get; set; }
        public double HomeFailedToScorePercentFTTotalAtHome { get; set; }
        public double HomeFixturesWithoutConcededGoalsPercentFTTotalAtHome { get; set; }
        public int HomeGoalsScoredFTTotalAtHome { get; set; }
        public int HomeGoalsConcededFTTotalAtHome { get; set; }
        public double HomeAverageGoalsScoredFTTotalAtHome { get; set; }
        public double HomeAverageGoalsConcededFTTotalAtHome { get; set; }

        #endregion        
        
        #region GoalsMoment

        #region Scored

        public int HomeGoalsScoredAt00To15AtHome { get; set; }
        public double HomeGoalsScoredAt00To15PercentAtHome { get; set; }
        public int HomeGoalsScoredAt16To30AtHome { get; set; }
        public double HomeGoalsScoredAt16To30PercentAtHome { get; set; }
        public int HomeGoalsScoredAt31To45AtHome { get; set; }
        public double HomeGoalsScoredAt31To45PercentAtHome { get; set; }
        public int HomeGoalsScoredAt46To60AtHome { get; set; }
        public double HomeGoalsScoredAt46To60PercentAtHome { get; set; }
        public int HomeGoalsScoredAt61To75AtHome { get; set; }
        public double HomeGoalsScoredAt61To75PercentAtHome { get; set; }
        public int HomeGoalsScoredAt76To90AtHome { get; set; }
        public double HomeGoalsScoredAt76To90PercentAtHome { get; set; }

        #endregion

        #region Conceded

        public int HomeGoalsConcededAt00To15AtHome { get; set; }
        public double HomeGoalsConcededAt00To15PercentAtHome { get; set; }
        public int HomeGoalsConcededAt16To30AtHome { get; set; }
        public double HomeGoalsConcededAt16To30PercentAtHome { get; set; }
        public int HomeGoalsConcededAt31To45AtHome { get; set; }
        public double HomeGoalsConcededAt31To45PercentAtHome { get; set; }
        public int HomeGoalsConcededAt46To60AtHome { get; set; }
        public double HomeGoalsConcededAt46To60PercentAtHome { get; set; }
        public int HomeGoalsConcededAt61To75AtHome { get; set; }
        public double HomeGoalsConcededAt61To75PercentAtHome { get; set; }
        public int HomeGoalsConcededAt76To90AtHome { get; set; }
        public double HomeGoalsConcededAt76To90PercentAtHome { get; set; }

        #endregion

        #endregion

        #endregion

        #endregion

        #region AwayTeam

        #region StatsOverall

        #region Total

        public double AwayPPGTotal { get; set; }
        public int AwayWinsTotal { get; set; }
        public double AwayWinsPercentTotal { get; set; }
        public double AwayFirstToScorePercentTotal { get; set; }
        public double AwayFixturesWithoutConcededGoalsPercentTotal { get; set; }
        public double AwayFailedToScorePercentTotal { get; set; }
        public double AwayBothToScorePercentTotal { get; set; }
        public int AwayGoalsScoredTotal { get; set; }
        public int AwayGoalsConcededTotal { get; set; }
        public double AwayAverageGoalsScoredTotal { get; set; }
        public double AwayAverageGoalsConcededTotal { get; set; }

        #endregion

        #region HT

        public double AwayWinsPercentHTTotal { get; set; }
        public double AwayDrawsPercentHTTotal { get; set; }
        public double AwayLossesPercentHTTotal { get; set; }
        public double AwayFailedToScorePercentHTTotal { get; set; }
        public double AwayFixturesWithoutConcededGoalsPercentHTTotal { get; set; }
        public int AwayGoalsScoredHTTotal { get; set; }
        public int AwayGoalsConcededHTTotal { get; set; }
        public double AwayAverageGoalsScoredHTTotal { get; set; }
        public double AwayAverageGoalsConcededHTTotal { get; set; }

        #endregion

        #region FT

        public double AwayWinsPercentFTTotal { get; set; }
        public double AwayDrawsPercentFTTotal { get; set; }
        public double AwayLossesPercentFTTotal { get; set; }
        public double AwayFailedToScorePercentFTTotal { get; set; }
        public double AwayFixturesWithoutConcededGoalsPercentFTTotal { get; set; }
        public int AwayGoalsScoredFTTotal { get; set; }
        public int AwayGoalsConcededFTTotal { get; set; }
        public double AwayAverageGoalsScoredFTTotal { get; set; }
        public double AwayAverageGoalsConcededFTTotal { get; set; }

        #endregion

        #region GoalsMoment

        #region Scored

        public int AwayGoalsScoredAt00To15 { get; set; }
        public double AwayGoalsScoredAt00To15Percent { get; set; }
        public int AwayGoalsScoredAt16To30 { get; set; }
        public double AwayGoalsScoredAt16To30Percent { get; set; }
        public int AwayGoalsScoredAt31To45 { get; set; }
        public double AwayGoalsScoredAt31To45Percent { get; set; }
        public int AwayGoalsScoredAt46To60 { get; set; }
        public double AwayGoalsScoredAt46To60Percent { get; set; }
        public int AwayGoalsScoredAt61To75 { get; set; }
        public double AwayGoalsScoredAt61To75Percent { get; set; }
        public int AwayGoalsScoredAt76To90 { get; set; }
        public double AwayGoalsScoredAt76To90Percent { get; set; }

        #endregion

        #region Conceded

        public int AwayGoalsConcededAt00To15 { get; set; }
        public double AwayGoalsConcededAt00To15Percent { get; set; }
        public int AwayGoalsConcededAt16To30 { get; set; }
        public double AwayGoalsConcededAt16To30Percent { get; set; }
        public int AwayGoalsConcededAt31To45 { get; set; }
        public double AwayGoalsConcededAt31To45Percent { get; set; }
        public int AwayGoalsConcededAt46To60 { get; set; }
        public double AwayGoalsConcededAt46To60Percent { get; set; }
        public int AwayGoalsConcededAt61To75 { get; set; }
        public double AwayGoalsConcededAt61To75Percent { get; set; }
        public int AwayGoalsConcededAt76To90 { get; set; }
        public double AwayGoalsConcededAt76To90Percent { get; set; }

        #endregion

        #endregion

        #endregion

        #region StatsAtAway

        #region Total

        public double AwayPPGTotalAtAway { get; set; }
        public int AwayWinsTotalAtAway { get; set; }
        public double AwayWinsPercentTotalAtAway { get; set; }
        public double AwayFirstToScorePercentTotalAtAway { get; set; }
        public double AwayFixturesWithoutConcededGoalsPercentTotalAtAway { get; set; }
        public double AwayFailedToScorePercentTotalAtAway { get; set; }
        public double AwayBothToScorePercentTotalAtAway { get; set; }
        public int AwayGoalsScoredTotalAtAway { get; set; }
        public int AwayGoalsConcededTotalAtAway { get; set; }
        public double AwayAverageGoalsScoredTotalAtAway { get; set; }
        public double AwayAverageGoalsConcededTotalAtAway { get; set; }

        #endregion

        #region HT

        public double AwayWinsPercentHTTotalAtAway { get; set; }
        public double AwayDrawsPercentHTTotalAtAway { get; set; }
        public double AwayLossesPercentHTTotalAtAway { get; set; }
        public double AwayFailedToScorePercentHTTotalAtAway { get; set; }
        public double AwayFixturesWithoutConcededGoalsPercentHTTotalAtAway { get; set; }
        public int AwayGoalsScoredHTTotalAtAway { get; set; }
        public int AwayGoalsConcededHTTotalAtAway { get; set; }
        public double AwayAverageGoalsScoredHTTotalAtAway { get; set; }
        public double AwayAverageGoalsConcededHTTotalAtAway { get; set; }

        #endregion

        #region FT

        public double AwayWinsPercentFTTotalAtAway { get; set; }
        public double AwayDrawsPercentFTTotalAtAway { get; set; }
        public double AwayLossesPercentFTTotalAtAway { get; set; }
        public double AwayFailedToScorePercentFTTotalAtAway { get; set; }
        public double AwayFixturesWithoutConcededGoalsPercentFTTotalAtAway { get; set; }
        public int AwayGoalsScoredFTTotalAtAway { get; set; }
        public int AwayGoalsConcededFTTotalAtAway { get; set; }
        public double AwayAverageGoalsScoredFTTotalAtAway { get; set; }
        public double AwayAverageGoalsConcededFTTotalAtAway { get; set; }

        #endregion

        #region GoalsMoment

        #region Scored

        public int AwayGoalsScoredAt00To15AtAway { get; set; }
        public double AwayGoalsScoredAt00To15PercentAtAway { get; set; }
        public int AwayGoalsScoredAt16To30AtAway { get; set; }
        public double AwayGoalsScoredAt16To30PercentAtAway { get; set; }
        public int AwayGoalsScoredAt31To45AtAway { get; set; }
        public double AwayGoalsScoredAt31To45PercentAtAway { get; set; }
        public int AwayGoalsScoredAt46To60AtAway { get; set; }
        public double AwayGoalsScoredAt46To60PercentAtAway { get; set; }
        public int AwayGoalsScoredAt61To75AtAway { get; set; }
        public double AwayGoalsScoredAt61To75PercentAtAway { get; set; }
        public int AwayGoalsScoredAt76To90AtAway { get; set; }
        public double AwayGoalsScoredAt76To90PercentAtAway { get; set; }

        #endregion

        #region Conceded

        public int AwayGoalsConcededAt00To15AtAway { get; set; }
        public double AwayGoalsConcededAt00To15PercentAtAway { get; set; }
        public int AwayGoalsConcededAt16To30AtAway { get; set; }
        public double AwayGoalsConcededAt16To30PercentAtAway { get; set; }
        public int AwayGoalsConcededAt31To45AtAway { get; set; }
        public double AwayGoalsConcededAt31To45PercentAtAway { get; set; }
        public int AwayGoalsConcededAt46To60AtAway { get; set; }
        public double AwayGoalsConcededAt46To60PercentAtAway { get; set; }
        public int AwayGoalsConcededAt61To75AtAway { get; set; }
        public double AwayGoalsConcededAt61To75PercentAtAway { get; set; }
        public int AwayGoalsConcededAt76To90AtAway { get; set; }
        public double AwayGoalsConcededAt76To90PercentAtAway { get; set; }

        #endregion

        #endregion

        #endregion

        #endregion
    }
}
