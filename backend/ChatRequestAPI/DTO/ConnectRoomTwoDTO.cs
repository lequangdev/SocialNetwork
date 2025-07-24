using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ConnectRoomTwoDTO
    {
        public Guid? user_id {  get; set; }
        public Guid? receiver_id { get; set; }
    }
}
