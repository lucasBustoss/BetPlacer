using BetPlacer.Core.Messages.PlanningBet.Core.Integration;

namespace BetPlacer.Fixtures.API.Messages
{
    public interface IMessageSender
    {
        void SendMessage<T>(BaseMessage baseMessage, string queueName);
    }
}
