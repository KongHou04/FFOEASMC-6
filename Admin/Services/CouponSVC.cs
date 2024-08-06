using Admin.DTOs;
using System.Net.Http.Json;

namespace Admin.Services
{
    public class CouponSVC(HttpClient httpClient)
    {
        private readonly string apiHost = "https://localhost:7032/api/coupontypes";

        public async Task<CouponTypeDTO?> GetAvailableByCoupon(Guid id)
        {
            try
            {
                var data = await httpClient.GetFromJsonAsync<CouponTypeDTO?>(apiHost + $"/availablebycoupon/{id}");
                return data;
            }
            catch
            {
                return null;
            }
        }
    }
}
