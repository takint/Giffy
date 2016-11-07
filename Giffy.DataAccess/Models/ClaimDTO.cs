using System.ComponentModel.DataAnnotations;

namespace Giffy.DataAccess.Models
{
    public class ClaimDTO
    {
        [Required]
        [Display(Name = "Claim Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Claim Value")]
        public string Value { get; set; }
    }
}