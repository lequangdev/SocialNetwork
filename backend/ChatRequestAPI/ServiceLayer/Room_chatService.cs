using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EF_core;
using DataAccessLayer.Interfaces;
using Domain;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class Room_chatService : BaseService<Room_chatEntity>, IRoom_chatService
    {
        private readonly IRoom_chatRepo _room_chatRepo;
        public Room_chatService(IRoom_chatRepo room_chatRepo, AppDbContext dbContext) : base(room_chatRepo, dbContext)
        {
            _room_chatRepo = room_chatRepo;
        }


    }
}
