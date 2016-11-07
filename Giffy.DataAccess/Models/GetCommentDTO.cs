using System;
using System.Collections.Generic;

namespace Giffy.DataAccess.Models
{
    public class GetCommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public int Order { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }
        public string CreatedUserDisplayName { get; set; }
        public string CreatedUserAvatar { get; set; }

        public ICollection<GetCommentDTO> Replies { get; set; }
    }
}