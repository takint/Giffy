using System;
using System.Collections.Generic;
using Giffy.Entities.Models;

namespace Giffy.DataAccess.Models
{
    public class NewPostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Avatar { get; set; }
        public PostType PostType { get; set; }
        public bool IsActived { get; set; }
        
        public virtual ICollection<ImageDTO> Images { get; set; }
        public virtual ICollection<VideoDTO> Videos { get; set; }
        public virtual ICollection<TagDTO> Tags { get; set; }
    }
}