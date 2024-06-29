using BetPlacer.Core.API.Models.Request.PinnacleOdds.Market;
using BetPlacer.Core.API.Models.Request.PinnacleOdds.SpecialMarket;
using BetPlacer.Core.API.Models.Request.PinnacleOdds;
using System.Text.Json;

namespace BetPlacer.Core.API.Service.PinnacleOdds
{
    public class PinnacleOddsService : IPinnacleOddsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly string _apiKey;

        public PinnacleOddsService(IConfiguration configuration)
        {
            var handler = new HttpClientHandler();
            handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;

            _apiUrl = configuration.GetValue<string>("PinnacleOddsApi:AppUrl");
            _apiKey = configuration.GetValue<string>("PinnacleOddsApi:AppKey");

            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(_apiUrl) };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding*", "gzip, deflate, br");
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _apiKey);

        }

        public async Task<List<PinnacleOddsModel>> GetOdds(int leagueCode)
        {
            List<PinnacleOddsModel> pinnacleOdds = new List<PinnacleOddsModel>();

            PinnacleOddsMarketRequest market = await GetMarkets(leagueCode);

            foreach (var match in market.Events)
            {
                PinnacleOddsSpecialMarketRequest specialMarket = await GetSpecialMarkets(match.Code);
                PinnacleOddsMoneyLineRequest moOdds = match.Odds.FTOdds.MoneyLine;
                PinnacleOddsTotalsRequest goalsOdds = match.Odds.FTOdds.Totals;
                PinnacleOddsSpecialMarketMarketsRequest bttsMarket = specialMarket.Markets.Where(m => m.Name == "Both Teams To Score?").FirstOrDefault();

                if (bttsMarket != null)
                {
                    PinnacleOddsSpecialMarketLinesRequest bttsYesMarket = bttsMarket.Lines.Where(l => l.Value.Name == "Yes").Select(l => l.Value).FirstOrDefault();
                    PinnacleOddsSpecialMarketLinesRequest bttsNoMarket = bttsMarket.Lines.Where(l => l.Value.Name == "No").Select(l => l.Value).FirstOrDefault();

                    if (bttsYesMarket != null && bttsNoMarket != null)
                    {
                        PinnacleOddsModel pinnacleOdd = new PinnacleOddsModel(
                            match.Date, 
                            match.LeagueName, 
                            match.HomeTeam, 
                            match.AwayTeam, 
                            moOdds != null ? moOdds.HomeOdd : 0,
                            moOdds != null ? moOdds.DrawOdd : 0,
                            moOdds != null ? moOdds.AwayOdd : 0,
                            goalsOdds != null && goalsOdds.OverUnder25 != null ? goalsOdds.OverUnder25.Over25Odd : 0,
                            goalsOdds != null && goalsOdds.OverUnder25 != null ? goalsOdds.OverUnder25.Under25Odd : 0, 
                            bttsYesMarket.Price, 
                            bttsNoMarket.Price);

                        pinnacleOdds.Add(pinnacleOdd);
                    }
                }
            }

            return pinnacleOdds;
        }

        private async Task<PinnacleOddsMarketRequest> GetMarkets(int leagueCode)
        {
            var request = await _httpClient.GetAsync($"markets?sport_id=1&is_have_odds=true&league_ids={leagueCode}");

            if (request.IsSuccessStatusCode)
            {
                var responseString = await request.Content.ReadAsStringAsync();
                PinnacleOddsMarketRequest response = JsonSerializer.Deserialize<PinnacleOddsMarketRequest>(responseString);

                return response;
            }
            else
            {
                var errorMessage = System.Text.Json.JsonSerializer.Deserialize<object>(await request.Content.ReadAsStringAsync());
                Console.WriteLine(errorMessage);
                Console.WriteLine(request.StatusCode);
                return null;
            }
        }

        public async Task<PinnacleOddsSpecialMarketRequest> GetSpecialMarkets(int eventCode)
        {
            var request = await _httpClient.GetAsync($"special-markets?sport_id=1&is_have_odds=true&event_ids={eventCode}");

            if (request.IsSuccessStatusCode)
            {
                var responseString = await request.Content.ReadAsStringAsync();
                PinnacleOddsSpecialMarketRequest response = JsonSerializer.Deserialize<PinnacleOddsSpecialMarketRequest>(responseString);

                return response;
            }
            else
            {
                var errorMessage = System.Text.Json.JsonSerializer.Deserialize<object>(await request.Content.ReadAsStringAsync());
                Console.WriteLine(errorMessage);
                Console.WriteLine(request.StatusCode);
                return null;
            }
        }
    }
}
