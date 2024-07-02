namespace BetPlacer.Fixtures.API.Models.RequestModel.Telegram
{
    public class TelegramRequestModel
    {
        public TelegramRequestModel(int type, List<string> objects)
        {
            Type = type;
            Objects = objects;
        }

        public int Type { get; set; }
        public List<string> Objects { get; set; }
    }
}
