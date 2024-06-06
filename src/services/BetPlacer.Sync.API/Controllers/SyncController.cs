using BetPlacer.Core.Controllers;
using BetPlacer.Core.Models.Response.Core;
using BetPlacer.Sync.API.Models.Response.Leagues;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace BetPlacer.Sync.API.Controllers
{
    [Route("api/sync")]
    public class SyncController : BaseController
    {
        private readonly HttpClient _httpClient;
        private readonly string _leaguesApiUrl;
        private readonly string _teamsApiUrl;
        private readonly string _fixturesApiUrl;

        public SyncController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(30);
            _leaguesApiUrl = configuration.GetValue<string>("Apis:LeaguesApi");
            _teamsApiUrl = configuration.GetValue<string>("Apis:TeamsApi");
            _fixturesApiUrl = configuration.GetValue<string>("Apis:FixturesApi");
        }

        [HttpPost]
        public async Task<ActionResult> Sync()
        {
            try
            {
                var leagues = await SyncLeagues();

                if (leagues != null && leagues.Count > 0)
                {
                    foreach (var league in leagues)
                    {
                        Stopwatch st = new Stopwatch();
                        
                        Console.WriteLine($"Começando a sincronizar as infos da liga {league.Name}");
                        st.Start();

                        await SyncTeams(league.Seasons, league.Name);
                        await SyncFixtures(league.Seasons, league.Name);
                        
                        st.Stop();
                        double elapsedSeconds = st.Elapsed.TotalSeconds;

                        Console.WriteLine($"Fim do sync das infos da liga {league.Name}");
                        Console.WriteLine($"Tempo decorrido: {elapsedSeconds} segundos");

                        Console.WriteLine($"Começando a calcular stats da liga {league.Name}");
                        st.Start();

                        await CalculateStats(league.Seasons);

                        st.Stop();
                        double elapsedSeconds2 = st.Elapsed.TotalSeconds;
                        Console.WriteLine($"Fim do calculo de stats da liga {league.Name}");
                        Console.WriteLine($"Tempo decorrido: {elapsedSeconds2} segundos");

                    }
                }

                return OkResponse("data synchronized.");
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }

        #region Private methods

        private async Task<List<LeagueSyncResponseModel>> SyncLeagues()
        {
            var requestLeagues = await _httpClient.PostAsync(_leaguesApiUrl, null);

            if (requestLeagues.IsSuccessStatusCode)
            {
                var responseLeaguesString = await requestLeagues.Content.ReadAsStringAsync();
                BaseCoreResponseModel<LeagueSyncResponseModel> response = JsonSerializer.Deserialize<BaseCoreResponseModel<LeagueSyncResponseModel>>(responseLeaguesString);

                return response.Data.ToList();
            }
            else
            {
                var errorMessage = JsonSerializer.Deserialize<object>(await requestLeagues.Content.ReadAsStringAsync());
                Console.WriteLine(errorMessage);
                Console.WriteLine(requestLeagues.StatusCode);
                return null;
            }
        }

        private async Task SyncTeams(List<LeagueSeasonSyncModel> leagueSeasons, string name)
        {
            int currentCount = 0;
            int count = leagueSeasons.Count;

            foreach (var leagueSeason in leagueSeasons)
            {
                currentCount++;
                Console.WriteLine($"Sincronizando times da liga {name}, season: {leagueSeason.Year}. Faltam {count - currentCount} seasons");

                var body = new Dictionary<string, object> { { "leagueSeasonCode", leagueSeason.Code } };
                var requestTeams = await _httpClient.PostAsJsonAsync(_teamsApiUrl, body);

                if (!requestTeams.IsSuccessStatusCode)
                    throw new Exception("error synchronizing teams.");
            }
        }

        private async Task SyncFixtures(List<LeagueSeasonSyncModel> leagueSeasons, string name)
        {
            int currentCount = 0;
            int count = leagueSeasons.Count;

            foreach (var leagueSeason in leagueSeasons)
            {
                currentCount++;
                Console.WriteLine($"Sincronizando os jogos da liga {name}, season: {leagueSeason.Year}. Faltam {count - currentCount} seasons");

                var body = new Dictionary<string, object> { { "leagueSeasonCode", leagueSeason.Code } };
                var requestTeams = await _httpClient.PostAsJsonAsync(_fixturesApiUrl, body);

                if (!requestTeams.IsSuccessStatusCode)
                    throw new Exception("error synchronizing fixtures.");
            }
        }

        private async Task CalculateStats(List<LeagueSeasonSyncModel> leagueSeasons)
        {
            foreach (var leagueSeason in leagueSeasons)
            {
                var body = new Dictionary<string, object> { { "leagueSeasonCode", leagueSeason.Code } };
                var requestTeams = await _httpClient.PostAsJsonAsync($"{_fixturesApiUrl}/stats", body);

                if (!requestTeams.IsSuccessStatusCode)
                    throw new Exception("error calculate fixtures stats.");
            }
        }

        #endregion
    }
}
