using BetPlacer.Core.Controllers;
using BetPlacer.Core.Models.Response.FootballAPI.Fixtures;
using BetPlacer.Core.Models.Response.Core;
using BetPlacer.Core.Models.Response.Microservice.Leagues;
using BetPlacer.Core.Models.Response.Microservice.Teams;
using BetPlacer.Fixtures.API.Models.Enums;
using BetPlacer.Fixtures.API.Models.RequestModel;
using BetPlacer.Fixtures.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using BetPlacer.Backtest.API.Models;
using BetPlacer.Core.API.Models.Request.PinnacleOdds;
using System.Text;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.Request;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.Entities;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Telegram;

namespace BetPlacer.Fixtures.API.Controllers
{
    [Route("api/fixtures")]
    public class FixturesController : BaseController
    {
        private readonly IFixturesRepository _fixturesRepository;
        private readonly HttpClient _coreClient;
        private readonly string _apiUrl;
        private readonly string _apiKey;

        HttpClient _teamsClient = new HttpClient();
        private readonly string _teamsApiUrl;

        HttpClient _leaguesClient = new HttpClient();
        private readonly string _leaguesApiUrl;

        HttpClient _backetestClient = new HttpClient();
        private readonly string _backetestApiUrl;

        HttpClient _punterClient = new HttpClient();
        private readonly string _punterApiUrl;

        HttpClient _telegramClient = new HttpClient();
        private readonly string _telegramApiUrl;

        public FixturesController(IFixturesRepository fixturesRepository, IConfiguration configuration)
        {
            _fixturesRepository = fixturesRepository;

            #region CoreApi

            _apiUrl = configuration.GetValue<string>("CoreApi:AppUrl");
            _apiKey = configuration.GetValue<string>("CoreApi:AppKey");

            var coreApiAddress = Environment.GetEnvironmentVariable("BETPLACER_CoreApiAddress") ?? configuration["BetPlacer:CoreApiAddress"];
            if (string.IsNullOrEmpty(coreApiAddress))
                throw new Exception("A variável de ambiente BETPLACER_CoreApiAddress não está definida.");

            _apiUrl = coreApiAddress;

            _coreClient = new HttpClient() { BaseAddress = new Uri(_apiUrl) };
            _coreClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            _coreClient.DefaultRequestHeaders.Add("Accept", "application/json");

            #endregion

            #region TeamsApi

            var teamsApiAddress = Environment.GetEnvironmentVariable("BETPLACER_TeamsApiAddress") ?? configuration["BetPlacer:TeamsApiAddress"];
            if (string.IsNullOrEmpty(teamsApiAddress))
                throw new Exception("A variável de ambiente BETPLACER_TeamsApiAddress não está definida.");

            _teamsApiUrl = teamsApiAddress;

            #endregion

            #region LeaguesApi

            var leaguesApiAddress = Environment.GetEnvironmentVariable("BETPLACER_LeaguesApiAddress") ?? configuration["BetPlacer:LeaguesApiAddress"];
            if (string.IsNullOrEmpty(leaguesApiAddress))
                throw new Exception("A variável de ambiente BETPLACER_LeaguesApiAddress não está definida.");

            _leaguesApiUrl = leaguesApiAddress;

            #endregion

            #region PunterApi

            var punterApiAddress = Environment.GetEnvironmentVariable("BETPLACER_PunterApiAddress") ?? configuration["BetPlacer:PunterApiAddress"];
            if (string.IsNullOrEmpty(punterApiAddress))
                throw new Exception("A variável de ambiente BETPLACER_PunterApiAddress não está definida.");

            _punterApiUrl = punterApiAddress;

            #endregion

            #region TelegramApi

            var telegramApiAddress = Environment.GetEnvironmentVariable("BETPLACER_TelegramApiAddress") ?? configuration["BetPlacer:TelegramApiAddress"];
            if (string.IsNullOrEmpty(telegramApiAddress))
                throw new Exception("A variável de ambiente BETPLACER_TelegramApiAddress não está definida.");

            _telegramApiUrl = telegramApiAddress;

            #endregion

            _leaguesClient = new HttpClient() { BaseAddress = new Uri(_leaguesApiUrl) };
            _teamsClient = new HttpClient() { BaseAddress = new Uri(_teamsApiUrl) };
            _punterClient = new HttpClient() { BaseAddress = new Uri(_punterApiUrl) };
            _telegramClient = new HttpClient() { BaseAddress = new Uri(_telegramApiUrl) };
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
                var fixtures = _fixturesRepository.List(type, leagues, teams, withGoals, withStats, saveAsMessage, backtestHash);
                return OkResponse(fixtures.ToList());
            }
        }

        [HttpGet("odds/{leagueCode}")]
        public async Task<ActionResult> GetFixturesWithoutOdds(int leagueCode)
        {
            var league = await GetLeaguesById(leagueCode);

            var fixtures = _fixturesRepository.GetFixturesWithoutOdds(league.FirstOrDefault());
            return OkResponse(fixtures);
        }

        [HttpGet("date")]
        public async Task<ActionResult> GetFixturesByDate()
        {
            var startDate = DateTime.UtcNow.Date;
            var endDate = DateTime.UtcNow.Date.AddDays(3).AddMilliseconds(-1);
            var fixtureCodes = _fixturesRepository.GetFixtureCodesByDate(startDate, endDate);

            IEnumerable<PunterBacktestFixture> fixturesStrategy = await GetFixturesStrategy(fixtureCodes);

            Task<IEnumerable<LeaguesApiResponseModel>> taskLeagues = GetLeagues();
            Task<IEnumerable<TeamsApiResponseModel>> taskTeams = GetTeams();

            await Task.WhenAll(taskLeagues, taskTeams);

            IEnumerable<LeaguesApiResponseModel> leagues = taskLeagues.Result;
            IEnumerable<TeamsApiResponseModel> teams = taskTeams.Result;

            var fixtures = _fixturesRepository.ListFixturesByDate(leagues, teams, fixturesStrategy);
            return OkResponse(fixtures.ToList());
        }

        [HttpPost("odds")]
        public ActionResult CreateOdds([FromBody] FixtureOddsRequest oddsRequest)
        {
            try
            {
                _fixturesRepository.CreateOrUpdateOdds(new FixtureOdds(oddsRequest));

                return OkResponse("odds created");
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpPut("odds")]
        public ActionResult UpdateOdds([FromBody] FixtureOddsRequest oddsRequest)
        {
            try
            {
                _fixturesRepository.CreateOrUpdateOdds(new FixtureOdds(oddsRequest));

                return OkResponse("odds updated");
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
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

                var request = await _coreClient.GetAsync($"fixtures/complete?leagueSeasonCode={syncRequestModel.LeagueSeasonCode}");

                if (request.IsSuccessStatusCode)
                {
                    var responseLeaguesString = await request.Content.ReadAsStringAsync();
                    BaseCoreResponseModel<FixturesFootballResponseModel> response = JsonSerializer.Deserialize<BaseCoreResponseModel<FixturesFootballResponseModel>>(responseLeaguesString);

                    _fixturesRepository.CreateOrUpdateCompleteFixtures(response.Data);

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
        public async Task<ActionResult> SyncNextFixtures(FixturesRequestModel syncRequestModel)
        {
            try
            {
                if (syncRequestModel == null || !syncRequestModel.IsValid())
                    throw new Exception("param leagueSeasonCode is required.");

                var request = await _coreClient.GetAsync($"fixtures/next?leagueSeasonCode={syncRequestModel.LeagueSeasonCode}");

                if (request.IsSuccessStatusCode)
                {
                    var responseLeaguesString = await request.Content.ReadAsStringAsync();
                    BaseCoreResponseModel<FixturesFootballResponseModel> response = JsonSerializer.Deserialize<BaseCoreResponseModel<FixturesFootballResponseModel>>(responseLeaguesString);

                    if (response != null & response.Data != null && response.Data.Count() > 0)
                    {
                        var leagues = await GetLeagues();
                        var league = leagues.Where(l => l.Season.Any(s => s.Code == syncRequestModel.LeagueSeasonCode)).FirstOrDefault();
                        List<PinnacleOddsModel> pinnacleOdds = await GetPinnacleOdds(league.Code);

                        var matchesNotFound = _fixturesRepository.CreateOrUpdateNextFixtures(response.Data, pinnacleOdds);

                        if (matchesNotFound != null && matchesNotFound.Count > 0)
#pragma warning disable 4014
                            SendTelegramMessage(1, matchesNotFound);
#pragma warning restore 4014

                    }

                    return OkResponse("Next fixtures synchronized");
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

        [HttpPost("stats")]
        public ActionResult CalculateStats([FromBody] FixturesRequestModel syncRequestModel)
        {
            try
            {
                if (syncRequestModel == null || !syncRequestModel.IsValid())
                    throw new Exception("param leagueSeasonCode is required.");

                _fixturesRepository.CalculateFixtureStats(syncRequestModel.LeagueSeasonCode.Value);

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

        private async Task<IEnumerable<LeaguesApiResponseModel>> GetLeaguesById(int leagueCode)
        {
            var request = await _leaguesClient.GetAsync($"{leagueCode}");

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

        private async Task<IEnumerable<BacktestFixture>> GetBacktestFixtures(List<int> fixtureCodes)
        {
            var request = await _backetestClient.GetAsync($"fixtures?fixtureCodesString={string.Join(", ", fixtureCodes)}");

            if (request.IsSuccessStatusCode)
            {
                var responseLeaguesString = await request.Content.ReadAsStringAsync();
                BaseCoreResponseModel<BacktestFixture> response = JsonSerializer.Deserialize<BaseCoreResponseModel<BacktestFixture>>(responseLeaguesString);

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

        private async Task<IEnumerable<PunterBacktestFixture>> GetFixturesStrategy(List<int> fixtureCodes)
        {
            var request = await _punterClient.GetAsync($"fixtures");

            if (request.IsSuccessStatusCode)
            {
                var responseLeaguesString = await request.Content.ReadAsStringAsync();
                BaseCoreResponseModel<PunterBacktestFixture> response = JsonSerializer.Deserialize<BaseCoreResponseModel<PunterBacktestFixture>>(responseLeaguesString);

                return response.Data.Where(d => fixtureCodes.Contains(d.FixtureCode)).ToList();
            }
            else
            {
                var errorMessage = JsonSerializer.Deserialize<object>(await request.Content.ReadAsStringAsync());
                Console.WriteLine(errorMessage);
                Console.WriteLine(request.StatusCode);
                return null;
            }
        }

        private async Task<List<PinnacleOddsModel>> GetPinnacleOdds(int leagueCode)
        {
            var request = await _coreClient.GetAsync($"pinnacle?leagueCode={leagueCode}");

            if (request.IsSuccessStatusCode)
            {
                var responseLeaguesString = await request.Content.ReadAsStringAsync();
                BaseCoreResponseModel<PinnacleOddsModel> response = JsonSerializer.Deserialize<BaseCoreResponseModel<PinnacleOddsModel>>(responseLeaguesString);

                return response.Data.ToList();
            }
            else
            {
                var errorMessage = JsonSerializer.Deserialize<object>(await request.Content.ReadAsStringAsync());
                Console.WriteLine(errorMessage);
                Console.WriteLine(request.StatusCode);
                return null;
            }
        }

        private async Task SendTelegramMessage(int type, List<string> objectName)
        {
            try
            {
                TelegramRequestModel requestModel = new TelegramRequestModel(type, objectName, null);
                string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                
                var request = await _telegramClient.PostAsync("", httpContent);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        #endregion
    }
}
