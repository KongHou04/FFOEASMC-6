using System.ComponentModel.DataAnnotations;

namespace Restaurant.DTOs.Auth
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
