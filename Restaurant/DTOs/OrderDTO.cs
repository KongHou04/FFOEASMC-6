using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime OrderTime { get; set; }

        [Required]
        [StringLength(450)]
        public string Address { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Range(0, 1000000)]
        public decimal SubTotal { get; set; } = 0;

        [Required]
        [Range(0, 1000000)]
        public decimal Discount { get; set; } = 0;

        [Required]
        [Range(0, 1000000)]
        public decimal Total { get; set; } = 0;

        [StringLength(255)]
        public string? Note { get; set; }

        [Required]
        [Range(0, 2)]
        public int DeliveryStatus { get; set; } = 0;

        [Required]
        [Range(0, 1)]
        public int PaymentStatus { get; set; } = 0;

        [Required]
        [Column(TypeName = "Bit")]
        public bool IsCanceled { get; set; } = false;


        #region  Relationship config
        public ICollection<OrderDetailDTO> OrderDetails { get; set; } = [];

        public Guid? CustomerId { get; set; }

        public Guid? CouponId { get; set; }

        #endregion
    }
}
