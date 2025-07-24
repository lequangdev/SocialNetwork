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
using HostBase.Controller;
using ServiceLayer.Interfaces;
using Domain;
using DTO;
using System.Collections.Generic;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthUserController : BaseApi<UserEntity>
    {
        private readonly IProducer _producer;
        private readonly ISmsService _smsService;
        private readonly ILoggingService _loggingService;
        private readonly IJwtService _jwtService;
        private readonly IResponseCacheService _responseCacheService;
        private readonly IUserService _UserService;

        public AuthUserController 
        (
            IProducer producer,
            ISmsService smsService,
            ILoggingService loggingService,
            IJwtService jwtService,
            IResponseCacheService responseCacheService,
            IUserService UserService
        ) : base(UserService)
        {
            _producer = producer;
            _smsService = smsService;
            _loggingService = loggingService;
            _jwtService = jwtService;
            _responseCacheService = responseCacheService;
            _UserService = UserService;
        }

        [HttpPost("InsertUser")]
        public async Task<IActionResult> InsertUser([FromBody] UserEntity user)
        {
            try
            {
                bool result = await _UserService.InsertUser(user);
                if (result)
                {
                    return Ok(new { message = "User inserted successfully" });
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to insert user" });
                }
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
                
            }

        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO request)
        {
            try
            {
                UserEntity result = await _UserService.LoginUser(request.user_account!, request.user_password!);
                if (result != null)
                {
                    
                    var token = _jwtService.GenerateUserToken(result.user_id);
                    var response = new
                    {
                        Token = token,
                        User = result 
                    };
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500, new { message = "Tài khoản hoặc mất khẩu không chính xác" });
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Internal server error", error = ex.Message });

            }

        }

        [HttpGet("GetUserByFullname")]
        public async Task<IActionResult> GetUserByFullname([FromHeader] string payload)
        {
            try
            {
                var result = await _UserService.GetUserByFullname(payload);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });

            }

        }

        



    }
}
