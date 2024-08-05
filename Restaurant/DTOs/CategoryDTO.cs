using System.ComponentModel.DataAnnotations;

namespace Restaurant.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }

        [Required]
        public bool IsAvailable { get; set; } = true;
    }
}
