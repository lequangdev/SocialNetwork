using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EF_core;
using DataAccessLayer.Interfaces;
using Domain;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class Room_userRepo : BaseRepo<Room_userEntity>, IRoom_userRepo
    {

        public Room_userRepo(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Guid?>> getListRoom_id(Guid? user_id)
        {
            var rooms = await _dbContext.room_user
            .Where(ru => ru.user_id == user_id)
            .Include(ru => ru.room_chat) 
            .Select(ru => ru.room_chat.room_id)
            .ToListAsync();

            return rooms;
        }

        public async Task<List<RoomUserDTO>> GetRoomWithUsersByRoom_id(List<Guid?> room_id)
        {
            var result = new List<RoomUserDTO>();
            foreach (var item in room_id)
            {
                var roomWithUsers = await _dbContext.room_user
               .Where(ru => ru.room_id == item)
               .Include(ru => ru.room_chat)
               .Include(ru => ru.user)
               .ToListAsync();

                if (roomWithUsers.Count == 0)
                    return result;

                var roomDto = new RoomUserDTO
                {
                    room_id = roomWithUsers.First().room_chat.room_id,
                    room_name = roomWithUsers.First().room_chat.room_name,
                    Users = roomWithUsers.Select(ru => new UserDTO
                    {
                        user_id = ru.user.user_id,
                        user_avatar = ru.user.user_avatar,
                        user_fullName = ru.user.user_fullName
                    }).ToList()
                };
                result.Add(roomDto);
            }
            return result;
        }

    }
}
