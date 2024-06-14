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
        public double AwayGoalScoredAvg { get; set; }
        public double AwayGoalScoredStd { get; set; }
        public double AwayGoalScoredCv { get; set; }
    }
}
