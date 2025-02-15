using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.RabitMq.MessageBus.ConsumerService.Interface;
using MassTransit;
using MasstransitRabitMQ.contract.Abstractions.Messages;

namespace Infrastructure
{
    public abstract class Consumer<TMessage> : IConsumer<TMessage>
        where TMessage : class, INotificationEvent
    {
        private readonly ISmsService _smsService;

        protected Consumer(ISmsService smsService)
        {
            _smsService = smsService;
        }
        public async Task Consume(ConsumeContext<TMessage> context)
        {
            _smsService.SendSmsService(context.Message.ToString());
        }
    }
}
