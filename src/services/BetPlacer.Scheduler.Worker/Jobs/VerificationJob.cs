using BetPlacer.Core.Models.Response.Core;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.LeagueFixtureByDate;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Punter;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Telegram;
using BetPlacer.Scheduler.Worker.Repositories;
using Quartz;
using System.Net.Http;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;

namespace BetPlacer.Scheduler.Worker.Jobs
{
    [DisallowConcurrentExecution]
    public class VerificationJob : IJob
    {
        ISchedulerRepository _repository;
        private readonly HttpClient _httpClient;
        private readonly string _leaguesApiUrl;
        private readonly string _syncApiUrl;
        private readonly string _fixturesApiUrl;
        private readonly string _punterApiUrl;
        private readonly string _telegramApiUrl;

        public VerificationJob(ISchedulerRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(30);

            #region leaguesApi

            var leaguesApiUrl = Environment.GetEnvironmentVariable("BETPLACER_LeaguesApiAddress") ?? configuration["BetPlacer:LeaguesApiAddress"];

            if (string.IsNullOrEmpty(leaguesApiUrl))
                throw new Exception("A variável de ambiente BETPLACER_LeaguesApiAddress não está definida.");

            _leaguesApiUrl = leaguesApiUrl;

            #endregion

            #region SyncApi

            var syncApiUrl = Environment.GetEnvironmentVariable("BETPLACER_SyncApiAddress") ?? configuration["BetPlacer:SyncApiAddress"];

            if (string.IsNullOrEmpty(syncApiUrl))
                throw new Exception("A variável de ambiente BETPLACER_SyncApiAddress não está definida.");

            _syncApiUrl = syncApiUrl;

            #endregion

            #region FixturesApi

            var fixturesApiUrl = Environment.GetEnvironmentVariable("BETPLACER_FixturesApiAddress") ?? configuration["BetPlacer:FixturesApiAddress"];

            if (string.IsNullOrEmpty(fixturesApiUrl))
                throw new Exception("A variável de ambiente BETPLACER_FixturesApiAddress não está definida.");

            _fixturesApiUrl = fixturesApiUrl;

            #endregion

            #region PunterApi

            var punterApiUrl = Environment.GetEnvironmentVariable("BETPLACER_PunterApiAddress") ?? configuration["BetPlacer:PunterApiAddress"];

            if (string.IsNullOrEmpty(punterApiUrl))
                throw new Exception("A variável de ambiente BETPLACER_PunterApiAddress não está definida.");

            _punterApiUrl = punterApiUrl;

            #endregion

            #region TelegramApi

            var telegramApiUrl = Environment.GetEnvironmentVariable("BETPLACER_TelegramApiAddress") ?? configuration["BetPlacer:TelegramApiAddress"];

            if (string.IsNullOrEmpty(telegramApiUrl))
                throw new Exception("A variável de ambiente BETPLACER_TelegramApiAddress não está definida.");

            _telegramApiUrl = telegramApiUrl;

            #endregion
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"Começando a verificação...");

            var executionOfDay = _repository.GetSchedulerExecution();

            if (executionOfDay != null && (!executionOfDay.OddsFilled || !executionOfDay.AnalysisDone))
            {
                #region First step

                Console.WriteLine("Sincronizando as partidas...");
                await SyncFixtures();

                Console.WriteLine("Buscando as partidas atuais...");
                LeagueFixtureByDate fixtures = await GetFixturesByDate();

                #endregion

                #region Last step

                List<int> leagueCodes = fixtures.LeagueFixtures.Select(l => l.LeagueCode).ToList();

                Console.WriteLine("Analisando os métodos...");
                await AnalyzeMatches(leagueCodes);
                
                Console.WriteLine("Retornando todos os dados...");
                fixtures = await GetFixturesByDate();

                #endregion

                if (!executionOfDay.OddsFilled)
                {
                    bool filledNow = true;
                    
                    foreach (var league in fixtures.LeagueFixtures)
                    {
                        var fixturesWithoutOdds = league.Fixtures.Where(f => !f.InformedOdds).ToList();

                        if (fixturesWithoutOdds != null && fixturesWithoutOdds.Count > 0)
                        {
                            await SendTelegramMessage(fixturesWithoutOdds, 2);
                            filledNow = false;
                        }
                    }

                    if (filledNow)
                        executionOfDay.OddsFilled = true;
                    else
                        return;
                }

                List<FixtureDate> fixturesToSendTelegram = new List<FixtureDate>();
                if (!executionOfDay.AnalysisDone)
                {
                    bool analyzedNow = true;

                    foreach (var league in fixtures.LeagueFixtures)
                    {
                        var fixturesWithoutAnalysis = league.Fixtures.Where(f => !f.AnalyzedFixture).ToList();

                        if (fixturesWithoutAnalysis != null && fixturesWithoutAnalysis.Count > 0)
                        {
                            await SendTelegramMessage(fixturesWithoutAnalysis, 3);
                            analyzedNow = false;
                        } else
                        {
                            var fixturesWithFilters = league.Fixtures.Where(f => f.Filters != null).ToList();

                            if (fixturesWithFilters != null && fixturesWithFilters.Count > 0)
                                fixturesToSendTelegram.AddRange(fixturesWithFilters);
                        }
                    }

                    if (analyzedNow)
                    {
                        executionOfDay.AnalysisDone = true;
                        await SendTelegramMessage(fixturesToSendTelegram, 4);
                    }
                    else
                        return;
                }
                
                if (executionOfDay.AnalysisDone || executionOfDay.OddsFilled)
                    _repository.UpdateExecution(executionOfDay);
            }

            Console.WriteLine("Fim de execução");
        }

        #region Private methods

        private async Task SyncFixtures()
        {
            try
            {
                var response = await _httpClient.PostAsync($"{_syncApiUrl}/league/current", null);
                
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to sync fixtures. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private async Task<LeagueFixtureByDate> GetFixturesByDate()
        {
            try
            {
                var request = await _httpClient.GetAsync($"{_fixturesApiUrl}/date");

                if (request.IsSuccessStatusCode)
                {
                    var responseLeaguesString = await request.Content.ReadAsStringAsync();
                    BaseCoreResponseModel<LeagueFixtureByDate> response = JsonSerializer.Deserialize<BaseCoreResponseModel<LeagueFixtureByDate>>(responseLeaguesString);

                    string actualDate = DateTime.UtcNow.ToString("dd/MM/yyyy");
                    var fixturesDate = response.Data.Where(d => d.Date == actualDate).FirstOrDefault();
                    return fixturesDate;
                }
                else
                {
                    var errorMessage = JsonSerializer.Deserialize<object>(await request.Content.ReadAsStringAsync());
                    Console.WriteLine(errorMessage);
                    Console.WriteLine(request.StatusCode);
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        private async Task SendTelegramMessage(List<FixtureDate> fixtures, int type)
        {
            List<string> matches = new List<string>();
            Dictionary<string, string> markets = new Dictionary<string, string>();
            
            foreach (var fixture in fixtures)
            {
                string match = $"{fixture.HomeTeamName} x {fixture.AwayTeamName}";
                matches.Add(match);

                if (type == 4)
                    markets.Add(match, fixture.Filters);
            }

            try
            {
                TelegramRequestModel requestModel = new TelegramRequestModel(type, matches, markets);
                string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var request = await _httpClient.PostAsync($"{_telegramApiUrl}", httpContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private async Task AnalyzeMatches(List<int> list)
        {
            try
            {
                string actualDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
                AnalyzeMatchRequest analyzeRequest = new AnalyzeMatchRequest(list, actualDate);
                string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(analyzeRequest);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                await _httpClient.PostAsync($"{_punterApiUrl}/analyze", httpContent);
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
