using System;
using System.Collections.Generic;

namespace Giffy.Entities.Models
{
    public class League : Tracking
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string FullName { get; set; } 
        public string ShortName { get; set; }
        public string NickName { get; set; } 
        public string Logo { get; set; }
        public string Avatar { get; set; }
        public string FunnyAvatar  { get; set; }
        public string LeagueImage { get;set;}
        public string Description { get; set; }
        public DateTime? FoundedDate {get;set;}
        public string History{get;set;}
        public bool IsActived { get; set; }
        public DateTime? JoinedDate  { get; set; }
        public short Level { get; set; }
        public bool Popular { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<User> Fans { get; set; }
    }
}