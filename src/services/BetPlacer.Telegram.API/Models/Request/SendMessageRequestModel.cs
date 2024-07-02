using static BetPlacer.Telegram.API.Enums.TelegramEnums;

namespace BetPlacer.Telegram.API.Models.Request
{
    public class SendMessageRequestModel
    {
        public SendMessageType Type { get; set; }
        public List<string> Objects { get; set; }
    }
}
