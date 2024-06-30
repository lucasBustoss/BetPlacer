using BetPlacer.Core.Messages.PlanningBet.Core.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetPlacer.Core.Interfaces
{
    public interface IMessageBus
    {
        Task PublicMessage(BaseMessage message, string queueName);
    }
}
