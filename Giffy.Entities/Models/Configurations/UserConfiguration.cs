using System.Data.Entity.ModelConfiguration;

namespace Giffy.Entities.Models.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasMany(u => u.Posts).WithRequired(p => p.CreatedUser).HasForeignKey(p => p.CreatedUserId);
        }
    }

}
