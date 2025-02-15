using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.RabitMq.MessageBus.ConsumerService.Interface;
using MasstransitRabitMQ.contract.Abstractions.IntergrationEvents;

namespace Infrastructure.RabitMq.MessageBus.Events
{
    public class SendSmsWhenReceivedEventSmsConsumer : Consumer<DomainEvent.SmsNotificationEvent>
    {
        public SendSmsWhenReceivedEventSmsConsumer(ISmsService smsService) : base(smsService)
        {

        }
    }
}
