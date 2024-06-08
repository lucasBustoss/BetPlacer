using BetPlacer.Core.Messages.PlanningBet.Core.Integration;

namespace BetPlacer.Fixtures.API.Messages.ModelToMessage
{
    public class EndOfMessagesMessage : BaseMessage
    {
        public EndOfMessagesMessage()
        {
            IsFinished = true;
        }

        public bool IsFinished { get; set; }
    }
}
