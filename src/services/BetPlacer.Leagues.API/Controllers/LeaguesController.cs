using BetPlacer.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using BetPlacer.Leagues.API.Repositories;
using System.Text.Json;
using BetPlacer.Core.Models.Response.Core;
using BetPlacer.Core.Models.Response.API.Leagues;

namespace BetPlacer.Leagues.Controllers
{
    [Route("api/leagues")]
    public class LeaguesController : BaseController
    {
        private readonly LeaguesRepository _leaguesRepository;
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly string _apiKey;

        public LeaguesController(LeaguesRepository leaguesRepository, IConfiguration configuration)
        {
            _leaguesRepository = leaguesRepository;
            
            _httpClient = new HttpClient();
            _apiUrl = configuration.GetValue<string>("CoreApi:AppUrl");
            _apiKey = configuration.GetValue<string>("CoreApi:AppKey");

            _httpClient = new HttpClient() { BaseAddress = new Uri(_apiUrl) };
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        [HttpGet]
        public ActionResult GetLeagues()
        {
            var leagues = _leaguesRepository.List(true);
            return OkResponse(leagues);
        }

        [HttpPost]
        public async Task<ActionResult> SyncLeagues()
        {
            try
            {
                var request = await _httpClient.GetAsync("leagues");

                if (request.IsSuccessStatusCode)
                {
                    var responseLeaguesString = await request.Content.ReadAsStringAsync();
                    BaseCoreResponseModel<LeaguesResponseModel> response = JsonSerializer.Deserialize<BaseCoreResponseModel<LeaguesResponseModel>>(responseLeaguesString);

                    await _leaguesRepository.CreateOrUpdate(response.Data);

                    var leagues = _leaguesRepository.List(true);

                    return OkResponse(leagues);
                }
                //else
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
