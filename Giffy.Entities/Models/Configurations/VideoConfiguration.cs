using System.Data.Entity.ModelConfiguration;

namespace Giffy.Entities.Models.Configurations
{
    public class VideoConfiguration : EntityTypeConfiguration<Video>
    {
        public VideoConfiguration()
        {
            HasRequired(p => p.Post).WithMany(p => p.Videos).HasForeignKey(p => p.PostId).WillCascadeOnDelete(true);
        }
    }

}
