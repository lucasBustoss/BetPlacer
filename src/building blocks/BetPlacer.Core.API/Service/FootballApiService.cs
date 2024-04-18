using BetPlacer.Core.API.Models.Response;
using BetPlacer.Core.API.Models.Response.Leagues;
using BetPlacer.Core.API.Models.Response.Teams;
using System.Text.Json;

namespace BetPlacer.Core.API.Service
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

        public async Task<IEnumerable<LeaguesResponseModel>> GetLeagues()
        {
            var request = await _httpClient.GetAsync($"league-list?key={_apiKey}");
            return await TreatApiRequest<LeaguesResponseModel>(request);
        }

        public async Task<IEnumerable<TeamsResponseModel>> GetTeams(int leagueSeasonCode)
        {
            var request = await _httpClient.GetAsync($"league-teams?season_id={leagueSeasonCode}&key={_apiKey}");
            return await TreatApiRequest<TeamsResponseModel>(request);
        }

        #region Private methods
        
        private async Task<IEnumerable<T>> TreatApiRequest<T>(HttpResponseMessage request)
        {
            if (request.IsSuccessStatusCode)
            {
                var responseLeaguesString = await request.Content.ReadAsStringAsync();
                BaseResponse<T> responseLeague = JsonSerializer.Deserialize<BaseResponse<T>>(responseLeaguesString);

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
