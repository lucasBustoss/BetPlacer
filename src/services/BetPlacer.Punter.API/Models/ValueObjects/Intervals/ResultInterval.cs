namespace BetPlacer.Punter.API.Models.ValueObjects.Intervals
{
    public class ResultInterval
    {
        public ResultInterval(string name, double percentMatches, double result, double coefficientVariation, double inferiorLimit, bool active, int code)
        {
            Name = name;
            PercentMatches = percentMatches;
            Result = result;
            CoefficientVariation = coefficientVariation;
            InferiorLimit = inferiorLimit;
            Active = active;
            Code = code;
        }

        public int Code { get; set; }
        public string Name { get; set; }
        public double PercentMatches { get; set; }
        public double Result { get; set; }
        public double CoefficientVariation { get; set; }
        public double InferiorLimit { get; set; }
        public bool Active { get; set; }
    }
}
