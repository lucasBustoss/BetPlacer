using BetPlacer.Backtest.API.Models;
using BetPlacer.Backtest.API.Models.Entities;
using BetPlacer.Backtest.API.Models.ValueObjects;
using BetPlacer.Core.Models.Response.Microservice.Leagues;
using BetPlacer.Core.Models.Response.Microservice.Teams;

namespace BetPlacer.Backtest.API.Services
{
    public interface IBacktestOrchestrator
    {
        Task<BacktestVO> StartBacktestAsync(BacktestParameters parameters, List<LeaguesApiResponseModel> leagues, List<TeamsApiResponseModel> teams, string backtestHash);
    }
}
