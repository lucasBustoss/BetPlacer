using BetPlacer.Core.API.Service;
using BetPlacer.Core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetPlacer.Leagues.Controllers
{
    [Route("api/leagues")]
    public class LeaguesController : BaseController
    {
        private readonly IFootballApiService _footballApiServices;

        public LeaguesController(IFootballApiService footballApiServices)
        {
            _footballApiServices = footballApiServices;
        }

        [HttpGet]
        public ActionResult GetLeagues()
        {
            _footballApiServices.GetLeagues();
            return OkResponse("Deu certo!");
        }
    }
}
