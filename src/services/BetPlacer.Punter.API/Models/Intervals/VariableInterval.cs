namespace BetPlacer.Punter.API.Models.Intervals
{
    public class VariableInterval
    {
        public VariableInterval(string interval, double result, double accumulateResult, int matchesCount)
        {
            Interval = interval;
            Result = result;
            AccumulateResult = accumulateResult;
            MatchesCount = matchesCount;
        }

        public string Interval { get; set; }
        public double Result { get; set; }
        public double AccumulateResult { get; set; }
        public int MatchesCount { get; set; }
    }
}
