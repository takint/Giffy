using Giffy.Entities.Models;

namespace Giffy.DataAccess.Models
{
    public class ImageDTO : Tracking
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string CropInfo { get; set; }
        public bool IsActived { get; set; }
        public ImageType ImageType { get; set; }
    }
}
