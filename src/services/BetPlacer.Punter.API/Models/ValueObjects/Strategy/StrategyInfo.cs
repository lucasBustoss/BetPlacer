using BetPlacer.Punter.API.Models.ValueObjects.Intervals;
using BetPlacer.Punter.API.Models.ValueObjects.Match;

namespace BetPlacer.Punter.API.Models.ValueObjects.Strategy
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

        public StrategyInfo(int code, string name, List<string> classifications, double resultAfterClassification, List<BestInterval> bestIntervals, List<ResultInterval> resultIntervals)
        {
            Code = code;
            Name = name;
            Classifications = classifications;
            ResultAfterClassification = resultAfterClassification;
            BestIntervals = bestIntervals;
            ResultAfterIntervals = resultIntervals;
        }

        public int Code { get; set; }
        public string Name { get; set; }
        public List<string> Classifications { get; set; }
        public List<MatchAnalyzed> Matches { get; set; }
        public double ResultAfterClassification { get; set; }
        public List<BestInterval> BestIntervals { get; set; }
        public List<ResultInterval> ResultAfterIntervals { get; set; }
    }
}
