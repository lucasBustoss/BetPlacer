using BetPlacer.Core.Controllers;
using BetPlacer.Punter.API.Models;
using BetPlacer.Punter.API.Models.Entities;
using BetPlacer.Punter.API.Models.Request;
using BetPlacer.Punter.API.Models.ValueObjects.Strategy;
using BetPlacer.Punter.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BetPlacer.Punter.API.Controllers
{
    [Route("api/punter")]
    public class PunterController : BaseController
    {
        private readonly PunterRepository _punterRepository;
        private readonly BacktestService _backtestService;

        public PunterController(PunterRepository punterRepository)
        {
            _punterRepository = punterRepository;
            _backtestService = new BacktestService();
        }

        [HttpPost]
        public async Task<ActionResult> CreateBacktest([FromBody] AnalyzeLeagueRequest analyzeLeagueRequest)
        {
            List<MatchBaseData> info = await _punterRepository.GetMatchBaseDataAsync(analyzeLeagueRequest.LeagueCode);

            List<StrategyInfo> strategies = _backtestService.CalculateStats(info);
            await _punterRepository.Create(analyzeLeagueRequest.LeagueCode, strategies);
            //TelegramMessage.SendMessage();

            return OkResponse(strategies);
        }

        [HttpPost("analyze")]
        public async Task<ActionResult> AnalyzeNextMatches([FromBody] AnalyzeMatchRequest analyzeMatchRequest)
        {
            if (analyzeMatchRequest != null && analyzeMatchRequest.LeagueCodes.Count > 0)
            {
                foreach (int leagueCode in analyzeMatchRequest.LeagueCodes)
                {
                    var backtest = _punterRepository.GetBacktestsByLeague(leagueCode);

                    if (backtest == null || backtest.Count == 0)
                        continue;

                    List<MatchBaseData> lastMatches = await _punterRepository.GetLastMatches(leagueCode);
                    List<NextMatch> nextMatches = await _punterRepository.GetNextMatches(analyzeMatchRequest.Date, leagueCode);

                    List<FixtureStrategyModel> fixtureStrategies = _backtestService.FilterMatches(backtest, lastMatches, nextMatches);

                    foreach (var nextMatch in nextMatches)
                    {
                        var existentAnalysis = fixtureStrategies.Where(f => f.FixtureCode == nextMatch.MatchCode).FirstOrDefault();

                        if (existentAnalysis == null)
                            fixtureStrategies.Add(new FixtureStrategyModel(nextMatch.MatchCode, null));
                    }

                    if (fixtureStrategies.Count > 0)
                        _punterRepository.SaveMatchAnalysis(fixtureStrategies);
                }
            }

            return OkResponse("matches analyzed.");
        }

        [HttpPost("filter/active")]
        public ActionResult ActiveFilter([FromBody] ActiveFilterRequest activeFilterRequest)
        {
            _punterRepository.ActiveFilter(activeFilterRequest.StrategyCode, activeFilterRequest.FilterCode);

            return OkResponse("filter active.");
        }

        [HttpGet]
        public ActionResult GetBacktestByLeague(int leagueCode)
        {
            var backtest = _punterRepository.GetBacktestsByLeague(leagueCode);
            return OkResponse(backtest);
        }

        [HttpGet("fixtures")]
        public ActionResult GetFixtureStrategies()
        {
            var backtest = _punterRepository.GetFixturesStrategy();
            return OkResponse(backtest);
        }
    }
}
