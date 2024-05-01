using BetPlacer.Core.Controllers;
using BetPlacer.Core.Models.Response.Core;
using BetPlacer.Sync.API.Models.Response.Leagues;
using Microsoft.AspNetCore.Mvc;
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
                        // O trecho abaixo verifica se o nome é Premier League
                        // Porque é a unica liga que funciona com a chave de teste
                        if (league.Name != "England Premier League")
                            continue;

                        await Task.WhenAll(
                            Task.Run(() => SyncTeams(league.Seasons)),
                            Task.Run(() => SyncFixtures(league.Seasons))
                        );
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

        private async void SyncTeams(List<LeagueSeasonSyncModel> leagueSeasons)
        {
            foreach (var leagueSeason in leagueSeasons)
            {
                // Verificação dos leagueSeasonCode que não estão presentes na key de teste
                if (leagueSeason.Code != 1625 && leagueSeason.Code != 2012 && leagueSeason.Code != 4759 && leagueSeason.Code != 9660)
                    continue;

                var body = new Dictionary<string, object> { { "leagueSeasonCode", leagueSeason.Code } };
                var requestTeams = await _httpClient.PostAsJsonAsync(_teamsApiUrl, body);

                if (!requestTeams.IsSuccessStatusCode)
                    throw new Exception("error synchronizing teams.");
            }
        }

        private async Task SyncFixtures(List<LeagueSeasonSyncModel> leagueSeasons)
        {
            foreach (var leagueSeason in leagueSeasons)
            {
                // Verificação dos leagueSeasonCode que não estão presentes na key de teste
                if (leagueSeason.Code != 1625 && leagueSeason.Code != 2012 && leagueSeason.Code != 4759 && leagueSeason.Code != 9660)
                    continue;

                var body = new Dictionary<string, object> { { "leagueSeasonCode", leagueSeason.Code } };
                var requestTeams = await _httpClient.PostAsJsonAsync(_fixturesApiUrl, body);

                if (!requestTeams.IsSuccessStatusCode)
                    throw new Exception("error synchronizing complete fixtures.");
            }
        }

        #endregion
    }
}
