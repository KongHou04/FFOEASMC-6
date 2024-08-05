using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models.Db
{
    [Table("combodetails")]
    public class ComboDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; } = 1;


        #region Relationship config
        public Guid ComboId { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        #endregion
    }
}
