using Domain;
using DTO;
using HostBase.Controller;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class FriendshipController : BaseApi<FriendshipEntity>
    {
        private readonly IFriendshipService _friendshipService;
        public FriendshipController(IFriendshipService friendshipService): base(friendshipService)
        {
            _friendshipService = friendshipService;
        }
        [HttpGet("GetFriendInvitationByUser_id")]
        public async Task<IActionResult> GetFriendInvitationByUser_id([FromHeader] Guid id)
        {
            try
            {
                var result = await _friendshipService.GetFriendInvitationByUser_id(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpGet("GetFriendByUser_id")]
        public async Task<IActionResult> GetFriendByUser_id([FromHeader] Guid id)
        {
            try
            {
                var result = await _friendshipService.GetFriendByUser_id(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPut("updateFriend")]
        public async Task<IActionResult> updateFriend([FromBody] FriendDTO friend)
        {
            try
            {
                var result = await _friendshipService.AcceptFriendship(friend);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

    }
}
