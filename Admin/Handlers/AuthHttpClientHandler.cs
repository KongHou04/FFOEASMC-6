using System.Net.Http.Headers;
using Admin.Services;

namespace Customer.Handlers
{
    public class AuthHttpClientHandler(LocalStorageSVC localStorageSVC) : DelegatingHandler
    {

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await localStorageSVC.GetItemAsync("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Token now is " + token);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
