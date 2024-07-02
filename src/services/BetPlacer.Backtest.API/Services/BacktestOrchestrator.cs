using BetPlacer.Backtest.API.Messages.Consumer;
using BetPlacer.Backtest.API.Models;
using BetPlacer.Backtest.API.Models.Entities;
using BetPlacer.Backtest.API.Models.ValueObjects;
using BetPlacer.Core.Models.Response.Microservice.Leagues;
using BetPlacer.Core.Models.Response.Microservice.Teams;

namespace BetPlacer.Backtest.API.Services
{
    public class BacktestOrchestrator : IBacktestOrchestrator
    {
        private readonly IServiceProvider _serviceProvider;

        public BacktestOrchestrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<BacktestVO> StartBacktestAsync(BacktestParameters parameters, List<LeaguesApiResponseModel> leagues, List<TeamsApiResponseModel> teams, string backtestHash)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var calculateBacktest = new CalculateBacktest(parameters, leagues, teams);
                var consumers = new List<MessageConsumer>
                {
                    new MessageConsumer(calculateBacktest, backtestHash),
                    new MessageConsumer(calculateBacktest, backtestHash),
                    new MessageConsumer(calculateBacktest, backtestHash)
                };

                // Starta todos os consumidores
                foreach (var consumer in consumers)
                {
                    consumer.StartConsuming();
                    await consumer.StartAsync(CancellationToken.None);
                }

                while (consumers.Count(consumer => consumer.IsFinished) < 1)
                {
                    await Task.Delay(2000);
                }

                // Pare todos os consumidores
                foreach (var consumer in consumers)
                {
                    consumer.StopConsuming();
                    await consumer.StopAsync(CancellationToken.None);
                }

                consumers.First().DeleteQueue();

                var backtestResult = calculateBacktest.GenerateResult();
                return backtestResult;
            }
        }
    }
}
