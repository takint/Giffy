using System.Data.Entity.ModelConfiguration;

namespace Giffy.Entities.Models.Configurations
{
    public class ImageConfiguration : EntityTypeConfiguration<Image>
    {
        public ImageConfiguration()
        {
            HasRequired(p => p.Post).WithMany(p => p.Images).HasForeignKey(p => p.PostId).WillCascadeOnDelete(true);
        }
    }

}
