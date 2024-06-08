using System.Text.Json.Serialization;

namespace BetPlacer.Backtest.API.Messages
{
    public class EndOfMessagesMessage
    {
        [JsonPropertyName("isFinished")]
        public bool IsFinished { get; set; }
    }
}
