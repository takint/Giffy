﻿using System;
using System.Collections.Generic;
using Giffy.Entities.Models;

namespace Giffy.DataAccess.Models
{
    public class GetPlayerDTO
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        public string FunnyAvatar { get; set; }
        public string Description { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string History { get; set; }
        public decimal TransferPrice { get; set; }
        public bool IsActived { get; set; }
        public DateTime? JoinedDate { get; set; }
        public short Level { get; set; }
        public bool Popular { get; set; }
        public string CreatedUserId { get; set; }
        public virtual User CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedUserId { get; set; }
        public virtual User UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}