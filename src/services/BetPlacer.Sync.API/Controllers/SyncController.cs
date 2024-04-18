using BetPlacer.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BetPlacer.Sync.API.Controllers
{
    [Route("api/sync")]
    public class SyncController : BaseController
    {
        [HttpPost]
        public ActionResult Sync()
        {
            return OkResponse("Deu certo!");
        }
    }
}
