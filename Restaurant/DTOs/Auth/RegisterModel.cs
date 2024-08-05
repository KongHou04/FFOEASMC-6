using System.ComponentModel.DataAnnotations;

namespace Restaurant.DTOs.Auth
{
    public class RegisterModel
    {
        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
