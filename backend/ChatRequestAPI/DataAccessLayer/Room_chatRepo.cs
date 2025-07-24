using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EF_core;
using DataAccessLayer.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class Room_chatRepo : BaseRepo<Room_chatEntity>, IRoom_chatRepo
    {


        public Room_chatRepo(AppDbContext dbContext) : base(dbContext) {

        }
        public async Task<bool> InsertRoom_chat(Room_chatEntity room_chat)
        {
            await _dbContext.room_chat.AddRangeAsync(room_chat);
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }

    }
}
