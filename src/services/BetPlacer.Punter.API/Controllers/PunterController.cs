using BetPlacer.Core.Controllers;
using BetPlacer.Punter.API.Models;
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
        public async Task<ActionResult> CreateBacktest(int leagueCode)
        {
            List<MatchBaseData> info = await _punterRepository.GetMatchBaseDataAsync(leagueCode);

            List<StrategyInfo> strategies = _backtestService.CalculateStats(info);
            _punterRepository.Create(leagueCode, strategies);
            //TelegramMessage.SendMessage();

            return OkResponse(strategies);
        }

        [HttpPost("analyze")]
        public async Task<ActionResult> AnalyzeNextMatches([FromBody] AnalyzeMatchRequest analyzeMatchRequest)
        {
            var backtest = _punterRepository.GetBacktestsByLeague(analyzeMatchRequest.LeagueCode);

            if (backtest == null)
                return BadRequestResponse("dont exists a backtest active in this league.");

            List<MatchBaseData> lastMatches = await _punterRepository.GetLastMatches(analyzeMatchRequest.LeagueCode);
            List<NextMatch> nextMatches = await _punterRepository.GetNextMatches(analyzeMatchRequest.Date, analyzeMatchRequest.LeagueCode);

            _backtestService.FilterMatches(backtest, lastMatches, nextMatches);

            return OkResponse("matches analyzed.");
        }

        [HttpGet]
        public ActionResult GetBacktestByLeague(int leagueCode)
        {
            var backtest = _punterRepository.GetBacktestsByLeague(leagueCode);
            return OkResponse(backtest);
        }
    }
}
