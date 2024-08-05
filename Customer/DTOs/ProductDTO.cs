using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Customer.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0, 1000000)]
        public decimal UnitPrice { get; set; } = 0;

        [Required]
        [Range(0, 100)]
        public int PercentDiscount { get; set; } = 0;

        [Required]
        [Range(0, 1000000)]
        public decimal HardDiscount { get; set; } = 0;

        [Required]
        public string Image { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }

        [Required]
        public bool IsAvailable { get; set; } = true;


        #region Relationship config
        public Guid CategoryId { get; set; }

        public ICollection<ComboDetailDTO> ComboDetails { get; set; } = [];

        #endregion
    }
}
