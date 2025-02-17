using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using MasstransitRabitMQ.contract.Abstractions.IntergrationEvents;
using MasstransitRabitMQ.contract.Constants;
using MassTransit;
using Microsoft.Extensions.Logging;
using Infrastructure.RabitMq.MessageBus.ConsumerService.Interface;
using Infrastructure.RabitMq.MessageBus.ConsumerService;
using Infrastructure.Serilog;
using Infrastructure.Jwt;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Redis;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IProducer _producer;
        private readonly ISmsService _smsService;
        private readonly ILoggingService _loggingService;
        private readonly IJwtService _jwtService;
        private readonly IResponseCacheService _responseCacheService;

        public AuthController 
        (
            IProducer producer,
            ISmsService smsService,
            ILoggingService loggingService,
            IJwtService jwtService,
            IResponseCacheService responseCacheService
        ) 
        {
            _producer = producer;
            _smsService = smsService;
            _loggingService = loggingService;
            _jwtService = jwtService;
            _responseCacheService = responseCacheService;
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

        [HttpPost("test")]
        public bool TestEvent()
        {
            try
            {
                int x = 0;
                int y = 10/x;
            }
            catch (Exception ex) {
                _loggingService.LogError("co loi", ex);
            }
            return true;
        }

        [HttpPost("test2")]
        public string TestJwt()
        {
            try
            {
                var token = _jwtService.GenerateToken("quang1", "quang", "admin");
                return token;
            }
            catch (Exception ex)
            {
                _loggingService.LogError("co loi", ex);
                return "co loi";
            }
        }
        [Authorize]
        [HttpGet("secure-data")]
        public IActionResult GetSecureData()
        {
            return Ok(new { Message = "Bạn đã xác thực thành công!" });
        }

        [HttpGet("GetAll")]
        [Cache(100)]
        public async Task<IActionResult> Get(string keyword, int pageIndex, int pageSize)
        {
            var result = "quang";
            return Ok(result);
        }

        [HttpGet("Created")]
        [Cache(100)]
        public async Task<IActionResult> Created()
        {
            await _responseCacheService.RemoveCacheResponseAsync("/Auth/GetAll");
            return Ok();
        }



    }

}
