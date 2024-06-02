using BetPlacer.Core.Models.Response.Microservice.Leagues;

namespace BetPlacer.Backtest.API.Models.Entities.Leagues
{
    public class LeagueSeasonBacktestModel
    {
        public LeagueSeasonBacktestModel(LeaguesApiResponseModel league, LeagueSeasonApiResponseModel season, double ratio)
        {
            LeagueCode = league.Code;
            LeagueName = league.Name;
            LeagueSeasonCode = season.Code;
            LeagueSeasonYear = season.Year;
            LeagueSeasonRatio = ratio;
        }

        public int LeagueCode { get; set; }
        public int LeagueSeasonCode { get; set; }
        public string LeagueName { get; set; }
        public string LeagueSeasonYear { get; set; }
        public double LeagueSeasonRatio { get; set; }
    }
}
