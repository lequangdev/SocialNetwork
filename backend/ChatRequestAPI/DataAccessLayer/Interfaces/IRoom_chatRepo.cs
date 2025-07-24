using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DataAccessLayer.Interfaces
{
    public interface IRoom_chatRepo : IBaseRepo<Room_chatEntity>
    {
        Task<bool> InsertRoom_chat(Room_chatEntity Room_chat);
    }
}
