using BetPlacer.Core.API.Service;
using BetPlacer.Core.Controllers;
using BetPlacer.Leagues.API.Repositories;
using BetPlacer.Leagues.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                List<LeagueModel> leaguesToSave = leagues.Select(league => new LeagueModel(league)).ToList();

                _leaguesRepository.CreateOrUpdate(leaguesToSave);

                return OkResponse("Leagues synchronized.");
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }
    }
}
