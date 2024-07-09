namespace BetPlacer.Punter.API.Models.ValueObjects.Match.Team
{
    /// <summary>
    ///     Classe que calcula a média ponderada, desvio padrão e coeficiente de variação das variáveis de cada time de MatchBaseData
    /// </summary>

    public class TeamAverageData
    {
        public TeamAverageData(string teamName)
        {
            TeamName = teamName;
        }

        public string TeamName { get; set; }

        public double? HomeGoalScoredAvg { get; set; }
        public double? HomeGoalScoredStd { get; set; }
        public double? HomeGoalScoredCv { get; set; }
        public double? HomeGoalScoredValueAvg { get; set; }
        public double? HomeGoalScoredCostAvg { get; set; }

        public double? HomeGoalConcededAvg { get; set; }
        public double? HomeGoalConcededStd { get; set; }
        public double? HomeGoalConcededCv { get; set; }
        public double? HomeGoalConcededValueAvg { get; set; }
        public double? HomeGoalConcededCostAvg { get; set; }

        public double? HomePointsAvg { get; set; }
        public double? HomePointsStd { get; set; }
        public double? HomePointsCv { get; set; }

        public double? HomeGoalsDifferenceAvg { get; set; }
        public double? HomeGoalsDifferenceStd { get; set; }
        public double? HomeGoalsDifferenceCv { get; set; }

        public double? HomeOddsAvg { get; set; }
        public double? HomeOddsStd { get; set; }
        public double? HomeOddsCv { get; set; }

        public double? HomeRPSMO { get; set; }
        public double? HomeRPSGoals { get; set; }
        public double? HomeRPSBTTS { get; set; }

        public double? AwayGoalScoredAvg { get; set; }
        public double? AwayGoalScoredStd { get; set; }
        public double? AwayGoalScoredCv { get; set; }
        public double? AwayGoalScoredValueAvg { get; set; }
        public double? AwayGoalScoredCostAvg { get; set; }

        public double? AwayGoalConcededAvg { get; set; }
        public double? AwayGoalConcededStd { get; set; }
        public double? AwayGoalConcededCv { get; set; }
        public double? AwayGoalConcededValueAvg { get; set; }
        public double? AwayGoalConcededCostAvg { get; set; }

        public double? AwayPointsAvg { get; set; }
        public double? AwayPointsStd { get; set; }
        public double? AwayPointsCv { get; set; }

        public double? AwayGoalsDifferenceAvg { get; set; }
        public double? AwayGoalsDifferenceStd { get; set; }
        public double? AwayGoalsDifferenceCv { get; set; }

        public double? AwayOddsAvg { get; set; }
        public double? AwayOddsStd { get; set; }
        public double? AwayOddsCv { get; set; }

        public double? AwayRPSMO { get; set; }
        public double? AwayRPSGoals { get; set; }
        public double? AwayRPSBTTS { get; set; }
    }
}
