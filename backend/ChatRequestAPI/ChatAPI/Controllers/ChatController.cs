using Infrastructure;
using MasstransitRabitMQ.contract.Abstractions.IntergrationEvents;
using MasstransitRabitMQ.contract.Constants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatRequestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly Producer _producer;

        public ChatController(Producer producer)
        {
            _producer = producer;
        }
        [HttpPost(Name = "publish-sms-notification")]
        public async Task<ActionResult> publishSmsNotificationEvent()
        {
            var smsEvent = new DomainEvent.SmsNotificationEvent()
            {
                Id = Guid.NewGuid(),
                Description = "Sms description",
                Name = "Sms notification",
                TimeStamp = DateTime.Now,
                TransactionId = Guid.NewGuid(),
                Type = NotificationType.sms
            };
            await _producer.PublishSms(smsEvent);
            return Accepted();
        }

    }

}