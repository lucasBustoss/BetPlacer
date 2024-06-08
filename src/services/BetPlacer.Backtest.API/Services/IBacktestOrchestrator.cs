using BetPlacer.Backtest.API.Models;
using BetPlacer.Backtest.API.Models.Entities;
using BetPlacer.Core.Models.Response.Microservice.Leagues;
using BetPlacer.Core.Models.Response.Microservice.Teams;

namespace BetPlacer.Backtest.API.Services
{
    public interface IBacktestOrchestrator
    {
        Task<BacktestModel> StartBacktestAsync(BacktestParameters parameters, List<LeaguesApiResponseModel> leagues, List<TeamsApiResponseModel> teams, string backtestHash);
    }
}
