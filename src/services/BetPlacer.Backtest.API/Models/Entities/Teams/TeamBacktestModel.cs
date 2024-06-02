using BetPlacer.Core.Models.Response.Microservice.Teams;

namespace BetPlacer.Backtest.API.Models.Entities.Leagues
{
    public class TeamBacktestModel
    {
        public TeamBacktestModel(TeamsApiResponseModel team, double ratio)
        {
            TeamCode = team.Code;
            TeamName = team.Name;
            TeamRatio = ratio;
        }

        public int TeamCode { get; set; }
        public string TeamName { get; set; }
        public double TeamRatio { get; set; }
    }
}
