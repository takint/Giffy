using System.Data.Entity.ModelConfiguration;

namespace Giffy.Entities.Models.Configurations
{
    public class MenuConfiguration : EntityTypeConfiguration<Menu>
    {
        public MenuConfiguration()
        {
            HasOptional(e => e.Parent)
            .WithMany()
            .HasForeignKey(m => m.ParentId);
        }
    }

}
