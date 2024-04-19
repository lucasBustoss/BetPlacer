using BetPlacer.Core.Controllers;
using BetPlacer.Core.Models.Response.API.Teams;
using BetPlacer.Core.Models.Response.Core;
using BetPlacer.Teams.API.Controllers.RequestModel;
using BetPlacer.Teams.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BetPlacer.Teams.API.Controllers
{
    [Route("api/teams")]
    public class TeamsController : BaseController
    {
        private readonly TeamsRepository _teamsRepository;
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly string _apiKey;

        public TeamsController(TeamsRepository teamsRepository, IConfiguration configuration)
        {
            _teamsRepository = teamsRepository;

            _httpClient = new HttpClient();
            _apiUrl = configuration.GetValue<string>("CoreApi:AppUrl");
            _apiKey = configuration.GetValue<string>("CoreApi:AppKey");

            _httpClient = new HttpClient() { BaseAddress = new Uri(_apiUrl) };
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        [HttpGet]
        public ActionResult GetTeams()
        {
            return OkResponse("Deu certo");
        }

        [HttpPost]
        public async Task<ActionResult> SyncTeams([FromBody] TeamsRequestModel syncRequestModel)
        {
            try
            {
                if (syncRequestModel == null || !syncRequestModel.IsValid())
                    throw new Exception("param leagueSeasonCode is required.");

                var request = await _httpClient.GetAsync($"teams?leagueSeasonCode={syncRequestModel.LeagueSeasonCode}");

                if (request.IsSuccessStatusCode)
                {
                    var responseLeaguesString = await request.Content.ReadAsStringAsync();
                    BaseCoreResponseModel<TeamsResponseModel> response = JsonSerializer.Deserialize<BaseCoreResponseModel<TeamsResponseModel>>(responseLeaguesString);

                    if (response != null && response.Data != null)
                        await _teamsRepository.Create(response.Data);

                    return OkResponse("Teams synchronized.");
                }
                else
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
