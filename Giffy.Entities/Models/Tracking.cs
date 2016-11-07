using System;

namespace Giffy.Entities.Models
{
    public class Tracking : Entity
    {
        public string CreatedUserId { get; set; }
        public virtual User CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedUserId { get; set; }
        public virtual User UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
