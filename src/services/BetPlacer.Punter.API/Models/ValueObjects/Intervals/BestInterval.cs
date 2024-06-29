namespace BetPlacer.Punter.API.Models.ValueObjects.Intervals
{
    public class BestInterval
    {
        public BestInterval(string propertyName, double initialInterval, double finalInterval, double coefficientVariation, double inferiorLimit)
        {
            PropertyName = propertyName;
            InitialInterval = initialInterval;
            FinalInterval = finalInterval;
            CoefficientVariation = coefficientVariation;
            InferiorLimit = inferiorLimit;
        }

        public string PropertyName { get; set; }
        public double InitialInterval { get; set; }
        public double FinalInterval { get; set; }
        public double CoefficientVariation { get; set; }
        public double InferiorLimit { get; set; }
    }
}
