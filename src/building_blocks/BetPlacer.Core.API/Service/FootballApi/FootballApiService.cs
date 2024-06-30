using BetPlacer.Core.Models.Response.API;
using BetPlacer.Core.Models.Response.FootballAPI.Fixtures;
using BetPlacer.Core.Models.Response.FootballAPI.Leagues;
using BetPlacer.Core.Models.Response.FootballAPI.Teams;
using System.Text.Json;

namespace BetPlacer.Core.API.Service.FootballApi
{
    public class FootballApiService : IFootballApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly string _apiKey;

        public FootballApiService(IConfiguration configuration)
        {
            _apiUrl = configuration.GetValue<string>("FootballApi:AppUrl");
            _apiKey = configuration.GetValue<string>("FootballApi:AppKey");

            _httpClient = new HttpClient() { BaseAddress = new Uri(_apiUrl) };
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<IEnumerable<LeaguesFootballResponseModel>> GetLeagues()
        {
            IEnumerable<LeaguesFootballResponseModel> leaguesFiltered = new List<LeaguesFootballResponseModel>();

            var request = await _httpClient.GetAsync($"league-list?key={_apiKey}&chosen_leagues_only=true");
            var leagues = await TreatApiRequest<LeaguesFootballResponseModel>(request);

            foreach (var league in leagues)
            {
                var seasons = new List<LeagueSeasonResponseModel>();

                foreach (var season in league.Season)
                {
                    int year = 0;

                    if (season.Year.ToString().Length == 8)
                        year = int.Parse(season.Year.ToString().Substring(0, 4));
                    else
                        year = season.Year;

                    if (year != 0 && year >= 2013)
                        seasons.Add(season);
                }

                league.Season = seasons;
            }

            return leagues.Where(l => l.Season.Count() > 0).ToList();
        }

        public async Task<IEnumerable<TeamsFootballResponseModel>> GetTeams(int leagueSeasonCode)
        {
            var request = await _httpClient.GetAsync($"league-teams?season_id={leagueSeasonCode}&key={_apiKey}");
            return await TreatApiRequest<TeamsFootballResponseModel>(request);
        }

        public async Task<IEnumerable<FixturesFootballResponseModel>> GetCompleteFixtures(int leagueSeasonCode)
        {
            var request = await _httpClient.GetAsync($"league-matches?season_id={leagueSeasonCode}&key={_apiKey}");
            var fixtures = await TreatApiRequest<FixturesFootballResponseModel>(request);

            return fixtures.Where(fixture => fixture.Status == "complete");
        }

        public async Task<IEnumerable<FixturesFootballResponseModel>> GetNextFixtures(int leagueSeasonCode)
        {
            var request = await _httpClient.GetAsync($"league-matches?season_id={leagueSeasonCode}&key={_apiKey}");
            var fixtures = await TreatApiRequest<FixturesFootballResponseModel>(request);

            return fixtures.Where(fixture => fixture.Status == "incomplete" && DateTimeOffset.FromUnixTimeSeconds(fixture.DateTimestamp) > DateTime.Now);
        }

        #region Private methods

        private async Task<IEnumerable<T>> TreatApiRequest<T>(HttpResponseMessage request)
        {
            if (request.IsSuccessStatusCode)
            {
                var responseLeaguesString = await request.Content.ReadAsStringAsync();
                BaseApiResponse<T> responseLeague = JsonSerializer.Deserialize<BaseApiResponse<T>>(responseLeaguesString);

                return responseLeague.Data;
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
