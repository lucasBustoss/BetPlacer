using BetPlacer.Core.API.Models.Response.Teams;
using BetPlacer.Core.API.Service;
using BetPlacer.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BetPlacer.Core.API.Controllers
{
    [Route("api/core")]
    public class CoreController : BaseController
    {
        private readonly IFootballApiService _footballApiService;

        public CoreController(IFootballApiService footballApiService)
        {
            _footballApiService = footballApiService;
        }

        [HttpGet("leagues")]
        public async Task<ActionResult> GetLeaguesFromApi()
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
        public async Task<ActionResult> GetTeamsFromApi([FromQuery] int leagueSeasonCode)
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
    }
}
