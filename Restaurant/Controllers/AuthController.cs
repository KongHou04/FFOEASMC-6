using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Restaurant.DTOs;
using Restaurant.DTOs.Auth;
using Restaurant.Helpers;
using Restaurant.Models.Db;
using Restaurant.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController
        (IConfiguration configuration, EmailSender emailSender, 
        UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
        RoleManager<IdentityRole> roleManager) : ControllerBase
    {
        [HttpPost("register/customer")]
        public async Task<IActionResult> RegisterAsCustomer([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new AppUser { UserName = model.Email, Email = model.Email };
            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest("Cannot create new account!");

            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole("Customer"));
                if (!roleResult.Succeeded)
                    return BadRequest("Failed to create Customer role");
            }

            var roleAssignResult = await userManager.AddToRoleAsync(user, "Customer");
            if (!roleAssignResult.Succeeded)
                return BadRequest("Failed to assign role to user");

            try
            {
                await emailSender.SendEmailAsync(user.Email, "Register successfully", "Now you can login into our system by using your email address and password.");
            }
            catch
            {
                return Ok("Account created successfully but cannot send notification to your email");
            }

            return Ok();
        }

        [HttpPost("register/staff/{isAdminUser}")]
        public async Task<IActionResult> RegisterAsStaff([FromBody] RegisterModel model, [FromRoute] bool isAdminUser = false)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new AppUser { UserName = model.Email, Email = model.Email };
            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest("Cannot create new account!");

            string role = isAdminUser ? "Admin" : "Staff";
            if (!await roleManager.RoleExistsAsync(role))
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                if (!roleResult.Succeeded)
                    return BadRequest("Failed to create staff role");
            }

            var roleAssignResult = await userManager.AddToRoleAsync(user, role);
            if (!roleAssignResult.Succeeded)
                return BadRequest("Failed to assign role to user");

            try
            {
                await emailSender.SendEmailAsync(user.Email, "Register successfully", "Now you can login into our system by using your email address and password.");
            }
            catch
            {
                return Ok("Account created successfully but cannot send notification to your email");
            }

            return Ok();
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromRoute] string email, [FromQuery] string resetPasswordClientUiUrl)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required.");

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("User not found.");

            // Generate password reset token
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            // URL encode the token and email
            var encodedToken = WebUtility.UrlEncode(token);
            var encodedEmail = WebUtility.UrlEncode(email);

            // Build password reset link with client UI URL
            var resetLink = $"{resetPasswordClientUiUrl}?email={encodedEmail}&token={encodedToken}";

            try
            {
                await emailSender.SendEmailAsync(email, "Reset your password",
                    $"<div>You can change your password by clicking <a href=\"{resetLink}\">here</a></div>");
                return Ok("Password reset link has been sent to your email.");
            }
            catch
            {
                return BadRequest("Cannot send link to your email");
            }
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("User not found.");

            var result = await userManager.ResetPasswordAsync(user, model.Token!, model.NewPassword!);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Password has been reset successfully.");
        }

        [HttpPost("access")]
        public async Task<IActionResult> GetAccessToken([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await userManager.FindByNameAsync(loginModel.Username!);
            if (user == null)
                return BadRequest();

            var result = await signInManager.CheckPasswordSignInAsync(user, loginModel.Password!, false);

            if (result.IsNotAllowed)
                return BadRequest("Not allowed");

            if (result.IsLockedOut)
                return BadRequest("Account is locked out");

            if (!result.Succeeded)
                return BadRequest("Errors occured");

            // Generate jwt
            var token = await GenerateToken(user);

            // Create refresh token
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.Now.AddHours(16);
            await userManager.UpdateAsync(user);

            return Ok(new TokenModel
            {
                Jwt = token,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshJwt([FromBody] TokenModel tokenModel)
        {
            if (string.IsNullOrWhiteSpace(tokenModel.Jwt) || string.IsNullOrWhiteSpace(tokenModel.RefreshToken))
                return BadRequest();

            var principal = GetPrincipalFromExpiredToken(tokenModel.Jwt);
            if (principal == null)
                return BadRequest();

            var userId = principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return BadRequest();

            var user = await userManager.FindByIdAsync(userId);
            if (user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpiry <= DateTime.Now || await userManager.IsLockedOutAsync(user))
                return BadRequest();

            var newAccessToken = await GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.Now.AddHours(16);
            await userManager.UpdateAsync(user);

            return Ok(new TokenModel(newAccessToken, newRefreshToken));
        }


        private async Task<string> GenerateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(15),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return principal;
        }

        [HttpGet("email")]
        public async Task<string?> GetEmail()
        {
            var userObj = HttpContext.Items["User"] as AppUser;
            if (userObj == null)
                return null;
            var existingUser = await userManager.FindByIdAsync(userObj.Id);
            return existingUser?.Email ?? null;
        }
    }
}
