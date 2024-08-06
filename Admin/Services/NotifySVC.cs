using Microsoft.JSInterop;
using System.Globalization;

namespace Admin.Services
{
    public class NotifySVC(IJSRuntime jsRuntime)
    {
        public async Task Notify(string msg)
        {
            await jsRuntime.InvokeVoidAsync("showNotification", msg);
        }
    }
}
