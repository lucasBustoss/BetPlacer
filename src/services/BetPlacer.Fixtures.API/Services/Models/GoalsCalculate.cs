namespace BetPlacer.Fixtures.API.Services.Models
{
    public class GoalsCalculate
    {
        public GoalsCalculate(double ftsPercent, double cleanSheetsPercent, double failedToScorePercent, double bothToScorePercent, int goalsScored, int goalsConceded, double averageGoalsScored, double averageGoalsConceded)
        {
            FTSPercent = ftsPercent;
            CleanSheetsPercent = cleanSheetsPercent;
            FailedToScorePercent = failedToScorePercent;
            BothToScorePercent = bothToScorePercent;
            GoalsScored = goalsScored;
            GoalsConceded = goalsConceded;
            AverageGoalsScored = averageGoalsScored;
            AverageGoalsConceded = averageGoalsConceded;
        }

        public double FTSPercent { get; set; }
        public double CleanSheetsPercent { get; set; }
        public double FailedToScorePercent { get; set; }
        public double BothToScorePercent { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public double AverageGoalsScored { get; set; }
        public double AverageGoalsConceded { get; set; }
    }
}
