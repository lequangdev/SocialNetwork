using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DTO;

namespace ServiceLayer.Interfaces
{
    public interface IMessageService : IBaseService<MessageEntity>
    {
        Task<bool> SendMessageAsync(MessageDTO messager);
        Task<Guid?> CrreateRoomTwoAsync(ConnectRoomTwoDTO user);
        Task<Guid?> CheckedRoomUserAsync(ConnectRoomTwoDTO user);
        Task<List<MessageEntity>> getMessageByRoomId(Guid room_id);

    }
}
