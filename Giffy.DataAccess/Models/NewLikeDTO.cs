using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Giffy.Entities.Models;

namespace Giffy.DataAccess.Models
{
    public class NewLikeDTO
    {
        public int Id { get; set; }
        public int ActionForId { get; set; }
        public ActionFor ActionFor { get; set; }
    }
}