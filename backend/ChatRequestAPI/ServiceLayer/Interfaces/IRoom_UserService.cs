using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DTO;

namespace ServiceLayer.Interfaces
{
    public interface IRoom_UserService : IBaseService<Room_userEntity>
    {
        Task<List<Guid?>> getListRoom_id(Guid? user_id);
        Task<List<RoomUserDTO>> GetRoomWithUsersByRoom_id(List<Guid?> room_id);
    }
}
