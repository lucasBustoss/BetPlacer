using BetPlacer.Core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetPlacer.Leagues.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : BaseController
    {
        [HttpGet]
        public ActionResult GetLeagues()
        {
            return OkResponse("Deu certo!");
        }
    }
}
