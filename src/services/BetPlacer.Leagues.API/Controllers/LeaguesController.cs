using BetPlacer.Core.API.Service;
using BetPlacer.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using BetPlacer.Leagues.API.Repositories;

namespace BetPlacer.Leagues.Controllers
{
    [Route("api/leagues")]
    public class LeaguesController : BaseController
    {
        private readonly IFootballApiService _footballApiServices;
        private readonly LeaguesRepository _leaguesRepository;

        public LeaguesController(IFootballApiService footballApiServices, LeaguesRepository leaguesRepository)
        {
            _footballApiServices = footballApiServices;
            _leaguesRepository = leaguesRepository;
        }

        [HttpGet]
        public ActionResult GetLeagues()
        {

            return OkResponse("Deu certo!");
        }

        [HttpPost]
        public async Task<ActionResult> SyncLeagues()
        {
            try
            {
                var leagues = await _footballApiServices.GetLeagues();

                await _leaguesRepository.CreateOrUpdate(leagues);

                return OkResponse("Leagues synchronized.");
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }
    }
}
