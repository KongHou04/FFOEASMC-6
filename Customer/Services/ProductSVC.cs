using Customer.DTOs;
using System.Net.Http.Json;

namespace Customer.Services
{
    public class ProductSVC(HttpClient httpClient)
    {
        private readonly string apiHost = "https://localhost:7032/api/products";

        public async Task<IEnumerable<ProductDTO>?> GetAllValidAsync()
        {
            try
            {
                var data = await httpClient.GetFromJsonAsync<IEnumerable<ProductDTO>>(apiHost + "/available");
                return data;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ProductDTO>?> GetRandomAvailableAsync(int number = 6)
        {
            try
            {
                var data = await httpClient.GetFromJsonAsync<IEnumerable<ProductDTO>>(apiHost + $"/randomavailable?number={number}");
                return data?.ToList();
            }
            catch
            {
                return null;
            }
        }

        public async Task<ProductDTO?> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await httpClient.GetFromJsonAsync<ProductDTO?>(apiHost + "/" + id);
                return data;
            }
            catch
            {
                return null;
            }
        }

        public decimal? CaculteSellingUnitPrice(ProductDTO product)
        {
            var percent = (100 - product.PercentDiscount) / 100m;
            decimal sellingUnitPrice = (percent * product.UnitPrice) - product.HardDiscount;
            return sellingUnitPrice < 0 ? 0 : sellingUnitPrice;
        }
    
    
    
    
    }
}
