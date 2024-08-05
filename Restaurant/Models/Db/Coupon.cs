using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.Db
{
    [Table("coupons")]
    public class Coupon
    {
        [Key]
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
