using System.ComponentModel.DataAnnotations;

namespace Customer.Models
{
    public class CartItem
    {
        public Guid ProductId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; } = 1;
    }
}
