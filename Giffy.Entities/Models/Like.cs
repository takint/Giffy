using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Giffy.Entities.Models
{
    public class Like : Tracking
    {
        public int Id { get; set; }

        public int? PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
