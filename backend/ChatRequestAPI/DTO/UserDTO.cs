using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserDTO
    {
        public Guid? user_id { get; set;}
        public string? user_fullName { get; set;}
        public string? user_avatar { get; set;}
    }
}
