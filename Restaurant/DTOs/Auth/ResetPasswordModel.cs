using System.ComponentModel.DataAnnotations;

namespace Restaurant.DTOs.Auth
{
    public class ResetPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;

    }
}
