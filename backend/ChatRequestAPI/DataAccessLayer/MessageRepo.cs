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
    public class MessageRepo : BaseRepo<MessageEntity>, IMessageRepo
    {

        public MessageRepo(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> SendMessageAsync(MessageEntity message)
        {
            _dbContext.Message.Add(message);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Guid?> CheckedRoomUserAsync(ConnectRoomTwoDTO user)
        {
            var roomId = await _dbContext.room_user
            .Where(ru => ru.user_id == user.user_id || ru.user_id == user.receiver_id)
            .GroupBy(ru => ru.room_id)
            .Where(g => g.Select(x => x.user_id).Distinct().Count() == 2)
            .Select(g => (Guid?)g.Key)
            .FirstOrDefaultAsync();
            return roomId;
        }

        public async Task<List<MessageEntity>> getMessageByRoomId(Guid room_id)
        {
            return await _dbContext.Message
            .Where(m => m.room_id == room_id)
            .OrderBy(m => m.created_at) 
            .ToListAsync();
        }

    }
}
