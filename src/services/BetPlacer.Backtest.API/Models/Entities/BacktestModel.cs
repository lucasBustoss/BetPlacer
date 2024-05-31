using BetPlacer.Backtest.API.Models.Entities.Leagues;

namespace BetPlacer.Backtest.API.Models.Entities
{
    public class BacktestModel
    {
        public DateTime CreationDate { get; set; }
        public int Type { get; set; }
        public int TeamType { get; set; }
        public double Ratio { get; set; }
        public int MaxGoodRun { get; set; }
        public int MaxBadRun { get; set; }
        public List<LeagueBacktestModel> Leagues { get; set; }
        public List<LeagueSeasonBacktestModel> LeagueSeasons { get; set; }
        public List<TeamBacktestModel> Teams { get; set; }
        public List<TeamSeasonBacktestModel> TeamSeasons { get; set; }
    }
}
