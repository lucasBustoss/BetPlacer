namespace BetPlacer.Punter.API.Models.Strategy
{
    public class StrategyClassification
    {
        public StrategyClassification(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public int TotalMatches { get; set; }
        public double ProfitLoss { get; set; }
        public double HistoricalCoefficientVariation { get; set; }
        public double AverageOdd { get; set; }
        public double WinRate { get; set; }
        public int TotalGreens { get; set; }
        public int TotalReds { get; set; }
    }
}
