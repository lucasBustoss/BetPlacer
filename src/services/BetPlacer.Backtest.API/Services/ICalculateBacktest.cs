using BetPlacer.Backtest.API.Models.Entities;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures;

namespace BetPlacer.Backtest.API.Services
{
    public interface ICalculateBacktest
    {
        void CalculateFixture(FixturesApiResponseModel fixture);
        BacktestModel GenerateResult();
    }
}
