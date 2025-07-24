using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MessageEntity
    {
        [Key]
        public Guid? message_id { get; set; }
        public string? message_content { get; set; }
        public string? message_type { get; set; }
        public Guid? room_id { get; set; }
        public Guid? user_id { get; set; }
        public int? message_status { get; set; }
        public string? created_by { get; set; }
        public string? modified_by { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? modified_at { get; set; } = DateTime.Now;

    }
}
