using System;
using System.Collections.Generic;

namespace Giffy.Entities.Models
{
    public class Post : Tracking
    {
        public Post()
        {
            Images = new List<Image>();
            Videos = new List<Video>();
        }
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Slug { get; set; }  
        public string Description { get; set; }
        public string Content { get; set; }
        public string Avatar { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool IsActived { get; set; }
        public PostType PostType { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}
