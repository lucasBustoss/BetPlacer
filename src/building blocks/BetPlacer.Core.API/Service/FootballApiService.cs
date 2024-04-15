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

        public async Task<string> GetLeagues()
        {
            var request = await _httpClient.GetAsync($"/league-list?key={_apiKey}");

            if (request.IsSuccessStatusCode)
            {
                var responseTeamsString = await request.Content.ReadAsStringAsync();
                Console.WriteLine(responseTeamsString);
            }

            return "OK";
        }
    }
}
