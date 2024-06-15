namespace BetPlacer.Punter.API.Models.Match.Team
{
    /// <summary>
    ///     Classe que calcula a média ponderada, desvio padrão e coeficiente de variação das variáveis de cada time de MatchBaseData
    /// </summary>

    public class TeamAverageData
    {
        public TeamAverageData(string teamName, string season)
        {
            TeamName = teamName;
            Season = season;
        }

        public string TeamName { get; set; }
        public string Season { get; set; }

        public double HomeGoalScoredAvg { get; set; }
        public double HomeGoalScoredStd { get; set; }
        public double HomeGoalScoredCv { get; set; }
        public double HomeGoalScoredValueAvg { get; set; }
        public double HomeGoalScoredCostAvg { get; set; }

        public double HomeGoalConcededAvg { get; set; }
        public double HomeGoalConcededStd { get; set; }
        public double HomeGoalConcededCv { get; set; }
        public double HomeGoalConcededValueAvg { get; set; }
        public double HomeGoalConcededCostAvg { get; set; }

        public double HomePointsAverage { get; set; }
        public double HomePointsStd { get; set; }
        public double HomePointsCv { get; set; }

        public double AwayGoalScoredAvg { get; set; }
        public double AwayGoalScoredStd { get; set; }
        public double AwayGoalScoredCv { get; set; }
        public double AwayGoalScoredValueAvg { get; set; }
        public double AwayGoalScoredCostAvg { get; set; }

        public double AwayGoalConcededAvg { get; set; }
        public double AwayGoalConcededStd { get; set; }
        public double AwayGoalConcededCv { get; set; }
        public double AwayGoalConcededValueAvg { get; set; }
        public double AwayGoalConcededCostAvg { get; set; }

        public double AwayPointsAverage { get; set; }
        public double AwayPointsStd { get; set; }
        public double AwayPointsCv { get; set; }
    }
}
