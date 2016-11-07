using Giffy.Entities.Models;

namespace Giffy.DataAccess.Models
{
    public class VideoDTO : Tracking
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Picture { get; set; }
        public bool IsActived { get; set; }
        public VideoType VideoType { get; set; }
    }
}
