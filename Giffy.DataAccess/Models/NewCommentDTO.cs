using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Giffy.Entities.Models;

namespace Giffy.DataAccess.Models
{
    public class NewCommentDTO
    {
        public int Id { get; set; }
        [StringLength(2000)]
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public int ActionForId { get; set; }
        public ActionFor ActionFor { get; set; }
        public int Order { get; set; }
    }
}