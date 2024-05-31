using BetPlacer.Backtest.API.Models;
using BetPlacer.Backtest.API.Models.Request;
using BetPlacer.Backtest.API.Services;
using BetPlacer.Core.Controllers;
using BetPlacer.Core.Models.Response.Core;
using BetPlacer.Core.Models.Response.Microservice.Leagues;
using BetPlacer.Core.Models.Response.Microservice.Teams;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BetPlacer.Backtest.API.Controllers
{
    [Route("api/backtest")]
    public class BacktestController : BaseController
    {
        HttpClient _fixturesClient = new HttpClient();
        private readonly string _fixturesApiUrl;

        public BacktestController(IConfiguration configuration)
        {
            #region FixturesApi

            _fixturesApiUrl = configuration.GetValue<string>("FixturesApi:AppUrl");
            _fixturesClient = new HttpClient() { BaseAddress = new Uri(_fixturesApiUrl) };

            #endregion
        }

        [HttpPost]
        public async Task<ActionResult> CalculateBacktest([FromBody] BacktestRequestModel backtestRequestModel)
        {
            try
            {
                IEnumerable<FixturesApiResponseModel> fixtures = await this.GetFixtures();
                BacktestParameters backtest = new BacktestParameters(backtestRequestModel);

                CalculateBacktest calculateBacktest = new CalculateBacktest();
                calculateBacktest.Calculate(backtest, fixtures.ToList());

                return OkResponse("backtest generated");
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }

        #region Private methods

        private async Task<IEnumerable<FixturesApiResponseModel>> GetFixtures()
        {
            var request = await _fixturesClient.GetAsync("?searchType=completed&withGoals=true&withStats=true");

            if (request.IsSuccessStatusCode)
            {
                var responseLeaguesString = await request.Content.ReadAsStringAsync();
                BaseCoreResponseModel<FixturesApiResponseModel> response = JsonSerializer.Deserialize<BaseCoreResponseModel<FixturesApiResponseModel>>(responseLeaguesString);

                return response.Data;
            }
            else
            {
                var errorMessage = JsonSerializer.Deserialize<object>(await request.Content.ReadAsStringAsync());
                Console.WriteLine(errorMessage);
                Console.WriteLine(request.StatusCode);
                return null;
            }
        }


        #endregion
    }
}
