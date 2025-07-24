using Domain;
using HostBase.Controller;
using Infrastructure;
using Infrastructure.Jwt;
using Infrastructure.RabitMq.MessageBus.ConsumerService.Interface;
using Infrastructure.Redis;
using Infrastructure.Serilog;
using MasstransitRabitMQ.contract.Abstractions.IntergrationEvents;
using MasstransitRabitMQ.contract.Constants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceLayer.Interfaces;
using DTO;
using ChatAPI;
using Microsoft.AspNetCore.SignalR;
using ServiceLayer;

namespace ChatRequestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : BaseApi<MessageEntity>
    {
        private readonly IProducer _producer;
        private readonly ISmsService _smsService;
        private readonly ILoggingService _loggingService;
        private readonly IJwtService _jwtService;
        private readonly IResponseCacheService _responseCacheService;
        private readonly IMessageService _messageService;
        private readonly IRoom_UserService _room_userService;
        private readonly IHubContext<MessageHub> _hubContext;
        public ChatController
        (
            IProducer producer, ISmsService smsService,
            ILoggingService loggingService,
            IJwtService jwtService,
            IResponseCacheService responseCacheService,
            IMessageService messageService,
            IRoom_UserService room_userService,
            IHubContext<MessageHub> hubContext

        ) : base(messageService)
        {
            _producer = producer;
            _smsService = smsService;
            _jwtService = jwtService;
            _loggingService = loggingService;
            _messageService = messageService;
            _responseCacheService = responseCacheService;
            _room_userService = room_userService;
            _hubContext = hubContext;

        }

        [HttpPost("RoomUser")]
        public async Task<IActionResult> CheckedRoomUserAsync([FromBody] ConnectRoomTwoDTO user)
        {
            try
            {
                var result = await _messageService.CheckedRoomUserAsync(user);
                if(result == null)
                {
                    return Ok(0);
                }
                else

                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message }); 
            }
        }


        [HttpGet("GetMessage")]
        public async Task<IActionResult> getMessageByRoomId([FromHeader]  Guid room_id)
        {
            try
            {
                var result = await _messageService.getMessageByRoomId(room_id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message }); 
            }
        }

        [HttpGet("GetRoomByUser_id")]
        public async Task<IActionResult> getListRoom_id([FromHeader] Guid? user_id)
        {
            try
            {
                var result = await _room_userService.getListRoom_id(user_id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPost("GetRoomWithUsersByRoom_id")]
        public async Task<ActionResult<RoomUserDTO>> GetRoomWithUsersByRoom_id([FromBody]List<Guid?> room_id)
        {
            try
            {
               var result = await _room_userService.GetRoomWithUsersByRoom_id(room_id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

    }

}