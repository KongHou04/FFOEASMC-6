using System.ComponentModel.DataAnnotations;

namespace Restaurant.DTOs
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(450)]
        public string Address { get; set; } = string.Empty;

        [StringLength(10)]
        [RegularExpression(@"[0-9]")]
        public string? Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
