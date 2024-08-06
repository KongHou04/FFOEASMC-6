using Admin.DTOs.Auth;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;

namespace Admin.Services
{
    public class AuthSVC(HttpClient httpClient, LocalStorageSVC localStorageSVC)
    {
        private readonly string apiHost = "https://localhost:7032/api/auth";

        public string? Email { get; set; } = null;

        public async Task<TokenModel?> LoginAsync(LoginModel loginModel)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(apiHost + "/access", loginModel);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<TokenModel>();
                if (result != null)
                {
                    await localStorageSVC.SetItemAsync("auth", result.Jwt!);
                    await localStorageSVC.SetItemAsync("username", loginModel.Username);
                } 
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> RegisterAsync(RegisterModel registerModel)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(apiHost + "/register/customer", registerModel);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string?> GetAccountEmail()
        {
            try
            {
                var response = await httpClient.GetStringAsync(apiHost + "/email");
                return response;
            }
            catch
            {
                return null;
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await localStorageSVC.RemoveItemAsync("auth");
                await localStorageSVC.RemoveItemAsync("username");
            }
            catch
            {
            }
        }
    }
}
