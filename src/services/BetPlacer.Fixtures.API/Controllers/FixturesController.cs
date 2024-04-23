using BetPlacer.Core.Controllers;
using BetPlacer.Core.Models.Response.API.Fixtures;
using BetPlacer.Core.Models.Response.API.Leagues;
using BetPlacer.Core.Models.Response.Core;
using BetPlacer.Fixtures.API.Models.RequestModel;
using BetPlacer.Fixtures.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace BetPlacer.Fixtures.API.Controllers
{
    [Route("api/fixtures")]
    public class FixturesController : BaseController
    {
        private readonly FixturesRepository _fixturesRepository;
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly string _apiKey;

        public FixturesController(FixturesRepository fixturesRepository, IConfiguration configuration)
        {
            _fixturesRepository = fixturesRepository;

            _httpClient = new HttpClient();
            _apiUrl = configuration.GetValue<string>("CoreApi:AppUrl");
            _apiKey = configuration.GetValue<string>("CoreApi:AppKey");

            _httpClient = new HttpClient() { BaseAddress = new Uri(_apiUrl) };
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        [HttpGet("complete")]
        public ActionResult GetCompleteFixtures()
        {
            return OkResponse("Complete");
        }

        [HttpGet("next")]
        public ActionResult GetNextFixtures()
        {
            return OkResponse("Next");
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
                    BaseCoreResponseModel<FixturesResponseModel> response = JsonSerializer.Deserialize<BaseCoreResponseModel<FixturesResponseModel>>(responseLeaguesString);

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
                    BaseCoreResponseModel<FixturesResponseModel> response = JsonSerializer.Deserialize<BaseCoreResponseModel<FixturesResponseModel>>(responseLeaguesString);

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
    }
}
