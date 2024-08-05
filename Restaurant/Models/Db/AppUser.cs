
using Microsoft.AspNetCore.Identity;

namespace Restaurant.Models.Db
{
    public class AppUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; } 
    }
}
