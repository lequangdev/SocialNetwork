using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserEntity
    {
        public Guid? user_id {  get; set; }
        public string? user_fullName { get; set; }
        public string? phone_number { get; set; }
        public string? user_account { get; set; }
        public string? user_passwork { get; set; }
        public string? user_avatar { get; set; }
        public string? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string? modified_by { get; set; }
        public DateTime? modified_date { get; set; }

    }
}
