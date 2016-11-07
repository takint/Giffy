using System.Collections.Generic;

namespace Giffy.Entities.Models
{
    public class Tag : Entity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string NickName { get; set; }
        public string Slug { get; set; }
        public string Avatar { get; set; }
        public TagType TagType { get; set; }
        public int SearchCount { get; set; }
        public short Level { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}