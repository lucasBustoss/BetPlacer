namespace BetPlacer.Punter.API.Models.ValueObjects.Strategy
{
    public class Strategy
    {
        public Strategy(string name)
        {
            Name = name;
            StrategyClassifications = new List<StrategyClassification>();
        }

        public string Name { get; set; }
        public List<StrategyClassification> StrategyClassifications { get; set; }
    }
}
