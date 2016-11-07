using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Giffy.Entities.Models.Enums;

namespace Giffy.Entities.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Level { get; set; }
        public string Avatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public bool IsActived { get; set; }
        public string FacebookAccount { get; set; }
        public string TwitterAccount { get; set; }
        public string GoogleAccount { get; set; }
        public string YahooAccount { get; set; }
        public DateTime JoinedDate { get; set; }
        public DateTime? LastLogin { get; set; }

        public ICollection<League> FavouriteLeagues { get; set; }
        public ICollection<Team> FavouriteTeams { get; set; }
        public ICollection<Player> FavouritePlayers { get; set; }
        public ICollection<Post> Posts { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            ClaimsIdentity userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            
            // Add custom user claims here
            return userIdentity;
        }
    }
}
