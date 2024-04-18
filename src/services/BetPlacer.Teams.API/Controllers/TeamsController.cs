using BetPlacer.Core.API.Service;
using BetPlacer.Core.Controllers;
using BetPlacer.Teams.API.Controllers.RequestModel;
using BetPlacer.Teams.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BetPlacer.Teams.API.Controllers
{
    [Route("api/teams")]
    public class TeamsController : BaseController
    {
        private readonly IFootballApiService _footballApiServices;
        private readonly TeamsRepository _teamsRepository;

        public TeamsController(IFootballApiService footballApiServices, TeamsRepository teamsRepository)
        {
            _footballApiServices = footballApiServices;
            _teamsRepository = teamsRepository;
        }

        [HttpGet]
        public ActionResult GetTeams()
        {
            return OkResponse("Deu certo");
        }

        [HttpPost]
        public async Task<ActionResult> SyncTeams([FromBody] TeamsSyncRequestModel syncRequestModel)
        {
            try
            {
                if (syncRequestModel == null)
                    throw new Exception();
                
                var teams = await _footballApiServices.GetTeams(syncRequestModel.LeagueSeasonCode);

                await _teamsRepository.Create(teams);

                return OkResponse("Teams synchronized.");
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }
    }
}
