using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MasstransitRabitMQ.contract.Abstractions.IntergrationEvents;
using MasstransitRabitMQ.contract.Constants;
using Microsoft.AspNetCore.Mvc;
using static MasstransitRabitMQ.contract.Abstractions.IntergrationEvents.DomainEvent;

namespace Infrastructure
{
    public interface IProducer
    {
        Task PublishSms(SmsNotificationEvent paramSms);
    }
    public class Producer : IProducer
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public Producer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task PublishSms(SmsNotificationEvent paramSms)
        {
                await _publishEndpoint.Publish(paramSms);
        }
    }
}
