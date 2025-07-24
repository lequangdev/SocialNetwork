using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Room_chatEntity
    {
        [Key]
        public Guid? room_id { get; set; }
        public string room_name { get; set; } = "NO NAME";
        public string room_private { get; set; } = "1";
        public Guid? room_key { get; set; }
        public string? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public string? modified_by { get; set; }
        public DateTime? modified_at { get; set; }

    }
}
