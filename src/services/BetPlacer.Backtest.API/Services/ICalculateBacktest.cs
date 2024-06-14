using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures;
using BetPlacer.Backtest.API.Models.ValueObjects;

namespace BetPlacer.Backtest.API.Services
{
    public interface ICalculateBacktest
    {
        void CalculateFixture(FixturesApiResponseModel fixture);
        BacktestVO GenerateResult();
    }
}
