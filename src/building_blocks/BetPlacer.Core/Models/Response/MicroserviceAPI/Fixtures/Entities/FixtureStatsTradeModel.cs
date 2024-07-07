using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.Entities
{
    public class FixtureStatsTradeModel
    {
        public FixtureModel Fixture { get; set; }

        [Key]
        public int Code { get; set; }
        public int FixtureCode { get; set; }

        public int HomeMatchesCountOverall { get; set; }
        public int HomeMatchesCountAtHome { get; set; }

        public int AwayMatchesCountOverall { get; set; }
        public int AwayMatchesCountAtAway { get; set; }

        #region HomeTeam

        #region StatsOverall

        #region Total

        [Column("home_ppg_total")]
        public double HomePPGTotal { get; set; }

        public int HomeWinsTotal { get; set; }
        public double HomeWinsPercentTotal { get; set; }
        public double HomeFirstToScorePercentTotal { get; set; }
        public double HomeToScoreTwoZeroPercentTotal { get; set; }
        public double HomeCleanSheetsPercentTotal { get; set; }
        public double HomeFailedToScorePercentTotal { get; set; }
        public double HomeBothToScorePercentTotal { get; set; }
        public int HomeGoalsScoredTotal { get; set; }
        public int HomeGoalsConcededTotal { get; set; }
        public double HomeAverageGoalsScoredTotal { get; set; }
        public double HomeAverageGoalsConcededTotal { get; set; }

        #endregion

        #region HT

        [Column("home_wins_percent_ht_total")]
        public double HomeWinsPercentHTTotal { get; set; }

        [Column("home_draws_percent_ht_total")]
        public double HomeDrawsPercentHTTotal { get; set; }

        [Column("home_losses_percent_ht_total")]
        public double HomeLossesPercentHTTotal { get; set; }

        [Column("home_first_to_score_percent_ht_total")]
        public double HomeFirstToScorePercentHTTotal { get; set; }

        [Column("home_to_score_two_zero_percent_ht_total")]
        public double HomeToScoreTwoZeroPercentHTTotal { get; set; }

        [Column("home_failed_to_score_percent_ht_total")]
        public double HomeFailedToScorePercentHTTotal { get; set; }

        [Column("home_both_to_score_percent_ht_total")]
        public double HomeBothToScorePercentHTTotal { get; set; }

        [Column("home_clean_sheets_percent_ht_total")]
        public double HomeCleanSheetsPercentHTTotal { get; set; }

        [Column("home_goals_scored_ht_total")]
        public int HomeGoalsScoredHTTotal { get; set; }

        [Column("home_goals_conceded_ht_total")]
        public int HomeGoalsConcededHTTotal { get; set; }

        [Column("home_avg_goals_scored_ht_total")]
        public double HomeAverageGoalsScoredHTTotal { get; set; }

        [Column("home_average_goals_conceded_ht_total")]
        public double HomeAverageGoalsConcededHTTotal { get; set; }

        #endregion

        #region FT

        [Column("home_wins_percent_ft_total")]
        public double HomeWinsPercentFTTotal { get; set; }

        [Column("home_draws_percent_ft_total")]
        public double HomeDrawsPercentFTTotal { get; set; }

        [Column("home_losses_percent_ft_total")]
        public double HomeLossesPercentFTTotal { get; set; }

        [Column("home_first_to_score_percent_ft_total")]
        public double HomeFirstToScorePercentFTTotal { get; set; }

        [Column("home_to_score_two_zero_percent_ft_total")]
        public double HomeToScoreTwoZeroPercentFTTotal { get; set; }

        [Column("home_failed_to_score_percent_ft_total")]
        public double HomeFailedToScorePercentFTTotal { get; set; }

        [Column("home_both_to_score_percent_ft_total")]
        public double HomeBothToScorePercentFTTotal { get; set; }

        [Column("home_clean_sheets_percent_ft_total")]
        public double HomeCleanSheetsPercentFTTotal { get; set; }

        [Column("home_goals_scored_ft_total")]
        public int HomeGoalsScoredFTTotal { get; set; }

        [Column("home_goals_conceded_ft_total")]
        public int HomeGoalsConcededFTTotal { get; set; }

        [Column("home_avg_goals_scored_ft_total")]
        public double HomeAverageGoalsScoredFTTotal { get; set; }

        [Column("home_average_goals_conceded_ft_total")]
        public double HomeAverageGoalsConcededFTTotal { get; set; }

        #endregion

        #region GoalsMoment

        #region Scored

        [Column("home_goals_scored_at_0_to_15")]
        public int HomeGoalsScoredAt0To15 { get; set; }

        [Column("home_goals_scored_at_0_to_15_percent")]
        public double HomeGoalsScoredAt0To15Percent { get; set; }

        [Column("home_goals_scored_at_16_to_30")]
        public int HomeGoalsScoredAt16To30 { get; set; }

        [Column("home_goals_scored_at_16_to_30_percent")]
        public double HomeGoalsScoredAt16To30Percent { get; set; }

        [Column("home_goals_scored_at_31_to_45")]
        public int HomeGoalsScoredAt31To45 { get; set; }

        [Column("home_goals_scored_at_31_to_45_percent")]
        public double HomeGoalsScoredAt31To45Percent { get; set; }

        [Column("home_goals_scored_at_46_to_60")]
        public int HomeGoalsScoredAt46To60 { get; set; }

        [Column("home_goals_scored_at_46_to_60_percent")]
        public double HomeGoalsScoredAt46To60Percent { get; set; }

        [Column("home_goals_scored_at_61_to_75")]
        public int HomeGoalsScoredAt61To75 { get; set; }

        [Column("home_goals_scored_at_61_to_75_percent")]
        public double HomeGoalsScoredAt61To75Percent { get; set; }

        [Column("home_goals_scored_at_75_to_90")]
        public int HomeGoalsScoredAt76To90 { get; set; }

        [Column("home_goals_scored_at_75_to_90_percent")]
        public double HomeGoalsScoredAt76To90Percent { get; set; }

        #endregion

        #region Conceded

        [Column("home_goals_conceded_at_0_to_15")]
        public int HomeGoalsConcededAt0To15 { get; set; }

        [Column("home_goals_conceded_at_0_to_15_percent")]
        public double HomeGoalsConcededAt0To15Percent { get; set; }

        [Column("home_goals_conceded_at_16_to_30")]
        public int HomeGoalsConcededAt16To30 { get; set; }

        [Column("home_goals_conceded_at_16_to_30_percent")]
        public double HomeGoalsConcededAt16To30Percent { get; set; }

        [Column("home_goals_conceded_at_31_to_45")]
        public int HomeGoalsConcededAt31To45 { get; set; }

        [Column("home_goals_conceded_at_31_to_45_percent")]
        public double HomeGoalsConcededAt31To45Percent { get; set; }

        [Column("home_goals_conceded_at_46_to_60")]
        public int HomeGoalsConcededAt46To60 { get; set; }

        [Column("home_goals_conceded_at_46_to_60_percent")]
        public double HomeGoalsConcededAt46To60Percent { get; set; }

        [Column("home_goals_conceded_at_61_to_75")]
        public int HomeGoalsConcededAt61To75 { get; set; }

        [Column("home_goals_conceded_at_61_to_75_percent")]
        public double HomeGoalsConcededAt61To75Percent { get; set; }

        [Column("home_goals_conceded_at_75_to_90")]
        public int HomeGoalsConcededAt76To90 { get; set; }

        [Column("home_goals_conceded_at_75_to_90_percent")]
        public double HomeGoalsConcededAt76To90Percent { get; set; }

        #endregion

        #endregion

        #endregion

        #region StatsAtHome

        #region Total

        [Column("home_ppg_at_home")]
        public double HomePPGAtHome { get; set; }

        public int HomeWinsAtHome { get; set; }
        public double HomeWinsPercentAtHome { get; set; }
        public double HomeFirstToScorePercentAtHome { get; set; }
        public double HomeToScoreTwoZeroPercentAtHome { get; set; }
        public double HomeCleanSheetsPercentAtHome { get; set; }
        public double HomeFailedToScorePercentAtHome { get; set; }
        public double HomeBothToScorePercentAtHome { get; set; }
        public int HomeGoalsScoredAtHome { get; set; }
        public int HomeGoalsConcededAtHome { get; set; }
        public double HomeAverageGoalsScoredAtHome { get; set; }
        public double HomeAverageGoalsConcededAtHome { get; set; }

        #endregion

        #region HT

        [Column("home_wins_percent_ht_at_home")]
        public double HomeWinsPercentHTAtHome { get; set; }

        [Column("home_draws_percent_ht_at_home")]
        public double HomeDrawsPercentHTAtHome { get; set; }

        [Column("home_losses_percent_ht_at_home")]
        public double HomeLossesPercentHTAtHome { get; set; }

        [Column("home_first_to_score_percent_ht_at_home")]
        public double HomeFirstToScorePercentHTAtHome { get; set; }

        [Column("home_to_score_two_zero_percent_ht_at_home")]
        public double HomeToScoreTwoZeroPercentHTAtHome { get; set; }

        [Column("home_failed_to_score_percent_ht_at_home")]
        public double HomeFailedToScorePercentHTAtHome { get; set; }

        [Column("home_both_to_score_percent_ht_at_home")]
        public double HomeBothToScorePercentHTAtHome { get; set; }

        [Column("home_clean_sheets_percent_ht_at_home")]
        public double HomeCleanSheetsPercentHTAtHome { get; set; }

        [Column("home_goals_scored_ht_at_home")]
        public int HomeGoalsScoredHTAtHome { get; set; }

        [Column("home_goals_conceded_ht_at_home")]
        public int HomeGoalsConcededHTAtHome { get; set; }

        [Column("home_avg_goals_scored_ht_at_home")]
        public double HomeAverageGoalsScoredHTAtHome { get; set; }

        [Column("home_avg_goals_conceded_ht_at_home")]
        public double HomeAverageGoalsConcededHTAtHome { get; set; }

        #endregion

        #region FT

        [Column("home_wins_percent_ft_at_home")]
        public double HomeWinsPercentFTAtHome { get; set; }

        [Column("home_draws_percent_ft_at_home")]
        public double HomeDrawsPercentFTAtHome { get; set; }

        [Column("home_losses_percent_ft_at_home")]
        public double HomeLossesPercentFTAtHome { get; set; }

        [Column("home_first_to_score_percent_ft_at_home")]
        public double HomeFirstToScorePercentFTAtHome { get; set; }

        [Column("home_to_score_two_zero_percent_ft_at_home")]
        public double HomeToScoreTwoZeroPercentFTAtHome { get; set; }

        [Column("home_failed_to_score_percent_ft_at_home")]
        public double HomeFailedToScorePercentFTAtHome { get; set; }

        [Column("home_both_to_score_percent_ft_at_home")]
        public double HomeBothToScorePercentFTAtHome { get; set; }

        [Column("home_clean_sheets_percent_ft_at_home")]
        public double HomeCleanSheetsPercentFTAtHome { get; set; }

        [Column("home_goals_scored_ft_at_home")]
        public int HomeGoalsScoredFTAtHome { get; set; }

        [Column("home_goals_conceded_ft_at_home")]
        public int HomeGoalsConcededFTAtHome { get; set; }

        [Column("home_avg_goals_scored_ft_at_home")]
        public double HomeAverageGoalsScoredFTAtHome { get; set; }

        [Column("home_avg_goals_conceded_ft_at_home")]
        public double HomeAverageGoalsConcededFTAtHome { get; set; }

        #endregion

        #region GoalsMoment

        #region Scored

        [Column("home_goals_scored_at_0_to_15_at_home")]
        public int HomeGoalsScoredAt0To15AtHome { get; set; }

        [Column("home_goals_scored_at_0_to_15_percent_at_home")]
        public double HomeGoalsScoredAt0To15PercentAtHome { get; set; }

        [Column("home_goals_scored_at_15_to_30_at_home")]
        public int HomeGoalsScoredAt16To30AtHome { get; set; }

        [Column("home_goals_scored_at_15_to_30_percent_at_home")]
        public double HomeGoalsScoredAt16To30PercentAtHome { get; set; }

        [Column("home_goals_scored_at_31_to_45_at_home")]
        public int HomeGoalsScoredAt31To45AtHome { get; set; }

        [Column("home_goals_scored_at_31_to_45_percent_at_home")]
        public double HomeGoalsScoredAt31To45PercentAtHome { get; set; }

        [Column("home_goals_scored_at_46_to_60_at_home")]
        public int HomeGoalsScoredAt46To60AtHome { get; set; }

        [Column("home_goals_scored_at_46_to_60_percent_at_home")]
        public double HomeGoalsScoredAt46To60PercentAtHome { get; set; }

        [Column("home_goals_scored_at_61_to_75_at_home")]
        public int HomeGoalsScoredAt61To75AtHome { get; set; }

        [Column("home_goals_scored_at_61_to_75_percent_at_home")]
        public double HomeGoalsScoredAt61To75PercentAtHome { get; set; }

        [Column("home_goals_scored_at_76_to_90_at_home")]
        public int HomeGoalsScoredAt76To90AtHome { get; set; }

        [Column("home_goals_scored_at_76_to_90_percent_at_home")]
        public double HomeGoalsScoredAt76To90PercentAtHome { get; set; }

        #endregion

        #region Conceded

        [Column("home_goals_conceded_at_0_to_15_at_home")]
        public int HomeGoalsConcededAt0To15AtHome { get; set; }

        [Column("home_goals_conceded_at_0_to_15_percent_at_home")]
        public double HomeGoalsConcededAt0To15PercentAtHome { get; set; }

        [Column("home_goals_conceded_at_15_to_30_at_home")]
        public int HomeGoalsConcededAt16To30AtHome { get; set; }

        [Column("home_goals_conceded_at_15_to_30_percent_at_home")]
        public double HomeGoalsConcededAt16To30PercentAtHome { get; set; }

        [Column("home_goals_conceded_at_31_to_45_at_home")]
        public int HomeGoalsConcededAt31To45AtHome { get; set; }

        [Column("home_goals_conceded_at_31_to_45_percent_at_home")]
        public double HomeGoalsConcededAt31To45PercentAtHome { get; set; }

        [Column("home_goals_conceded_at_46_to_60_at_home")]
        public int HomeGoalsConcededAt46To60AtHome { get; set; }

        [Column("home_goals_conceded_at_46_to_60_percent_at_home")]
        public double HomeGoalsConcededAt46To60PercentAtHome { get; set; }

        [Column("home_goals_conceded_at_61_to_75_at_home")]
        public int HomeGoalsConcededAt61To75AtHome { get; set; }

        [Column("home_goals_conceded_at_61_to_75_percent_at_home")]
        public double HomeGoalsConcededAt61To75PercentAtHome { get; set; }

        [Column("home_goals_conceded_at_76_to_90_at_home")]
        public int HomeGoalsConcededAt76To90AtHome { get; set; }

        [Column("home_goals_conceded_at_76_to_90_percent_at_home")]
        public double HomeGoalsConcededAt76To90PercentAtHome { get; set; }

        #endregion

        #endregion

        #endregion

        #endregion

        #region AwayTeam

        #region StatsOverall

        #region Total

        [Column("away_ppg_total")]
        public double AwayPPGTotal { get; set; }

        public int AwayWinsTotal { get; set; }
        public double AwayWinsPercentTotal { get; set; }
        public double AwayFirstToScorePercentTotal { get; set; }
        public double AwayToScoreTwoZeroPercentTotal { get; set; }
        public double AwayCleanSheetsPercentTotal { get; set; }
        public double AwayFailedToScorePercentTotal { get; set; }
        public double AwayBothToScorePercentTotal { get; set; }
        public int AwayGoalsScoredTotal { get; set; }
        public int AwayGoalsConcededTotal { get; set; }
        public double AwayAverageGoalsScoredTotal { get; set; }
        public double AwayAverageGoalsConcededTotal { get; set; }

        #endregion

        #region HT

        [Column("away_wins_percent_ht_total")]
        public double AwayWinsPercentHTTotal { get; set; }

        [Column("away_draws_percent_ht_total")]
        public double AwayDrawsPercentHTTotal { get; set; }

        [Column("away_losses_percent_ht_total")]
        public double AwayLossesPercentHTTotal { get; set; }

        [Column("away_first_to_score_percent_ht_total")]
        public double AwayFirstToScorePercentHTTotal { get; set; }

        [Column("away_to_score_two_zero_percent_ht_total")]
        public double AwayToScoreTwoZeroPercentHTTotal { get; set; }

        [Column("away_failed_to_score_percent_ht_total")]
        public double AwayFailedToScorePercentHTTotal { get; set; }

        [Column("away_both_to_score_percent_ht_total")]
        public double AwayBothToScorePercentHTTotal { get; set; }

        [Column("away_clean_sheets_percent_ht_total")]
        public double AwayCleanSheetsPercentHTTotal { get; set; }

        [Column("away_goals_scored_ht_total")]
        public int AwayGoalsScoredHTTotal { get; set; }

        [Column("away_goals_conceded_ht_total")]
        public int AwayGoalsConcededHTTotal { get; set; }

        [Column("away_avg_goals_scored_ht_total")]
        public double AwayAverageGoalsScoredHTTotal { get; set; }

        [Column("away_average_goals_conceded_ht_total")]
        public double AwayAverageGoalsConcededHTTotal { get; set; }

        #endregion

        #region FT

        [Column("away_wins_percent_ft_total")]
        public double AwayWinsPercentFTTotal { get; set; }

        [Column("away_draws_percent_ft_total")]
        public double AwayDrawsPercentFTTotal { get; set; }

        [Column("away_losses_percent_ft_total")]
        public double AwayLossesPercentFTTotal { get; set; }

        [Column("away_first_to_score_percent_ft_total")]
        public double AwayFirstToScorePercentFTTotal { get; set; }

        [Column("away_to_score_two_zero_percent_ft_total")]
        public double AwayToScoreTwoZeroPercentFTTotal { get; set; }

        [Column("away_failed_to_score_percent_ft_total")]
        public double AwayFailedToScorePercentFTTotal { get; set; }

        [Column("away_both_to_score_percent_ft_total")]
        public double AwayBothToScorePercentFTTotal { get; set; }

        [Column("away_clean_sheets_percent_ft_total")]
        public double AwayCleanSheetsPercentFTTotal { get; set; }

        [Column("away_goals_scored_ft_total")]
        public int AwayGoalsScoredFTTotal { get; set; }

        [Column("away_goals_conceded_ft_total")]
        public int AwayGoalsConcededFTTotal { get; set; }

        [Column("away_avg_goals_scored_ft_total")]
        public double AwayAverageGoalsScoredFTTotal { get; set; }

        [Column("away_average_goals_conceded_ft_total")]
        public double AwayAverageGoalsConcededFTTotal { get; set; }

        #endregion

        #region GoalsMoment

        #region Scored

        [Column("away_goals_scored_at_0_to_15")]
        public int AwayGoalsScoredAt0To15 { get; set; }

        [Column("away_goals_scored_at_0_to_15_percent")]
        public double AwayGoalsScoredAt0To15Percent { get; set; }

        [Column("away_goals_scored_at_16_to_30")]
        public int AwayGoalsScoredAt16To30 { get; set; }

        [Column("away_goals_scored_at_16_to_30_percent")]
        public double AwayGoalsScoredAt16To30Percent { get; set; }

        [Column("away_goals_scored_at_31_to_45")]
        public int AwayGoalsScoredAt31To45 { get; set; }

        [Column("away_goals_scored_at_31_to_45_percent")]
        public double AwayGoalsScoredAt31To45Percent { get; set; }

        [Column("away_goals_scored_at_46_to_60")]
        public int AwayGoalsScoredAt46To60 { get; set; }

        [Column("away_goals_scored_at_46_to_60_percent")]
        public double AwayGoalsScoredAt46To60Percent { get; set; }

        [Column("away_goals_scored_at_61_to_75")]
        public int AwayGoalsScoredAt61To75 { get; set; }

        [Column("away_goals_scored_at_61_to_75_percent")]
        public double AwayGoalsScoredAt61To75Percent { get; set; }

        [Column("away_goals_scored_at_75_to_90")]
        public int AwayGoalsScoredAt76To90 { get; set; }

        [Column("away_goals_scored_at_75_to_90_percent")]
        public double AwayGoalsScoredAt76To90Percent { get; set; }

        #endregion

        #region Conceded

        [Column("away_goals_conceded_at_0_to_15")]
        public int AwayGoalsConcededAt0To15 { get; set; }

        [Column("away_goals_conceded_at_0_to_15_percent")]
        public double AwayGoalsConcededAt0To15Percent { get; set; }

        [Column("away_goals_conceded_at_16_to_30")]
        public int AwayGoalsConcededAt16To30 { get; set; }

        [Column("away_goals_conceded_at_16_to_30_percent")]
        public double AwayGoalsConcededAt16To30Percent { get; set; }

        [Column("away_goals_conceded_at_31_to_45")]
        public int AwayGoalsConcededAt31To45 { get; set; }

        [Column("away_goals_conceded_at_31_to_45_percent")]
        public double AwayGoalsConcededAt31To45Percent { get; set; }

        [Column("away_goals_conceded_at_46_to_60")]
        public int AwayGoalsConcededAt46To60 { get; set; }

        [Column("away_goals_conceded_at_46_to_60_percent")]
        public double AwayGoalsConcededAt46To60Percent { get; set; }

        [Column("away_goals_conceded_at_61_to_75")]
        public int AwayGoalsConcededAt61To75 { get; set; }

        [Column("away_goals_conceded_at_61_to_75_percent")]
        public double AwayGoalsConcededAt61To75Percent { get; set; }

        [Column("away_goals_conceded_at_75_to_90")]
        public int AwayGoalsConcededAt76To90 { get; set; }

        [Column("away_goals_conceded_at_75_to_90_percent")]
        public double AwayGoalsConcededAt76To90Percent { get; set; }

        #endregion

        #endregion

        #endregion

        #region StatsAtAway

        #region Total

        [Column("away_ppg_at_away")]
        public double AwayPPGAtAway { get; set; }

        public int AwayWinsAtAway { get; set; }
        public double AwayWinsPercentAtAway { get; set; }
        public double AwayFirstToScorePercentAtAway { get; set; }
        public double AwayToScoreTwoZeroPercentAtAway { get; set; }
        public double AwayCleanSheetsPercentAtAway { get; set; }
        public double AwayFailedToScorePercentAtAway { get; set; }
        public double AwayBothToScorePercentAtAway { get; set; }
        public int AwayGoalsScoredAtAway { get; set; }
        public int AwayGoalsConcededAtAway { get; set; }
        public double AwayAverageGoalsScoredAtAway { get; set; }
        public double AwayAverageGoalsConcededAtAway { get; set; }

        #endregion

        #region HT

        [Column("away_wins_percent_ht_at_away")]
        public double AwayWinsPercentHTAtAway { get; set; }

        [Column("away_draws_percent_ht_at_away")]
        public double AwayDrawsPercentHTAtAway { get; set; }

        [Column("away_losses_percent_ht_at_away")]
        public double AwayLossesPercentHTAtAway { get; set; }

        [Column("away_first_to_score_percent_ht_at_away")]
        public double AwayFirstToScorePercentHTAtAway { get; set; }

        [Column("away_to_score_two_zero_percent_ht_at_away")]
        public double AwayToScoreTwoZeroPercentHTAtAway { get; set; }

        [Column("away_failed_to_score_percent_ht_at_away")]
        public double AwayFailedToScorePercentHTAtAway { get; set; }

        [Column("away_both_to_score_percent_ht_at_away")]
        public double AwayBothToScorePercentHTAtAway { get; set; }

        [Column("away_clean_sheets_percent_ht_at_away")]
        public double AwayCleanSheetsPercentHTAtAway { get; set; }

        [Column("away_goals_scored_ht_at_away")]
        public int AwayGoalsScoredHTAtAway { get; set; }

        [Column("away_goals_conceded_ht_at_away")]
        public int AwayGoalsConcededHTAtAway { get; set; }

        [Column("away_avg_goals_scored_ht_at_away")]
        public double AwayAverageGoalsScoredHTAtAway { get; set; }

        [Column("away_avg_goals_conceded_ht_at_away")]
        public double AwayAverageGoalsConcededHTAtAway { get; set; }

        #endregion

        #region FT

        [Column("away_wins_percent_ft_at_away")]
        public double AwayWinsPercentFTAtAway { get; set; }

        [Column("away_draws_percent_ft_at_away")]
        public double AwayDrawsPercentFTAtAway { get; set; }

        [Column("away_losses_percent_ft_at_away")]
        public double AwayLossesPercentFTAtAway { get; set; }

        [Column("away_first_to_score_percent_ft_at_away")]
        public double AwayFirstToScorePercentFTAtAway { get; set; }

        [Column("away_to_score_two_zero_percent_ft_at_away")]
        public double AwayToScoreTwoZeroPercentFTAtAway { get; set; }

        [Column("away_failed_to_score_percent_ft_at_away")]
        public double AwayFailedToScorePercentFTAtAway { get; set; }

        [Column("away_both_to_score_percent_ft_at_away")]
        public double AwayBothToScorePercentFTAtAway { get; set; }

        [Column("away_clean_sheets_percent_ft_at_away")]
        public double AwayCleanSheetsPercentFTAtAway { get; set; }

        [Column("away_goals_scored_ft_at_away")]
        public int AwayGoalsScoredFTAtAway { get; set; }

        [Column("away_goals_conceded_ft_at_away")]
        public int AwayGoalsConcededFTAtAway { get; set; }

        [Column("away_avg_goals_scored_ft_at_away")]
        public double AwayAverageGoalsScoredFTAtAway { get; set; }

        [Column("away_avg_goals_conceded_ft_at_away")]
        public double AwayAverageGoalsConcededFTAtAway { get; set; }

        #endregion

        #region GoalsMoment

        #region Scored

        [Column("away_goals_scored_at_0_to_15_at_away")]
        public int AwayGoalsScoredAt0To15AtAway { get; set; }

        [Column("away_goals_scored_at_0_to_15_percent_at_away")]
        public double AwayGoalsScoredAt0To15PercentAtAway { get; set; }

        [Column("away_goals_scored_at_15_to_30_at_away")]
        public int AwayGoalsScoredAt16To30AtAway { get; set; }

        [Column("away_goals_scored_at_15_to_30_percent_at_away")]
        public double AwayGoalsScoredAt16To30PercentAtAway { get; set; }

        [Column("away_goals_scored_at_31_to_45_at_away")]
        public int AwayGoalsScoredAt31To45AtAway { get; set; }

        [Column("away_goals_scored_at_31_to_45_percent_at_away")]
        public double AwayGoalsScoredAt31To45PercentAtAway { get; set; }

        [Column("away_goals_scored_at_46_to_60_at_away")]
        public int AwayGoalsScoredAt46To60AtAway { get; set; }

        [Column("away_goals_scored_at_46_to_60_percent_at_away")]
        public double AwayGoalsScoredAt46To60PercentAtAway { get; set; }

        [Column("away_goals_scored_at_61_to_75_at_away")]
        public int AwayGoalsScoredAt61To75AtAway { get; set; }

        [Column("away_goals_scored_at_61_to_75_percent_at_away")]
        public double AwayGoalsScoredAt61To75PercentAtAway { get; set; }

        [Column("away_goals_scored_at_76_to_90_at_away")]
        public int AwayGoalsScoredAt76To90AtAway { get; set; }

        [Column("away_goals_scored_at_76_to_90_percent_at_away")]
        public double AwayGoalsScoredAt76To90PercentAtAway { get; set; }

        #endregion

        #region Conceded

        [Column("away_goals_conceded_at_0_to_15_at_away")]
        public int AwayGoalsConcededAt0To15AtAway { get; set; }

        [Column("away_goals_conceded_at_0_to_15_percent_at_away")]
        public double AwayGoalsConcededAt0To15PercentAtAway { get; set; }

        [Column("away_goals_conceded_at_15_to_30_at_away")]
        public int AwayGoalsConcededAt16To30AtAway { get; set; }

        [Column("away_goals_conceded_at_15_to_30_percent_at_away")]
        public double AwayGoalsConcededAt16To30PercentAtAway { get; set; }

        [Column("away_goals_conceded_at_31_to_45_at_away")]
        public int AwayGoalsConcededAt31To45AtAway { get; set; }

        [Column("away_goals_conceded_at_31_to_45_percent_at_away")]
        public double AwayGoalsConcededAt31To45PercentAtAway { get; set; }

        [Column("away_goals_conceded_at_46_to_60_at_away")]
        public int AwayGoalsConcededAt46To60AtAway { get; set; }

        [Column("away_goals_conceded_at_46_to_60_percent_at_away")]
        public double AwayGoalsConcededAt46To60PercentAtAway { get; set; }

        [Column("away_goals_conceded_at_61_to_75_at_away")]
        public int AwayGoalsConcededAt61To75AtAway { get; set; }

        [Column("away_goals_conceded_at_61_to_75_percent_at_away")]
        public double AwayGoalsConcededAt61To75PercentAtAway { get; set; }

        [Column("away_goals_conceded_at_76_to_90_at_away")]
        public int AwayGoalsConcededAt76To90AtAway { get; set; }

        [Column("away_goals_conceded_at_76_to_90_percent_at_away")]
        public double AwayGoalsConcededAt76To90PercentAtAway { get; set; }

        #endregion

        #endregion

        #endregion

        #endregion
    }
}
