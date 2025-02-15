using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasstransitRabitMQ.contract.Abstractions.Messages;
using static MasstransitRabitMQ.contract.Abstractions.IntergrationEvents.DomainEvent;

namespace Infrastructure.RabitMq.MessageBus.ConsumerService.Interface
{
    public interface ISmsService
    {
        string SendSmsService(string message);
    }
}
