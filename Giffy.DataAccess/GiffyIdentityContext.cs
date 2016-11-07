using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Giffy.Entities;
using Giffy.Entities.Models;
using Giffy.Entities.Models.Configurations;

namespace Giffy.DataAccess
{
    public class GiffyIdentityContext : IdentityDbContext<User>
    {
        public GiffyIdentityContext()
            : base("GiffyContext", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Video> Videos { get; set; }

        public static GiffyIdentityContext Create()
        {
            return new GiffyIdentityContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            modelBuilder.Ignore<IdentityUser>();
            modelBuilder.Entity<User>().ToTable("Users", "dbo").HasKey(p => p.Id);
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles", "dbo");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins", "dbo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims", "dbo").HasKey(p => p.Id);
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "dbo").HasKey(p => p.Id);

            modelBuilder.Configurations.Add(new MenuConfiguration());
            modelBuilder.Configurations.Add(new ImageConfiguration());
            modelBuilder.Configurations.Add(new VideoConfiguration());
        }
    }
}
