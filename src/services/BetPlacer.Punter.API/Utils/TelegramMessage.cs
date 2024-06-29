using Telegram.Bot;

namespace BetPlacer.Punter.API.Utils
{
    public static class TelegramMessage
    {
        public static void SendMessage()
        {
            string botToken = "7444696015:AAFCGGzRagDFyaXQ9mByadb9PnRddCVZTz0";

            string chatId = "631174806";

            string message = "Terminei o processamento!";

            var botClient = new TelegramBotClient(botToken);

            botClient.SendTextMessageAsync(chatId, message);
        }
    }
}
