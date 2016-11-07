using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giffy.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string Text { get; set; }
        [StringLength(200)]
        public string Icon { get; set; }
        [StringLength(200)]
        public string Link { get; set; }
        public int Order { get; set; }
        public bool IsActived { get; set; }

        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public Menu Parent { get; set; }
    }
}