using BetPlacer.Core.Controllers;
using BetPlacer.Telegram.API.Models.Request;
using BetPlacer.Telegram.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BetPlacer.Telegram.API.Controllers
{
    [Route("api/telegram")]
    public class TelegramController : BaseController
    {
        private readonly TelegramService _telegramService;

        public TelegramController()
        {
            _telegramService = new TelegramService();
        }

        [HttpPost]
        public ActionResult SendMessageTeamNotFound([FromBody] SendMessageRequestModel sendMessageRequestModel)
        {
            try
            {
                _telegramService.SendMessage((int)sendMessageRequestModel.Type, sendMessageRequestModel.Objects, sendMessageRequestModel.Markets);
                return OkResponse("OK!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequestResponse(ex.Message);
            }
        }
    }
}
