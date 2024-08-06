using System.ComponentModel.DataAnnotations;

namespace Admin.DTOs
{
    public class ComboDetailDTO
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; } = 1;


        #region Relationship config
        public Guid ProductId { get; set; }

        #endregion
    }
}
