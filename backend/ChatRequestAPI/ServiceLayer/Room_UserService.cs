using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EF_core;
using DataAccessLayer.Interfaces;
using Domain;
using DTO;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class Room_UserService : BaseService<Room_userEntity>, IRoom_UserService
    {
        private readonly IRoom_userRepo _room_UserRepo;
        public Room_UserService(IRoom_userRepo room_UserRepo, AppDbContext dbContext) : base(room_UserRepo, dbContext)
        {
            _room_UserRepo = room_UserRepo;
        }

        public async Task<List<Guid?>> getListRoom_id(Guid? user_id)
        {
           return await _room_UserRepo.getListRoom_id(user_id);
        }

        public async Task<List<RoomUserDTO>> GetRoomWithUsersByRoom_id(List<Guid?> room_id)
        {
            return await _room_UserRepo.GetRoomWithUsersByRoom_id(room_id);
        }

    }
}
