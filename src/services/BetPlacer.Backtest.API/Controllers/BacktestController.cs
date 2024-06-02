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

        HttpClient _teamsClient = new HttpClient();
        private readonly string _teamsApiUrl;

        HttpClient _leaguesClient = new HttpClient();
        private readonly string _leaguesApiUrl;

        public BacktestController(IConfiguration configuration)
        {
            #region FixturesApi

            _fixturesApiUrl = configuration.GetValue<string>("FixturesApi:AppUrl");
            _fixturesClient = new HttpClient() { BaseAddress = new Uri(_fixturesApiUrl) };

            #endregion

            #region TeamsApi

            _teamsApiUrl = configuration.GetValue<string>("TeamsApi:AppUrl");
            _teamsClient = new HttpClient() { BaseAddress = new Uri(_teamsApiUrl) };

            #endregion

            #region LeaguesApi

            _leaguesApiUrl = configuration.GetValue<string>("LeaguesApi:AppUrl");
            _leaguesClient = new HttpClient() { BaseAddress = new Uri(_leaguesApiUrl) };

            #endregion
        }

        [HttpPost]
        public async Task<ActionResult> CalculateBacktest([FromBody] BacktestRequestModel backtestRequestModel)
        {
            try
            {
                IEnumerable<FixturesApiResponseModel> fixtures = await GetFixtures();
                
                Task<IEnumerable<LeaguesApiResponseModel>> taskLeagues = GetLeagues();
                Task<IEnumerable<TeamsApiResponseModel>> taskTeams = GetTeams();

                await Task.WhenAll(taskLeagues, taskTeams);

                IEnumerable<LeaguesApiResponseModel> leagues = taskLeagues.Result;
                IEnumerable<TeamsApiResponseModel> teams = taskTeams.Result;

                BacktestParameters parameters = new BacktestParameters(backtestRequestModel);

                CalculateBacktest calculateBacktest = new CalculateBacktest();
                var backtest = calculateBacktest.Calculate(parameters, fixtures.ToList(), leagues.ToList(), teams.ToList());

                return OkResponse(backtest);
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

        private async Task<IEnumerable<TeamsApiResponseModel>> GetTeams()
        {
            var request = await _teamsClient.GetAsync("");

            if (request.IsSuccessStatusCode)
            {
                var responseLeaguesString = await request.Content.ReadAsStringAsync();
                BaseCoreResponseModel<TeamsApiResponseModel> response = JsonSerializer.Deserialize<BaseCoreResponseModel<TeamsApiResponseModel>>(responseLeaguesString);

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

        private async Task<IEnumerable<LeaguesApiResponseModel>> GetLeagues()
        {
            var request = await _leaguesClient.GetAsync("?withSeasons=true");

            if (request.IsSuccessStatusCode)
            {
                var responseLeaguesString = await request.Content.ReadAsStringAsync();
                BaseCoreResponseModel<LeaguesApiResponseModel> response = JsonSerializer.Deserialize<BaseCoreResponseModel<LeaguesApiResponseModel>>(responseLeaguesString);

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
