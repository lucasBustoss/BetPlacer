using BetPlacer.Core.Controllers;
using BetPlacer.Core.Models.Response.FootballAPI.Fixtures;
using BetPlacer.Core.Models.Response.Core;
using BetPlacer.Core.Models.Response.Microservice.Leagues;
using BetPlacer.Core.Models.Response.Microservice.Teams;
using BetPlacer.Fixtures.API.Models.Enums;
using BetPlacer.Fixtures.API.Models.RequestModel;
using BetPlacer.Fixtures.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using BetPlacer.Fixtures.API.Models.ValueObjects;
using BetPlacer.Fixtures.API.Models.Entities.Trade;

namespace BetPlacer.Fixtures.API.Controllers
{
    [Route("api/fixtures")]
    public class FixturesController : BaseController
    {
        private readonly FixturesRepository _fixturesRepository;
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly string _apiKey;

        HttpClient _teamsClient = new HttpClient();
        private readonly string _teamsApiUrl;

        HttpClient _leaguesClient = new HttpClient();
        private readonly string _leaguesApiUrl;

        public FixturesController(FixturesRepository fixturesRepository, IConfiguration configuration)
        {
            _fixturesRepository = fixturesRepository;

            #region CoreApi

            _apiUrl = configuration.GetValue<string>("CoreApi:AppUrl");
            _apiKey = configuration.GetValue<string>("CoreApi:AppKey");

            _httpClient = new HttpClient() { BaseAddress = new Uri(_apiUrl) };
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

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

        [HttpGet("")]
        public async Task<ActionResult> GetFixtures([FromQuery] string searchType, [FromQuery] bool withGoals = false, [FromQuery] bool withStats = false, [FromQuery] bool saveAsMessage = false, [FromQuery] string backtestHash = "")
        {
            FixtureListSearchType type = FixtureListSearchType.All;

            if (searchType == "completed")
                type = FixtureListSearchType.OnlyCompleted;
            else if (searchType == "incompleted")
                type = FixtureListSearchType.OnlyNext;


            Task<IEnumerable<LeaguesApiResponseModel>> taskLeagues = GetLeagues();
            Task<IEnumerable<TeamsApiResponseModel>> taskTeams = GetTeams();

            await Task.WhenAll(taskLeagues, taskTeams);

            IEnumerable<LeaguesApiResponseModel> leagues = taskLeagues.Result;
            IEnumerable<TeamsApiResponseModel> teams = taskTeams.Result;

            if (saveAsMessage)
            {
                _ = _fixturesRepository.List(type, leagues, teams, withGoals, withStats, saveAsMessage, backtestHash);
                return OkResponse("Messages created");
            }
            else
            {
                var fixtures = await _fixturesRepository.List(type, leagues, teams, withGoals, withStats, saveAsMessage, backtestHash);
                return OkResponse(fixtures.ToList());
            }
        }

        [HttpPost]
        public async Task<ActionResult> SyncFixtures([FromBody] FixturesRequestModel syncRequestModel)
        {
            try
            {

                await SyncCompleteFixtures(syncRequestModel);
                await SyncNextFixtures(syncRequestModel);

                return OkResponse("Fixtures synchronized");
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpPost("complete")]
        public async Task<ActionResult> SyncCompleteFixtures([FromBody] FixturesRequestModel syncRequestModel)
        {
            try
            {
                if (syncRequestModel == null || !syncRequestModel.IsValid())
                    throw new Exception("param leagueSeasonCode is required.");

                var request = await _httpClient.GetAsync($"fixtures/complete?leagueSeasonCode={syncRequestModel.LeagueSeasonCode}");

                if (request.IsSuccessStatusCode)
                {
                    var responseLeaguesString = await request.Content.ReadAsStringAsync();
                    BaseCoreResponseModel<FixturesFootballResponseModel> response = JsonSerializer.Deserialize<BaseCoreResponseModel<FixturesFootballResponseModel>>(responseLeaguesString);

                    await _fixturesRepository.CreateOrUpdateCompleteFixtures(response.Data);

                    return OkResponse("Fixtures completed synchronized");
                }
                {
                    var errorMessage = JsonSerializer.Deserialize<object>(await request.Content.ReadAsStringAsync());
                    Console.WriteLine(errorMessage);
                    Console.WriteLine(request.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpPost("next")]
        public async Task<ActionResult> SyncNextFixtures([FromBody] FixturesRequestModel syncRequestModel)
        {
            try
            {
                if (syncRequestModel == null || !syncRequestModel.IsValid())
                    throw new Exception("param leagueSeasonCode is required.");

                var request = await _httpClient.GetAsync($"fixtures/next?leagueSeasonCode={syncRequestModel.LeagueSeasonCode}");

                if (request.IsSuccessStatusCode)
                {
                    var responseLeaguesString = await request.Content.ReadAsStringAsync();
                    BaseCoreResponseModel<FixturesFootballResponseModel> response = JsonSerializer.Deserialize<BaseCoreResponseModel<FixturesFootballResponseModel>>(responseLeaguesString);

                    await _fixturesRepository.CreateNextFixtures(response.Data);

                    return OkResponse("Next fixtures synchronized");
                }
                {
                    var errorMessage = JsonSerializer.Deserialize<object>(await request.Content.ReadAsStringAsync());
                    Console.WriteLine(errorMessage);
                    Console.WriteLine(request.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpPost("stats")]
        public async Task<ActionResult> CalculateStats([FromBody] FixturesRequestModel syncRequestModel)
        {
            try
            {
                if (syncRequestModel == null || !syncRequestModel.IsValid())
                    throw new Exception("param leagueSeasonCode is required.");

                await _fixturesRepository.CalculateFixtureStats(syncRequestModel.LeagueSeasonCode.Value);

                return OkResponse("stats calculated.");
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }


        #region Private methods

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
