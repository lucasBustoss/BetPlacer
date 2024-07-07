using Telegram.Bot;

namespace BetPlacer.Telegram.API.Services
{
    public class TelegramService
    {
        string _botToken = "7444696015:AAFCGGzRagDFyaXQ9mByadb9PnRddCVZTz0";
        string _userId = "631174806";

        public void SendMessage(int type, List<string> objectsName, Dictionary<string, string> fixtureMarket)
        {
            foreach (var objectName in objectsName)
            {
                string market = "";

                if (type == 4)
                    market = fixtureMarket[objectName];

                string message = GetFormattedMessage(type, objectName, market);

                var botClient = new TelegramBotClient(_botToken);

                botClient.SendTextMessageAsync(_userId, message);
            }
        }

        private string GetFormattedMessage(int type, string objectName, string market = "")
        {
            string message = "";

            switch (type)
            {
                case 1:
                    message = $"Não encontrei a correspondência do time {objectName} (Pinnacle) na FootyStats.";
                    break;
                case 2:
                    message = $"O jogo {objectName} não possui todas as odds informadas.";
                    break;
                case 3:
                    message = $"O jogo {objectName} precisa ser analisado.";
                    break;
                case 4:
                    message = $"Alerta de aposta para o jogo {objectName}. Mercado(s): {market}";
                    break;
                default:
                    break;
            }

            return message;
        }
    }
}
