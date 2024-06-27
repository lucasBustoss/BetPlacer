using BetPlacer.Punter.API.Models.Match;

namespace BetPlacer.Punter.API.Models.Strategy
{
    public class StrategyInfo
    {
        public StrategyInfo(string name, List<string> classifications, List<MatchAnalyzed> matches, double resultAfterClassification)
        {
            Name = name;
            Classifications = classifications;
            Matches = matches;
            ResultAfterClassification = resultAfterClassification;
        }

        public string Name { get; set; }
        public List<string> Classifications { get; set; }
        public List<MatchAnalyzed> Matches { get; set; }
        public double ResultAfterClassification { get; set; }
    }
}
