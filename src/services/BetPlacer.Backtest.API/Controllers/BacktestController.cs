using BetPlacer.Backtest.API.Messages.Consumer;
using BetPlacer.Backtest.API.Models;
using BetPlacer.Backtest.API.Models.Entities;
using BetPlacer.Backtest.API.Models.Request;
using BetPlacer.Backtest.API.Repositories;
using BetPlacer.Backtest.API.Services;
using BetPlacer.Core.Controllers;
using BetPlacer.Core.Models.Response.Core;
using BetPlacer.Core.Models.Response.Microservice.Leagues;
using BetPlacer.Core.Models.Response.Microservice.Teams;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace BetPlacer.Backtest.API.Controllers
{
    [Route("api/backtest")]
    public class BacktestController : BaseController
    {
        private readonly BacktestRepository _backtestRepository;
        private readonly IBacktestOrchestrator _backtestOrchestrator;

        HttpClient _fixturesClient;
        private readonly string _fixturesApiUrl;

        HttpClient _teamsClient;
        private readonly string _teamsApiUrl;

        HttpClient _leaguesClient;
        private readonly string _leaguesApiUrl;

        public BacktestController(BacktestRepository backtestRepository, IConfiguration configuration, IBacktestOrchestrator backtestOrchestrator)
        {
            _backtestRepository = backtestRepository;
            _backtestOrchestrator = backtestOrchestrator ?? throw new ArgumentNullException(nameof(backtestOrchestrator));

            #region FixturesApi

            _fixturesApiUrl = configuration.GetValue<string>("FixturesApi:AppUrl");
            _fixturesClient = new HttpClient() { BaseAddress = new Uri(_fixturesApiUrl) };
            _fixturesClient.Timeout = TimeSpan.FromMinutes(30);

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
                string backtestHash = CalculateSHA256Hash(GenerateRandomString());
                
                Task<IEnumerable<LeaguesApiResponseModel>> taskLeagues = GetLeagues();
                Task<IEnumerable<TeamsApiResponseModel>> taskTeams = GetTeams();

                await Task.WhenAll(taskLeagues, taskTeams);

                IEnumerable<LeaguesApiResponseModel> leagues = taskLeagues.Result;
                IEnumerable<TeamsApiResponseModel> teams = taskTeams.Result;

                _ = GetFixtures(backtestHash);

                BacktestParameters parameters = new BacktestParameters(backtestRequestModel);

                var backtest = await _backtestOrchestrator.StartBacktestAsync(parameters, leagues.ToList(), teams.ToList(), backtestHash);
                await _backtestRepository.CreateBacktest(backtest);

                return OkResponse("Backtest created");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpGet("filters")]
        public ActionResult GetFilters()
        {
            try
            {
                var filters = _backtestRepository.GetFilters();

                return OkResponse(filters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpGet("fixtures")]
        public ActionResult GetFixtureFilters([FromQuery] string fixtureCodesString)
        {
            try
            {
                if (fixtureCodesString == null)
                    return BadRequestResponse("its necessary inform fixtureCodesString param");
                
                List<int> fixtureCodes = fixtureCodesString.Split(',').Select(f =>  int.Parse(f)).ToList();
                var fixtureBacktests = _backtestRepository.GetFixtureBacktests(fixtureCodes);

                return OkResponse(fixtureBacktests);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpPost("fixtures")]
        public async Task<ActionResult> SaveFixtureFilters([FromBody] List<BacktestFilterFixtureRequestModel> request)
        {
            try
            {
                await _backtestRepository.SaveFixtureFilters(request);

                return OkResponse("Filters updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpPut("fixtures/{backtestCode}")]
        public ActionResult UpdateFiltersFixtureFlag(int backtestCode)
        {
            try
            {
                _backtestRepository.UpdateFilters(backtestCode);

                return OkResponse("Filters updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetBacktests([FromQuery] bool onlyWithFilterFixture = false)
        {
            var backtests = _backtestRepository.GetBacktests(onlyWithFilterFixture);
            return OkResponse(backtests);
        }

        [HttpGet("{id}")]
        public ActionResult GetBacktestById(int id)
        {
            var backtests = _backtestRepository.GetBacktests(false, id);

            if (backtests.Count > 0)
                return OkResponse(backtests[0]);

            return OkResponse(null);
        }

        #region Private methods

        public async Task GetFixtures(string backtestHash)
        {
            var request = await _fixturesClient.GetAsync($"?searchType=completed&withGoals=true&withStats=true&saveAsMessage=true&backtestHash={backtestHash}");

            if (request.IsSuccessStatusCode)
            {
                
            }
            else
            {
                var errorMessage = JsonConvert.DeserializeObject<object>(await request.Content.ReadAsStringAsync());
                Console.WriteLine(errorMessage);
                Console.WriteLine(request.StatusCode);
            }
        }

        private async Task<IEnumerable<TeamsApiResponseModel>> GetTeams()
        {
            var request = await _teamsClient.GetAsync("");

            if (request.IsSuccessStatusCode)
            {
                var responseLeaguesString = await request.Content.ReadAsStringAsync();
                BaseCoreResponseModel<TeamsApiResponseModel> response = System.Text.Json.JsonSerializer.Deserialize<BaseCoreResponseModel<TeamsApiResponseModel>>(responseLeaguesString);

                return response.Data;
            }
            else
            {
                var errorMessage = System.Text.Json.JsonSerializer.Deserialize<object>(await request.Content.ReadAsStringAsync());
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
                BaseCoreResponseModel<LeaguesApiResponseModel> response = System.Text.Json.JsonSerializer.Deserialize<BaseCoreResponseModel<LeaguesApiResponseModel>>(responseLeaguesString);

                return response.Data;
            }
            else
            {
                var errorMessage = System.Text.Json.JsonSerializer.Deserialize<object>(await request.Content.ReadAsStringAsync());
                Console.WriteLine(errorMessage);
                Console.WriteLine(request.StatusCode);
                return null;
            }
        }

        private string GenerateRandomString()
        {
            // Tamanho da string aleatória
            int length = 10;

            // Caracteres que podem estar na string
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            // Gerar a string aleatória
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string CalculateSHA256Hash(string input)
        {
            // Criar uma instância do algoritmo de hash SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                // Calcular o hash dos bytes da string
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Converter o hash em uma string hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        #endregion
    }
}
