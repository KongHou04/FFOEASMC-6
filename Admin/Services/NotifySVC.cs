using Microsoft.JSInterop;
using System.Globalization;

namespace Admin.Services
{
    public class NotifySVC(IJSRuntime jsRuntime)
    {
        public async Task Notify(string msg)
        {
            try
            {
                await jsRuntime.InvokeVoidAsync("showNotification", msg);

            }
            catch
            {
                Console.Write("Cannot send notification");
                return;
            }
        }
    }
}
