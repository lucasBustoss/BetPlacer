using Telegram.Bot;

namespace BetPlacer.Telegram.API.Services
{
    public class TelegramService
    {
        string _botToken = "7444696015:AAFCGGzRagDFyaXQ9mByadb9PnRddCVZTz0";
        string _userId = "631174806";

        public void SendMessage(int type, List<string> objectsName)
        {
            foreach (var objectName in objectsName)
            {
                string message = GetFormattedMessage(type, objectName);

                var botClient = new TelegramBotClient(_botToken);

                botClient.SendTextMessageAsync(_userId, message);
            }
        }

        private string GetFormattedMessage(int type, string objectName)
        {
            string message = "";

            switch (type)
            {
                case 1:
                    message = $"Não encontrei a correspondência do time {objectName} (Pinnacle) na FootyStats.";
                    break;
                default:
                    break;
            }

            return message;
        }
    }
}
