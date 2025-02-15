using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.RabitMq.MessageBus.ConsumerService.Interface;
using static MasstransitRabitMQ.contract.Abstractions.IntergrationEvents.DomainEvent;

namespace Infrastructure.RabitMq.MessageBus.ConsumerService
{
    public class SmsService : ISmsService
    {
        string ISmsService.SendSmsService(string message)
        {
            return message;
        }
    }
}
