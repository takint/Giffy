using System;
using System.Collections.Generic;

namespace Giffy.DataAccess.Models
{
    public class GetLikeDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }
        public string CreatedUserDisplayName { get; set; }
        public string CreatedUserAvatar { get; set; }
    }
}