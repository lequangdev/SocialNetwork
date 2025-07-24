using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RoomUserDTO
    {
        public Guid? room_id { get; set; }
        public string? room_name { get; set; }
        public List<UserDTO>? Users { get; set; }
    }
}
