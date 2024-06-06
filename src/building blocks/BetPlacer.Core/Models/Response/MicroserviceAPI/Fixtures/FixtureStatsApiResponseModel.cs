using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures
{
    public class FixtureStatsApiResponseModel
    {
        [JsonPropertyName("homeWinsTotal")]
        public int HomeWinsTotal { get; set; }

        [JsonPropertyName("homeWinsPercentTotal")]
        public double HomeWinsPercentTotal { get; set; }

        [JsonPropertyName("homeFirstToScorePercentTotal")]
        public double HomeFirstToScorePercentTotal { get; set; }

        [JsonPropertyName("homeToScoreTwoZeroPercentTotal")]
        public double HomeToScoreTwoZeroPercentTotal { get; set; }

        [JsonPropertyName("homeCleanSheetsPercentTotal")]
        public double HomeCleanSheetsPercentTotal { get; set; }

        [JsonPropertyName("homeFailedToScorePercentTotal")]
        public double HomeFailedToScorePercentTotal { get; set; }

        [JsonPropertyName("homeBothToScorePercentTotal")]
        public double HomeBothToScorePercentTotal { get; set; }

        [JsonPropertyName("homeGoalsScoredTotal")]
        public int HomeGoalsScoredTotal { get; set; }

        [JsonPropertyName("homeGoalsConcededTotal")]
        public int HomeGoalsConcededTotal { get; set; }

        [JsonPropertyName("homeAverageGoalsScoredTotal")]
        public double HomeAverageGoalsScoredTotal { get; set; }

        [JsonPropertyName("homeAverageGoalsConcededTotal")]
        public double HomeAverageGoalsConcededTotal { get; set; }

        [JsonPropertyName("homeWinsPercentHTTotal")]
        public double HomeWinsPercentHTTotal { get; set; }

        [JsonPropertyName("homeDrawsPercentHTTotal")]
        public double HomeDrawsPercentHTTotal { get; set; }

        [JsonPropertyName("homeLossesPercentHTTotal")]
        public double HomeLossesPercentHTTotal { get; set; }

        [JsonPropertyName("homeFirstToScorePercentHTTotal")]
        public double HomeFirstToScorePercentHTTotal { get; set; }

        [JsonPropertyName("homeToScoreTwoZeroPercentHTTotal")]
        public double HomeToScoreTwoZeroPercentHTTotal { get; set; }

        [JsonPropertyName("homeFailedToScorePercentHTTotal")]
        public double HomeFailedToScorePercentHTTotal { get; set; }

        [JsonPropertyName("homeBothToScorePercentHTTotal")]
        public double HomeBothToScorePercentHTTotal { get; set; }

        [JsonPropertyName("homeCleanSheetsPercentHTTotal")]
        public double HomeCleanSheetsPercentHTTotal { get; set; }

        [JsonPropertyName("homeGoalsScoredHTTotal")]
        public int HomeGoalsScoredHTTotal { get; set; }

        [JsonPropertyName("homeGoalsConcededHTTotal")]
        public int HomeGoalsConcededHTTotal { get; set; }

        [JsonPropertyName("homeAverageGoalsScoredHTTotal")]
        public double HomeAverageGoalsScoredHTTotal { get; set; }

        [JsonPropertyName("homeAverageGoalsConcededHTTotal")]
        public double HomeAverageGoalsConcededHTTotal { get; set; }

        [JsonPropertyName("homeWinsPercentFTTotal")]
        public double HomeWinsPercentFTTotal { get; set; }

        [JsonPropertyName("homeDrawsPercentFTTotal")]
        public double HomeDrawsPercentFTTotal { get; set; }

        [JsonPropertyName("homeLossesPercentFTTotal")]
        public double HomeLossesPercentFTTotal { get; set; }

        [JsonPropertyName("homeFirstToScorePercentFTTotal")]
        public double HomeFirstToScorePercentFTTotal { get; set; }

        [JsonPropertyName("homeToScoreTwoZeroPercentFTTotal")]
        public double HomeToScoreTwoZeroPercentFTTotal { get; set; }

        [JsonPropertyName("homeFailedToScorePercentFTTotal")]
        public double HomeFailedToScorePercentFTTotal { get; set; }

        [JsonPropertyName("homeBothToScorePercentFTTotal")]
        public double HomeBothToScorePercentFTTotal { get; set; }

        [JsonPropertyName("homeCleanSheetsPercentFTTotal")]
        public double HomeCleanSheetsPercentFTTotal { get; set; }

        [JsonPropertyName("homeGoalsScoredFTTotal")]
        public int HomeGoalsScoredFTTotal { get; set; }

        [JsonPropertyName("homeGoalsConcededFTTotal")]
        public int HomeGoalsConcededFTTotal { get; set; }

        [JsonPropertyName("homeAverageGoalsScoredFTTotal")]
        public double HomeAverageGoalsScoredFTTotal { get; set; }

        [JsonPropertyName("homeAverageGoalsConcededFTTotal")]
        public double HomeAverageGoalsConcededFTTotal { get; set; }

        [JsonPropertyName("homeGoalsScoredAt0To15")]
        public int HomeGoalsScoredAt0To15 { get; set; }

        [JsonPropertyName("homeGoalsScoredAt0To15Percent")]
        public double HomeGoalsScoredAt0To15Percent { get; set; }

        [JsonPropertyName("homeGoalsScoredAt16To30")]
        public int HomeGoalsScoredAt16To30 { get; set; }

        [JsonPropertyName("homeGoalsScoredAt16To30Percent")]
        public double HomeGoalsScoredAt16To30Percent { get; set; }

        [JsonPropertyName("homeGoalsScoredAt31To45")]
        public int HomeGoalsScoredAt31To45 { get; set; }

        [JsonPropertyName("homeGoalsScoredAt31To45Percent")]
        public double HomeGoalsScoredAt31To45Percent { get; set; }

        [JsonPropertyName("homeGoalsScoredAt46To60")]
        public int HomeGoalsScoredAt46To60 { get; set; }

        [JsonPropertyName("homeGoalsScoredAt46To60Percent")]
        public double HomeGoalsScoredAt46To60Percent { get; set; }

        [JsonPropertyName("homeGoalsScoredAt61To75")]
        public int HomeGoalsScoredAt61To75 { get; set; }

        [JsonPropertyName("homeGoalsScoredAt61To75Percent")]
        public double HomeGoalsScoredAt61To75Percent { get; set; }

        [JsonPropertyName("homeGoalsScoredAt76To90")]
        public int HomeGoalsScoredAt76To90 { get; set; }

        [JsonPropertyName("homeGoalsScoredAt76To90Percent")]
        public double HomeGoalsScoredAt76To90Percent { get; set; }

        [JsonPropertyName("homeGoalsConcededAt0To15")]
        public int HomeGoalsConcededAt0To15 { get; set; }

        [JsonPropertyName("homeGoalsConcededAt0To15Percent")]
        public double HomeGoalsConcededAt0To15Percent { get; set; }

        [JsonPropertyName("homeGoalsConcededAt16To30")]
        public int HomeGoalsConcededAt16To30 { get; set; }

        [JsonPropertyName("homeGoalsConcededAt16To30Percent")]
        public double HomeGoalsConcededAt16To30Percent { get; set; }

        [JsonPropertyName("homeGoalsConcededAt31To45")]
        public int HomeGoalsConcededAt31To45 { get; set; }

        [JsonPropertyName("homeGoalsConcededAt31To45Percent")]
        public double HomeGoalsConcededAt31To45Percent { get; set; }

        [JsonPropertyName("homeGoalsConcededAt46To60")]
        public int HomeGoalsConcededAt46To60 { get; set; }

        [JsonPropertyName("homeGoalsConcededAt46To60Percent")]
        public double HomeGoalsConcededAt46To60Percent { get; set; }

        [JsonPropertyName("homeGoalsConcededAt61To75")]
        public int HomeGoalsConcededAt61To75 { get; set; }

        [JsonPropertyName("homeGoalsConcededAt61To75Percent")]
        public double HomeGoalsConcededAt61To75Percent { get; set; }

        [JsonPropertyName("homeGoalsConcededAt76To90")]
        public int HomeGoalsConcededAt76To90 { get; set; }

        [JsonPropertyName("homeGoalsConcededAt76To90Percent")]
        public double HomeGoalsConcededAt76To90Percent { get; set; }

        [JsonPropertyName("homePPGAtHome")]
        public double HomePPGAtHome { get; set; }

        [JsonPropertyName("homeWinsAtHome")]
        public int HomeWinsAtHome { get; set; }

        [JsonPropertyName("homeWinsPercentAtHome")]
        public double HomeWinsPercentAtHome { get; set; }

        [JsonPropertyName("homeFirstToScorePercentAtHome")]
        public double HomeFirstToScorePercentAtHome { get; set; }

        [JsonPropertyName("homeToScoreTwoZeroPercentAtHome")]
        public double HomeToScoreTwoZeroPercentAtHome { get; set; }

        [JsonPropertyName("homeCleanSheetsPercentAtHome")]
        public double HomeCleanSheetsPercentAtHome { get; set; }

        [JsonPropertyName("homeFailedToScorePercentAtHome")]
        public double HomeFailedToScorePercentAtHome { get; set; }

        [JsonPropertyName("homeBothToScorePercentAtHome")]
        public double HomeBothToScorePercentAtHome { get; set; }

        [JsonPropertyName("homeGoalsScoredAtHome")]
        public int HomeGoalsScoredAtHome { get; set; }

        [JsonPropertyName("homeGoalsConcededAtHome")]
        public int HomeGoalsConcededAtHome { get; set; }

        [JsonPropertyName("homeAverageGoalsScoredAtHome")]
        public double HomeAverageGoalsScoredAtHome { get; set; }

        [JsonPropertyName("homeAverageGoalsConcededAtHome")]
        public double HomeAverageGoalsConcededAtHome { get; set; }

        [JsonPropertyName("homeWinsPercentHTAtHome")]
        public double HomeWinsPercentHTAtHome { get; set; }

        [JsonPropertyName("homeDrawsPercentHTAtHome")]
        public double HomeDrawsPercentHTAtHome { get; set; }

        [JsonPropertyName("homeLossesPercentHTAtHome")]
        public double HomeLossesPercentHTAtHome { get; set; }

        [JsonPropertyName("homeFirstToScorePercentHTAtHome")]
        public double HomeFirstToScorePercentHTAtHome { get; set; }

        [JsonPropertyName("homeToScoreTwoZeroPercentHTAtHome")]
        public double HomeToScoreTwoZeroPercentHTAtHome { get; set; }

        [JsonPropertyName("homeFailedToScorePercentHTAtHome")]
        public double HomeFailedToScorePercentHTAtHome { get; set; }

        [JsonPropertyName("homeBothToScorePercentHTAtHome")]
        public double HomeBothToScorePercentHTAtHome { get; set; }

        [JsonPropertyName("homeCleanSheetsPercentHTAtHome")]
        public double HomeCleanSheetsPercentHTAtHome { get; set; }

        [JsonPropertyName("homeGoalsScoredHTAtHome")]
        public int HomeGoalsScoredHTAtHome { get; set; }

        [JsonPropertyName("homeGoalsConcededHTAtHome")]
        public int HomeGoalsConcededHTAtHome { get; set; }

        [JsonPropertyName("homeAverageGoalsScoredHTAtHome")]
        public double HomeAverageGoalsScoredHTAtHome { get; set; }

        [JsonPropertyName("homeAverageGoalsConcededHTAtHome")]
        public double HomeAverageGoalsConcededHTAtHome { get; set; }

        [JsonPropertyName("homeWinsPercentFTAtHome")]
        public double HomeWinsPercentFTAtHome { get; set; }

        [JsonPropertyName("homeDrawsPercentFTAtHome")]
        public double HomeDrawsPercentFTAtHome { get; set; }

        [JsonPropertyName("homeLossesPercentFTAtHome")]
        public double HomeLossesPercentFTAtHome { get; set; }

        [JsonPropertyName("homeFirstToScorePercentFTAtHome")]
        public double HomeFirstToScorePercentFTAtHome { get; set; }

        [JsonPropertyName("homeToScoreTwoZeroPercentFTAtHome")]
        public double HomeToScoreTwoZeroPercentFTAtHome { get; set; }

        [JsonPropertyName("homeFailedToScorePercentFTAtHome")]
        public double HomeFailedToScorePercentFTAtHome { get; set; }

        [JsonPropertyName("homeBothToScorePercentFTAtHome")]
        public double HomeBothToScorePercentFTAtHome { get; set; }

        [JsonPropertyName("homeCleanSheetsPercentFTAtHome")]
        public double HomeCleanSheetsPercentFTAtHome { get; set; }

        [JsonPropertyName("homeGoalsScoredFTAtHome")]
        public int HomeGoalsScoredFTAtHome { get; set; }

        [JsonPropertyName("homeGoalsConcededFTAtHome")]
        public int HomeGoalsConcededFTAtHome { get; set; }

        [JsonPropertyName("homeAverageGoalsScoredFTAtHome")]
        public double HomeAverageGoalsScoredFTAtHome { get; set; }

        [JsonPropertyName("homeAverageGoalsConcededFTAtHome")]
        public double HomeAverageGoalsConcededFTAtHome { get; set; }

        [JsonPropertyName("homeGoalsScoredAt0To15AtHome")]
        public int HomeGoalsScoredAt0To15AtHome { get; set; }

        [JsonPropertyName("homeGoalsScoredAt0To15PercentAtHome")]
        public double HomeGoalsScoredAt0To15PercentAtHome { get; set; }

        [JsonPropertyName("homeGoalsScoredAt16To30AtHome")]
        public int HomeGoalsScoredAt16To30AtHome { get; set; }

        [JsonPropertyName("homeGoalsScoredAt16To30PercentAtHome")]
        public double HomeGoalsScoredAt16To30PercentAtHome { get; set; }

        [JsonPropertyName("homeGoalsScoredAt31To45AtHome")]
        public int HomeGoalsScoredAt31To45AtHome { get; set; }

        [JsonPropertyName("homeGoalsScoredAt31To45PercentAtHome")]
        public double HomeGoalsScoredAt31To45PercentAtHome { get; set; }

        [JsonPropertyName("homeGoalsScoredAt46To60AtHome")]
        public int HomeGoalsScoredAt46To60AtHome { get; set; }

        [JsonPropertyName("homeGoalsScoredAt46To60PercentAtHome")]
        public double HomeGoalsScoredAt46To60PercentAtHome { get; set; }

        [JsonPropertyName("homeGoalsScoredAt61To75AtHome")]
        public int HomeGoalsScoredAt61To75AtHome { get; set; }

        [JsonPropertyName("homeGoalsScoredAt61To75PercentAtHome")]
        public double HomeGoalsScoredAt61To75PercentAtHome { get; set; }

        [JsonPropertyName("homeGoalsScoredAt76To90AtHome")]
        public int HomeGoalsScoredAt76To90AtHome { get; set; }

        [JsonPropertyName("homeGoalsScoredAt76To90PercentAtHome")]
        public double HomeGoalsScoredAt76To90PercentAtHome { get; set; }

        [JsonPropertyName("homeGoalsConcededAt0To15AtHome")]
        public int HomeGoalsConcededAt0To15AtHome { get; set; }

        [JsonPropertyName("homeGoalsConcededAt0To15PercentAtHome")]
        public double HomeGoalsConcededAt0To15PercentAtHome { get; set; }

        [JsonPropertyName("homeGoalsConcededAt16To30AtHome")]
        public int HomeGoalsConcededAt16To30AtHome { get; set; }

        [JsonPropertyName("homeGoalsConcededAt16To30PercentAtHome")]
        public double HomeGoalsConcededAt16To30PercentAtHome { get; set; }

        [JsonPropertyName("homeGoalsConcededAt31To45AtHome")]
        public int HomeGoalsConcededAt31To45AtHome { get; set; }

        [JsonPropertyName("homeGoalsConcededAt31To45PercentAtHome")]
        public double HomeGoalsConcededAt31To45PercentAtHome { get; set; }

        [JsonPropertyName("homeGoalsConcededAt46To60AtHome")]
        public int HomeGoalsConcededAt46To60AtHome { get; set; }

        [JsonPropertyName("homeGoalsConcededAt46To60PercentAtHome")]
        public double HomeGoalsConcededAt46To60PercentAtHome { get; set; }

        [JsonPropertyName("homeGoalsConcededAt61To75AtHome")]
        public int HomeGoalsConcededAt61To75AtHome { get; set; }

        [JsonPropertyName("homeGoalsConcededAt61To75PercentAtHome")]
        public double HomeGoalsConcededAt61To75PercentAtHome { get; set; }

        [JsonPropertyName("homeGoalsConcededAt76To90AtHome")]
        public int HomeGoalsConcededAt76To90AtHome { get; set; }

        [JsonPropertyName("homeGoalsConcededAt76To90PercentAtHome")]
        public double HomeGoalsConcededAt76To90PercentAtHome { get; set; }

        [JsonPropertyName("awayPPGTotal")]
        public double AwayPPGTotal { get; set; }

        [JsonPropertyName("awayWinsTotal")]
        public int AwayWinsTotal { get; set; }

        [JsonPropertyName("awayWinsPercentTotal")]
        public double AwayWinsPercentTotal { get; set; }

        [JsonPropertyName("awayFirstToScorePercentTotal")]
        public double AwayFirstToScorePercentTotal { get; set; }

        [JsonPropertyName("awayToScoreTwoZeroPercentTotal")]
        public double AwayToScoreTwoZeroPercentTotal { get; set; }

        [JsonPropertyName("awayCleanSheetsPercentTotal")]
        public double AwayCleanSheetsPercentTotal { get; set; }

        [JsonPropertyName("awayFailedToScorePercentTotal")]
        public double AwayFailedToScorePercentTotal { get; set; }

        [JsonPropertyName("awayBothToScorePercentTotal")]
        public double AwayBothToScorePercentTotal { get; set; }

        [JsonPropertyName("awayGoalsScoredTotal")]
        public int AwayGoalsScoredTotal { get; set; }

        [JsonPropertyName("awayGoalsConcededTotal")]
        public int AwayGoalsConcededTotal { get; set; }

        [JsonPropertyName("awayAverageGoalsScoredTotal")]
        public double AwayAverageGoalsScoredTotal { get; set; }

        [JsonPropertyName("awayAverageGoalsConcededTotal")]
        public double AwayAverageGoalsConcededTotal { get; set; }

        [JsonPropertyName("awayWinsPercentHTTotal")]
        public double AwayWinsPercentHTTotal { get; set; }

        [JsonPropertyName("awayDrawsPercentHTTotal")]
        public double AwayDrawsPercentHTTotal { get; set; }

        [JsonPropertyName("awayLossesPercentHTTotal")]
        public double AwayLossesPercentHTTotal { get; set; }

        [JsonPropertyName("awayFirstToScorePercentHTTotal")]
        public double AwayFirstToScorePercentHTTotal { get; set; }

        [JsonPropertyName("awayToScoreTwoZeroPercentHTTotal")]
        public double AwayToScoreTwoZeroPercentHTTotal { get; set; }

        [JsonPropertyName("awayFailedToScorePercentHTTotal")]
        public double AwayFailedToScorePercentHTTotal { get; set; }

        [JsonPropertyName("awayBothToScorePercentHTTotal")]
        public double AwayBothToScorePercentHTTotal { get; set; }

        [JsonPropertyName("awayCleanSheetsPercentHTTotal")]
        public double AwayCleanSheetsPercentHTTotal { get; set; }

        [JsonPropertyName("awayGoalsScoredHTTotal")]
        public int AwayGoalsScoredHTTotal { get; set; }

        [JsonPropertyName("awayGoalsConcededHTTotal")]
        public int AwayGoalsConcededHTTotal { get; set; }

        [JsonPropertyName("awayAverageGoalsScoredHTTotal")]
        public double AwayAverageGoalsScoredHTTotal { get; set; }

        [JsonPropertyName("awayAverageGoalsConcededHTTotal")]
        public double AwayAverageGoalsConcededHTTotal { get; set; }

        [JsonPropertyName("awayWinsPercentFTTotal")]
        public double AwayWinsPercentFTTotal { get; set; }

        [JsonPropertyName("awayDrawsPercentFTTotal")]
        public double AwayDrawsPercentFTTotal { get; set; }

        [JsonPropertyName("awayLossesPercentFTTotal")]
        public double AwayLossesPercentFTTotal { get; set; }

        [JsonPropertyName("awayFirstToScorePercentFTTotal")]
        public double AwayFirstToScorePercentFTTotal { get; set; }

        [JsonPropertyName("awayToScoreTwoZeroPercentFTTotal")]
        public double AwayToScoreTwoZeroPercentFTTotal { get; set; }

        [JsonPropertyName("awayFailedToScorePercentFTTotal")]
        public double AwayFailedToScorePercentFTTotal { get; set; }

        [JsonPropertyName("awayBothToScorePercentFTTotal")]
        public double AwayBothToScorePercentFTTotal { get; set; }

        [JsonPropertyName("awayCleanSheetsPercentFTTotal")]
        public double AwayCleanSheetsPercentFTTotal { get; set; }

        [JsonPropertyName("awayGoalsScoredFTTotal")]
        public int AwayGoalsScoredFTTotal { get; set; }

        [JsonPropertyName("awayGoalsConcededFTTotal")]
        public int AwayGoalsConcededFTTotal { get; set; }

        [JsonPropertyName("awayAverageGoalsScoredFTTotal")]
        public double AwayAverageGoalsScoredFTTotal { get; set; }

        [JsonPropertyName("awayAverageGoalsConcededFTTotal")]
        public double AwayAverageGoalsConcededFTTotal { get; set; }

        [JsonPropertyName("awayGoalsScoredAt0To15")]
        public int AwayGoalsScoredAt0To15 { get; set; }

        [JsonPropertyName("awayGoalsScoredAt0To15Percent")]
        public double AwayGoalsScoredAt0To15Percent { get; set; }

        [JsonPropertyName("awayGoalsScoredAt16To30")]
        public int AwayGoalsScoredAt16To30 { get; set; }

        [JsonPropertyName("awayGoalsScoredAt16To30Percent")]
        public double AwayGoalsScoredAt16To30Percent { get; set; }

        [JsonPropertyName("awayGoalsScoredAt31To45")]
        public int AwayGoalsScoredAt31To45 { get; set; }

        [JsonPropertyName("awayGoalsScoredAt31To45Percent")]
        public double AwayGoalsScoredAt31To45Percent { get; set; }

        [JsonPropertyName("awayGoalsScoredAt46To60")]
        public int AwayGoalsScoredAt46To60 { get; set; }

        [JsonPropertyName("awayGoalsScoredAt46To60Percent")]
        public double AwayGoalsScoredAt46To60Percent { get; set; }

        [JsonPropertyName("awayGoalsScoredAt61To75")]
        public int AwayGoalsScoredAt61To75 { get; set; }

        [JsonPropertyName("awayGoalsScoredAt61To75Percent")]
        public double AwayGoalsScoredAt61To75Percent { get; set; }

        [JsonPropertyName("awayGoalsScoredAt76To90")]
        public int AwayGoalsScoredAt76To90 { get; set; }

        [JsonPropertyName("awayGoalsScoredAt76To90Percent")]
        public double AwayGoalsScoredAt76To90Percent { get; set; }

        [JsonPropertyName("awayGoalsConcededAt0To15")]
        public int AwayGoalsConcededAt0To15 { get; set; }

        [JsonPropertyName("awayGoalsConcededAt0To15Percent")]
        public double AwayGoalsConcededAt0To15Percent { get; set; }

        [JsonPropertyName("awayGoalsConcededAt16To30")]
        public int AwayGoalsConcededAt16To30 { get; set; }

        [JsonPropertyName("awayGoalsConcededAt16To30Percent")]
        public double AwayGoalsConcededAt16To30Percent { get; set; }

        [JsonPropertyName("awayGoalsConcededAt31To45")]
        public int AwayGoalsConcededAt31To45 { get; set; }

        [JsonPropertyName("awayGoalsConcededAt31To45Percent")]
        public double AwayGoalsConcededAt31To45Percent { get; set; }

        [JsonPropertyName("awayGoalsConcededAt46To60")]
        public int AwayGoalsConcededAt46To60 { get; set; }

        [JsonPropertyName("awayGoalsConcededAt46To60Percent")]
        public double AwayGoalsConcededAt46To60Percent { get; set; }

        [JsonPropertyName("awayGoalsConcededAt61To75")]
        public int AwayGoalsConcededAt61To75 { get; set; }

        [JsonPropertyName("awayGoalsConcededAt61To75Percent")]
        public double AwayGoalsConcededAt61To75Percent { get; set; }

        [JsonPropertyName("awayGoalsConcededAt76To90")]
        public int AwayGoalsConcededAt76To90 { get; set; }

        [JsonPropertyName("awayGoalsConcededAt76To90Percent")]
        public double AwayGoalsConcededAt76To90Percent { get; set; }

        [JsonPropertyName("awayPPGAtAway")]
        public double AwayPPGAtAway { get; set; }

        [JsonPropertyName("awayWinsAtAway")]
        public int AwayWinsAtAway { get; set; }

        [JsonPropertyName("awayWinsPercentAtAway")]
        public double AwayWinsPercentAtAway { get; set; }

        [JsonPropertyName("awayFirstToScorePercentAtAway")]
        public double AwayFirstToScorePercentAtAway { get; set; }

        [JsonPropertyName("awayToScoreTwoZeroPercentAtAway")]
        public double AwayToScoreTwoZeroPercentAtAway { get; set; }

        [JsonPropertyName("awayCleanSheetsPercentAtAway")]
        public double AwayCleanSheetsPercentAtAway { get; set; }

        [JsonPropertyName("awayFailedToScorePercentAtAway")]
        public double AwayFailedToScorePercentAtAway { get; set; }

        [JsonPropertyName("awayBothToScorePercentAtAway")]
        public double AwayBothToScorePercentAtAway { get; set; }

        [JsonPropertyName("awayGoalsScoredAtAway")]
        public int AwayGoalsScoredAtAway { get; set; }

        [JsonPropertyName("awayGoalsConcededAtAway")]
        public int AwayGoalsConcededAtAway { get; set; }

        [JsonPropertyName("awayAverageGoalsScoredAtAway")]
        public double AwayAverageGoalsScoredAtAway { get; set; }

        [JsonPropertyName("awayAverageGoalsConcededAtAway")]
        public double AwayAverageGoalsConcededAtAway { get; set; }

        [JsonPropertyName("awayWinsPercentHTAtAway")]
        public double AwayWinsPercentHTAtAway { get; set; }

        [JsonPropertyName("awayDrawsPercentHTAtAway")]
        public double AwayDrawsPercentHTAtAway { get; set; }

        [JsonPropertyName("awayLossesPercentHTAtAway")]
        public double AwayLossesPercentHTAtAway { get; set; }

        [JsonPropertyName("awayFirstToScorePercentHTAtAway")]
        public double AwayFirstToScorePercentHTAtAway { get; set; }

        [JsonPropertyName("awayToScoreTwoZeroPercentHTAtAway")]
        public double AwayToScoreTwoZeroPercentHTAtAway { get; set; }

        [JsonPropertyName("awayFailedToScorePercentHTAtAway")]
        public double AwayFailedToScorePercentHTAtAway { get; set; }

        [JsonPropertyName("awayBothToScorePercentHTAtAway")]
        public double AwayBothToScorePercentHTAtAway { get; set; }

        [JsonPropertyName("awayCleanSheetsPercentHTAtAway")]
        public double AwayCleanSheetsPercentHTAtAway { get; set; }

        [JsonPropertyName("awayGoalsScoredHTAtAway")]
        public int AwayGoalsScoredHTAtAway { get; set; }

        [JsonPropertyName("awayGoalsConcededHTAtAway")]
        public int AwayGoalsConcededHTAtAway { get; set; }

        [JsonPropertyName("awayAverageGoalsScoredHTAtAway")]
        public double AwayAverageGoalsScoredHTAtAway { get; set; }

        [JsonPropertyName("awayAverageGoalsConcededHTAtAway")]
        public double AwayAverageGoalsConcededHTAtAway { get; set; }

        [JsonPropertyName("awayWinsPercentFTAtAway")]
        public double AwayWinsPercentFTAtAway { get; set; }

        [JsonPropertyName("awayDrawsPercentFTAtAway")]
        public double AwayDrawsPercentFTAtAway { get; set; }

        [JsonPropertyName("awayLossesPercentFTAtAway")]
        public double AwayLossesPercentFTAtAway { get; set; }

        [JsonPropertyName("awayFirstToScorePercentFTAtAway")]
        public double AwayFirstToScorePercentFTAtAway { get; set; }

        [JsonPropertyName("awayToScoreTwoZeroPercentFTAtAway")]
        public double AwayToScoreTwoZeroPercentFTAtAway { get; set; }

        [JsonPropertyName("awayFailedToScorePercentFTAtAway")]
        public double AwayFailedToScorePercentFTAtAway { get; set; }

        [JsonPropertyName("awayBothToScorePercentFTAtAway")]
        public double AwayBothToScorePercentFTAtAway { get; set; }

        [JsonPropertyName("awayCleanSheetsPercentFTAtAway")]
        public double AwayCleanSheetsPercentFTAtAway { get; set; }

        [JsonPropertyName("awayGoalsScoredFTAtAway")]
        public int AwayGoalsScoredFTAtAway { get; set; }

        [JsonPropertyName("awayGoalsConcededFTAtAway")]
        public int AwayGoalsConcededFTAtAway { get; set; }

        [JsonPropertyName("awayAverageGoalsScoredFTAtAway")]
        public double AwayAverageGoalsScoredFTAtAway { get; set; }

        [JsonPropertyName("awayAverageGoalsConcededFTAtAway")]
        public double AwayAverageGoalsConcededFTAtAway { get; set; }

        [JsonPropertyName("awayGoalsScoredAt0To15AtAway")]
        public int AwayGoalsScoredAt0To15AtAway { get; set; }

        [JsonPropertyName("awayGoalsScoredAt0To15PercentAtAway")]
        public double AwayGoalsScoredAt0To15PercentAtAway { get; set; }

        [JsonPropertyName("awayGoalsScoredAt16To30AtAway")]
        public int AwayGoalsScoredAt16To30AtAway { get; set; }

        [JsonPropertyName("awayGoalsScoredAt16To30PercentAtAway")]
        public double AwayGoalsScoredAt16To30PercentAtAway { get; set; }

        [JsonPropertyName("awayGoalsScoredAt31To45AtAway")]
        public int AwayGoalsScoredAt31To45AtAway { get; set; }

        [JsonPropertyName("awayGoalsScoredAt31To45PercentAtAway")]
        public double AwayGoalsScoredAt31To45PercentAtAway { get; set; }

        [JsonPropertyName("awayGoalsScoredAt46To60AtAway")]
        public int AwayGoalsScoredAt46To60AtAway { get; set; }

        [JsonPropertyName("awayGoalsScoredAt46To60PercentAtAway")]
        public double AwayGoalsScoredAt46To60PercentAtAway { get; set; }

        [JsonPropertyName("awayGoalsScoredAt61To75AtAway")]
        public int AwayGoalsScoredAt61To75AtAway { get; set; }

        [JsonPropertyName("awayGoalsScoredAt61To75PercentAtAway")]
        public double AwayGoalsScoredAt61To75PercentAtAway { get; set; }

        [JsonPropertyName("awayGoalsScoredAt76To90AtAway")]
        public int AwayGoalsScoredAt76To90AtAway { get; set; }

        [JsonPropertyName("awayGoalsScoredAt76To90PercentAtAway")]
        public double AwayGoalsScoredAt76To90PercentAtAway { get; set; }

        [JsonPropertyName("awayGoalsConcededAt0To15AtAway")]
        public int AwayGoalsConcededAt0To15AtAway { get; set; }

        [JsonPropertyName("awayGoalsConcededAt0To15PercentAtAway")]
        public double AwayGoalsConcededAt0To15PercentAtAway { get; set; }

        [JsonPropertyName("awayGoalsConcededAt16To30AtAway")]
        public int AwayGoalsConcededAt16To30AtAway { get; set; }

        [JsonPropertyName("awayGoalsConcededAt16To30PercentAtAway")]
        public double AwayGoalsConcededAt16To30PercentAtAway { get; set; }

        [JsonPropertyName("awayGoalsConcededAt31To45AtAway")]
        public int AwayGoalsConcededAt31To45AtAway { get; set; }

        [JsonPropertyName("awayGoalsConcededAt31To45PercentAtAway")]
        public double AwayGoalsConcededAt31To45PercentAtAway { get; set; }

        [JsonPropertyName("awayGoalsConcededAt46To60AtAway")]
        public int AwayGoalsConcededAt46To60AtAway { get; set; }

        [JsonPropertyName("awayGoalsConcededAt46To60PercentAtAway")]
        public double AwayGoalsConcededAt46To60PercentAtAway { get; set; }

        [JsonPropertyName("awayGoalsConcededAt61To75AtAway")]
        public int AwayGoalsConcededAt61To75AtAway { get; set; }

        [JsonPropertyName("awayGoalsConcededAt61To75PercentAtAway")]
        public double AwayGoalsConcededAt61To75PercentAtAway { get; set; }

        [JsonPropertyName("awayGoalsConcededAt76To90AtAway")]
        public int AwayGoalsConcededAt76To90AtAway { get; set; }

        [JsonPropertyName("awayGoalsConcededAt76To90PercentAtAway")]
        public double AwayGoalsConcededAt76To90PercentAtAway { get; set; }
    }
}
