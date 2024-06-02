using BetPlacer.Backtest.API.Models.Entities.Leagues;
using BetPlacer.Backtest.API.Models.Entities.Odds;

namespace BetPlacer.Backtest.API.Models.Entities
{
    public class BacktestModel
    {
        public BacktestModel()
        {
            CreationDate = DateTime.Now;
            Leagues = new List<LeagueBacktestModel>();
            LeagueSeasons = new List<LeagueSeasonBacktestModel>();
            Teams = new List<TeamBacktestModel>();
            Odds = new List<OddsModel>();
        }

        public DateTime CreationDate { get; set; }
        public int Type { get; set; }
        public int TeamType { get; set; }
        public double FilteredFixtures { get; set; }
        public double MatchedFixtures { get; set; }
        public int MaxGoodRun { get; set; }
        public int MaxBadRun { get; set; }
        public List<LeagueBacktestModel> Leagues { get; set; }
        public List<LeagueSeasonBacktestModel> LeagueSeasons { get; set; }
        public List<TeamBacktestModel> Teams { get; set; }
        public List<OddsModel> Odds { get; set; }
    }
}
