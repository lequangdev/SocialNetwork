using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MessageDTO
    {
        public string? message_content { get; set; }
        public string? message_type { get; set; }
        public Guid? user_id { get; set; }
        public string? user_fullName { get; set; }
        public Guid? room_id { get; set; }
    }
}
