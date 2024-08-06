using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Customer.DTOs.Auth
{
    public class LoginModel
    {
        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;
    }
}
