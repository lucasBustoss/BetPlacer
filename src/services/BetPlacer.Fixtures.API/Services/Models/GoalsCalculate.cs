namespace BetPlacer.Fixtures.API.Services.Models
{
    public class GoalsCalculate
    {
        public GoalsCalculate(
            double ftsPercent, 
            double twoZeroPercent, 
            double cleanSheetsPercent, 
            double failedToScorePercent, 
            double bothToScorePercent, 
            int goalsScored, 
            int goalsConceded, 
            double averageGoalsScored, 
            double averageGoalsConceded, 
            int goalsScoredIn0To15Min,
            double goalsScoredIn0To15MinPercent,
            int goalsScoredIn16To30Min,
            double goalsScoredIn16To30MinPercent,
            int goalsScoredIn31To45Min,
            double goalsScoredIn31To45MinPercent,
            int goalsScoredIn46To60Min,
            double goalsScoredIn46To60MinPercent,
            int goalsScoredIn61To75Min,
            double goalsScoredIn61To75MinPercent,
            int goalsScoredIn76To90Min,
            double goalsScoredIn76To90MinPercent,
            int goalsConcededIn0To15Min,
            double goalsConcededIn0To15MinPercent,
            int goalsConcededIn16To30Min,
            double goalsConcededIn16To30MinPercent,
            int goalsConcededIn31To45Min,
            double goalsConcededIn31To45MinPercent,
            int goalsConcededIn46To60Min,
            double goalsConcededIn46To60MinPercent,
            int goalsConcededIn61To75Min,
            double goalsConcededIn61To75MinPercent,
            int goalsConcededIn76To90Min,
            double goalsConcededIn76To90MinPercent)
        {
            FTSPercent = ftsPercent;
            TwoZeroPercent = twoZeroPercent;
            CleanSheetsPercent = cleanSheetsPercent;
            FailedToScorePercent = failedToScorePercent;
            BothToScorePercent = bothToScorePercent;
            GoalsScored = goalsScored;
            GoalsConceded = goalsConceded;
            AverageGoalsScored = averageGoalsScored;
            AverageGoalsConceded = averageGoalsConceded;

            GoalsScoredIn0To15Min = goalsScoredIn0To15Min;
            GoalsScoredIn0To15MinPercent = goalsScoredIn0To15MinPercent;
            GoalsScoredIn16To30Min = goalsScoredIn16To30Min;
            GoalsScoredIn16To30MinPercent = goalsScoredIn16To30MinPercent;
            GoalsScoredIn31To45Min = goalsScoredIn31To45Min;
            GoalsScoredIn31To45MinPercent = goalsScoredIn31To45MinPercent;
            GoalsScoredIn46To60Min = goalsScoredIn46To60Min;
            GoalsScoredIn46To60MinPercent = goalsScoredIn46To60MinPercent;
            GoalsScoredIn61To75Min = goalsScoredIn61To75Min;
            GoalsScoredIn61To75MinPercent = goalsScoredIn61To75MinPercent;
            GoalsScoredIn76To90Min = goalsScoredIn76To90Min;
            GoalsScoredIn76To90MinPercent = goalsScoredIn76To90MinPercent;

            GoalsConcededIn0To15Min = goalsConcededIn0To15Min;
            GoalsConcededIn0To15MinPercent = goalsConcededIn0To15MinPercent;
            GoalsConcededIn16To30Min = goalsConcededIn16To30Min;
            GoalsConcededIn16To30MinPercent = goalsConcededIn16To30MinPercent;
            GoalsConcededIn31To45Min = goalsConcededIn31To45Min;
            GoalsConcededIn31To45MinPercent = goalsConcededIn31To45MinPercent;
            GoalsConcededIn46To60Min = goalsConcededIn46To60Min;
            GoalsConcededIn46To60MinPercent = goalsConcededIn46To60MinPercent;
            GoalsConcededIn61To75Min = goalsConcededIn61To75Min;
            GoalsConcededIn61To75MinPercent = goalsConcededIn61To75MinPercent;
            GoalsConcededIn76To90Min = goalsConcededIn76To90Min;
            GoalsConcededIn76To90MinPercent = goalsConcededIn76To90MinPercent;
        }

        public double FTSPercent { get; set; }
        public double TwoZeroPercent { get; set; }
        public double CleanSheetsPercent { get; set; }
        public double FailedToScorePercent { get; set; }
        public double BothToScorePercent { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public double AverageGoalsScored { get; set; }
        public double AverageGoalsConceded { get; set; }

        public int GoalsScoredIn0To15Min { get; set; }
        public double GoalsScoredIn0To15MinPercent { get; set; }
        public int GoalsConcededIn0To15Min { get; set; }
        public double GoalsConcededIn0To15MinPercent { get; set; }

        public int GoalsScoredIn16To30Min { get; set; }
        public double GoalsScoredIn16To30MinPercent { get; set; }
        public int GoalsConcededIn16To30Min { get; set; }
        public double GoalsConcededIn16To30MinPercent { get; set; }

        public int GoalsScoredIn31To45Min { get; set; }
        public double GoalsScoredIn31To45MinPercent { get; set; }
        public int GoalsConcededIn31To45Min { get; set; }
        public double GoalsConcededIn31To45MinPercent { get; set; }

        public int GoalsScoredIn46To60Min { get; set; }
        public double GoalsScoredIn46To60MinPercent { get; set; }
        public int GoalsConcededIn46To60Min { get; set; }
        public double GoalsConcededIn46To60MinPercent { get; set; }

        public int GoalsScoredIn61To75Min { get; set; }
        public double GoalsScoredIn61To75MinPercent { get; set; }
        public int GoalsConcededIn61To75Min { get; set; }
        public double GoalsConcededIn61To75MinPercent { get; set; }

        public int GoalsScoredIn76To90Min { get; set; }
        public double GoalsScoredIn76To90MinPercent { get; set; }
        public int GoalsConcededIn76To90Min { get; set; }
        public double GoalsConcededIn76To90MinPercent { get; set; }
    }
}
