using Giffy.Entities.Models;

namespace Giffy.DataAccess.Models
{
    public class TagDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Slug { get; set; }
        public string Avatar { get; set; }
        public int SearchCount { get; set; }
        public int PostCount { get; set; }
        public short Level { get; set; }
    }
}
