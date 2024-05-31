namespace BetPlacer.Backtest.API.Models.Entities.Leagues
{
    public class TeamSeasonBacktestModel
    {
        public int TeamCode { get; set; }
        public string TeamName { get; set; }
        public string TeamSeasonYear { get; set; }
        public double TeamSeasonRatio { get; set; }
    }
}
