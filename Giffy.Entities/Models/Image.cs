namespace Giffy.Entities.Models
{
    public class Image : Tracking
    {
        public int Id { get; set; }
        public string Name  { get; set; }
        public string Description { get; set; }
		public string Url { get; set; }
        public string CropInfo { get; set; }
        public bool IsActived { get; set; }       
        public ImageType ImageType { get; set; }

        public int? PostId { get; set; }
        public virtual Post Post { get; set; }
    }  
}