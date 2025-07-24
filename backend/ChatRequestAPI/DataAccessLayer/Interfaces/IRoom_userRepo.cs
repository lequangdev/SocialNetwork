using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DTO;

namespace DataAccessLayer.Interfaces
{
    public interface IRoom_userRepo : IBaseRepo<Room_userEntity>
    {
        Task<List<Guid?>> getListRoom_id(Guid? user_id);
        Task<List<RoomUserDTO>> GetRoomWithUsersByRoom_id(List<Guid?> payload);
    }
    
}
