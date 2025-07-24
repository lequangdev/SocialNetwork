using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserEntity
    {
        [Key]
        public Guid? user_id {  get; set; }
        public string? user_fullName { get; set; }
        public string? phone_number { get; set; }
        public string? user_account { get; set; }
        public string? user_password { get; set; }
        public string? user_avatar { get; set; }
        public string? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public string? modified_by { get; set; }
        public DateTime? modified_at { get; set; }

        [ForeignKey("room_user_id")]
        public ICollection<Room_userEntity>? Room_user { get; set; }

    }
}
