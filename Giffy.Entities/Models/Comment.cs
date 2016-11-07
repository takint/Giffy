using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Giffy.Entities.Models
{
    public class Comment : Tracking
    {
        public int Id { get; set; }
        [StringLength(2000)]
        public string Content { get; set; }
   		public string ImagePath { get; set; }
        public int Order { get; set; }

        public int? PostId { get; set; }
        public virtual Post Post { get; set; }
        public ICollection<Comment> Replies { get; set; }
    }
}
