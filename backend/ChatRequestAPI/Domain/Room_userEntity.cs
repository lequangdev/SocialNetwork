using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Room_userEntity
    {
        [Key]
        public Guid? room_user_id { get; set; }
        public Guid? room_id  { get; set;}
        public Guid? user_id {  get; set; }
        public string? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public string? modified_by { get; set; }
        public DateTime? modified_at { get; set; }

        [ForeignKey("room_id")]
        public Room_chatEntity? room_chat { get; set; }
        [ForeignKey("user_id")]
        public UserEntity? user { get; set; }        
    }
}

