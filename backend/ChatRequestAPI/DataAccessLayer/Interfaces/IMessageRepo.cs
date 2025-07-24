using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DTO;

namespace DataAccessLayer.Interfaces
{
    public interface IMessageRepo : IBaseRepo<MessageEntity>
    {
        Task<bool> SendMessageAsync(MessageEntity message);
        Task<Guid?> CheckedRoomUserAsync(ConnectRoomTwoDTO user);
        Task<List<MessageEntity>> getMessageByRoomId(Guid room_id);

    }
}
