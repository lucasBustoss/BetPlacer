namespace BetPlacer.Fixtures.API.Services.Models
{
    public class HTFTCalculate
    {
        public HTFTCalculate(
            double winsPercent,
            double drawsPercent,
            double lossesPercent,
            double failedToScorePercent,
            double cleanSheetsPercent,
            int goalsScored,
            int goalsConceded,
            double averageGoalsScored,
            double averageGoalsConceded)
        {
            WinsPercent = winsPercent;
            DrawsPercent = drawsPercent;
            LossesPercent = lossesPercent;
            FailedToScorePercent = failedToScorePercent;
            CleanSheetsPercent = cleanSheetsPercent;
            GoalsScored = goalsScored;
            GoalsConceded = goalsConceded;
            AverageGoalsScored = averageGoalsScored;
            AverageGoalsConceded = averageGoalsConceded;
        }

        public double WinsPercent { get; set; }
        public double DrawsPercent { get; set; }
        public double LossesPercent { get; set; }
        public double FailedToScorePercent { get; set; }
        public double CleanSheetsPercent { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public double AverageGoalsScored { get; set; }
        public double AverageGoalsConceded { get; set; }
    }
}
