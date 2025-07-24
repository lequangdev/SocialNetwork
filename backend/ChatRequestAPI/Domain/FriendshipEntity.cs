using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FriendshipEntity
    {
        [Key]
        public Guid? friendship_id { get; set; }
        public Guid? user_id { get; set; }
        public Guid? friend_id { get; set; }
        public string? status { get; set; }
        public string? created_by { get; set; }
        public string? modified_by { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? modified_at { get; set; } = DateTime.Now;

        [ForeignKey("user_id")]
        public UserEntity? user { get; set; }
        [ForeignKey("friend_id")]
        public UserEntity? friend { get; set; }
    }
}
