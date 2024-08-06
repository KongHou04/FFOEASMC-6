using System.ComponentModel.DataAnnotations;

namespace Customer.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [Required]
        public bool IsAvailable { get; set; } = true;
    }
}
