using BetPlacer.Core.Models.Response.API;
using BetPlacer.Core.Models.Response.FootballAPI.Fixtures;
using BetPlacer.Core.Models.Response.FootballAPI.Leagues;
using BetPlacer.Core.Models.Response.FootballAPI.Teams;
using System.Text.Json;

namespace BetPlacer.Core.API.Service.NewFootballApi
{
    public class NewFootballApiService : INewFootballApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly string _apiKey;

        public NewFootballApiService(IConfiguration configuration)
        {
            _apiUrl = configuration.GetValue<string>("NewFootballApi:AppUrl");
            _apiKey = configuration.GetValue<string>("NewFootballApi:AppKey");

            _httpClient = new HttpClient() { BaseAddress = new Uri(_apiUrl) };
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", _apiKey);
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
        }

        public async Task<IEnumerable<LeaguesFootballResponseModel>> GetLeagues()
        {
            IEnumerable<LeaguesFootballResponseModel> leaguesFiltered = new List<LeaguesFootballResponseModel>();

            var request = await _httpClient.GetAsync($"/leagues");
            var leagues = await TreatApiRequest<LeaguesFootballResponseModel>(request);

            foreach (var league in leagues)
            {
                var seasons = new List<LeagueSeasonResponseModel>();

                foreach (var season in league.Seasons)
                {
                    int year = 0;

                    string startYear = season.Start.Substring(0, 4);
                    string endYear = season.End.Substring(0, 4);

                    if (startYear != endYear)
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

        public Task<IEnumerable<TeamsFootballResponseModel>> GetTeams()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FixturesFootballResponseModel>> GetCompleteFixtures()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FixturesFootballResponseModel>> GetNextFixtures()
        {
            throw new NotImplementedException();
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
