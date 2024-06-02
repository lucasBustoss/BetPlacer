using BetPlacer.Core.Models.Response.Microservice.Leagues;

namespace BetPlacer.Backtest.API.Models.Entities.Leagues
{
    public class LeagueBacktestModel
    {
        public LeagueBacktestModel(LeaguesApiResponseModel league, double leagueRatio)
        {
            LeagueCode = league.Code;
            LeagueName = league.Name;
            LeagueRatio = leagueRatio;
        }

        public int LeagueCode { get; set; }
        public string LeagueName { get; set; }
        public double LeagueRatio { get; set; }
    }
}
