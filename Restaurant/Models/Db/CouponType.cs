using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models.Db
{
    [Table("coupontypes")]
    public class CouponType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;   

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [Range(0, 100)]
        public int PercentValue { get; set; } = 5;

        [Required]
        [Range(0, 1000000)]
        public decimal HardValue { get; set; } = 0;

        [Required]
        [Range(0, 1000000)]
        public decimal MinOrderSubTotalCondition { get; set; } = 0;

        #region  Relationship config
        public ICollection<Coupon> Coupons { get; set; } = [];

        #endregion
    }
}
