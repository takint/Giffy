﻿using System;
using Giffy.Entities.Models.Enums;

namespace Giffy.DataAccess.Models
{
    public class GetUserDTO
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }

        public string Avatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string FacebookAccount { get; set; }
        public string TwitterAccount { get; set; }
        public string GoogleAccount { get; set; }
        public string YahooAccount { get; set; }
        public DateTime JoinedDate { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}