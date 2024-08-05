using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.DTOs
{
    public class CouponDTO
    {
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "Bit")]
        public bool IsUsed { get; set; } = false;


        #region Relationship config
        public int CouponTypeId { get; set; }

        public Guid? CustomerId { get; set; }

        #endregion
    }
}
