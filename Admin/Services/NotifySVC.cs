using Microsoft.JSInterop;
using System.Globalization;

namespace Customer.Services
{
    public class NotifySVC(IJSRuntime jsRuntime)
    {
        public async Task Notify(string msg)
        {
            await jsRuntime.InvokeVoidAsync("showNotification", msg);
        }
    }
}
