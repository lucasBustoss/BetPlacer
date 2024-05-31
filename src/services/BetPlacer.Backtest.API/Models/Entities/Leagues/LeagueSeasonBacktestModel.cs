namespace BetPlacer.Backtest.API.Models.Entities.Leagues
{
    public class LeagueSeasonBacktestModel
    {
        public int LeagueCode { get; set; }
        public string LeagueName { get; set; }
        public string LeagueSeasonYear { get; set; }
        public double LeagueSeasonRatio { get; set; }
    }
}
