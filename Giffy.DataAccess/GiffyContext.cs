using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Giffy.DataAccess.Infrastructure;
using Giffy.Entities;
using Giffy.Entities.Models;
using Giffy.Entities.Models.Configurations;

namespace Giffy.DataAccess
{
    public class GiffyContext : DataContext
    {
        static GiffyContext()
        {
            Database.SetInitializer<GiffyContext>(null);
        }
        public GiffyContext() : base("Name=GiffyContext") { }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!
            
            modelBuilder.Ignore<IdentityUserRole>();
            modelBuilder.Ignore<IdentityUserLogin>();
            modelBuilder.Ignore<IdentityUserClaim>();
            modelBuilder.Ignore<IdentityRole>();
            modelBuilder.Ignore<IdentityUser>();
        }
    }
}
