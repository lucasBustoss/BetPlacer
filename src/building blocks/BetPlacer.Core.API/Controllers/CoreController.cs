using BetPlacer.Core.API.Models.Request.PinnacleOdds;
using BetPlacer.Core.API.Service.FootballApi;
using BetPlacer.Core.API.Service.PinnacleOdds;
using BetPlacer.Core.API.Utils;
using BetPlacer.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BetPlacer.Core.API.Controllers
{
    [Route("api/core")]
    public class CoreController : BaseController
    {
        private readonly IFootballApiService _footballApiService;
        private readonly IPinnacleOddsService _pinnacleOddsService;

        public CoreController(IFootballApiService footballApiService, IPinnacleOddsService pinnacleOddsService)
        {
            _footballApiService = footballApiService;
            _pinnacleOddsService = pinnacleOddsService;
        }

        [HttpGet("leagues")]
        public async Task<ActionResult> GetLeagues()
        {
            try
            {
                var leagues = await _footballApiService.GetLeagues();
                return OkResponse(leagues);
            }
            catch (HttpRequestException ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpGet("teams")]
        public async Task<ActionResult> GetTeams([FromQuery] int leagueSeasonCode)
        {
            try
            {
                if (leagueSeasonCode == 0)
                    throw new HttpRequestException("leagueSeasonCode param is required");

                var teams = await _footballApiService.GetTeams(leagueSeasonCode);
                return OkResponse(teams);
            }
            catch (HttpRequestException ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpGet("fixtures/complete")]
        public async Task<ActionResult> GetCompleteFixtures([FromQuery] int leagueSeasonCode)
        {
            try
            {
                if (leagueSeasonCode == 0)
                    throw new HttpRequestException("leagueSeasonCode param is required");

                var fixtures = await _footballApiService.GetCompleteFixtures(leagueSeasonCode);
                return OkResponse(fixtures);
            }
            catch (HttpRequestException ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpGet("fixtures/next")]
        public async Task<ActionResult> GetNextFixtures([FromQuery] int leagueSeasonCode)
        {
            try
            {
                if (leagueSeasonCode == 0)
                    throw new HttpRequestException("leagueSeasonCode param is required");

                var fixtures = await _footballApiService.GetNextFixtures(leagueSeasonCode);
                return OkResponse(fixtures);
            }
            catch (HttpRequestException ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpGet("pinnacle")]
        public async Task<ActionResult> GetPinnacleOdds([FromQuery]int leagueCode)
        {
            try
            {
                int pinnacleLeagueCode = PinnacleUtils.GetPinnacleLeagueCode(leagueCode);

                if (pinnacleLeagueCode == 0)
                    return BadRequestResponse("league code not recognized. Check the correspondency to Pinnacle Code");
                
                List<PinnacleOddsModel> pinnacleOdds = await _pinnacleOddsService.GetOdds(pinnacleLeagueCode);
                return OkResponse(pinnacleOdds);
            }
            catch (HttpRequestException ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }
    }
}
