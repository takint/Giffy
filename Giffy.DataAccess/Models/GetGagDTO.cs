using System;
using System.Collections.Generic;
using Giffy.Entities.Models;

namespace Giffy.DataAccess.Models
{
    public class GetGagDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string BackGagSlug { get; set; }
        public string ForwardGagSlug { get; set; }
        public bool IsActived { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }
        public string CreatedUserDisplayName { get; set; }
        public string CreatedUserAvatar { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public bool Liked { get; set; }

        public virtual ICollection<ImageDTO> Images { get; set; }
        public virtual ICollection<VideoDTO> Videos { get; set; }
        public virtual ICollection<TagDTO> Tags { get; set; }
        public virtual ICollection<GetGagDTO> SameTagsGags { get; set; }
        public virtual ICollection<GetGagDTO> SameUserGags { get; set; }
    }
}