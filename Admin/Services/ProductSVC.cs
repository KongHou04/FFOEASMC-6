using Admin.DTOs;

namespace Admin.Services
{
    public class ProductSVC
    {
        private readonly HttpClient _httpClient;
        private readonly string apiHost = "https://localhost:7032/api/products";

        public ProductSVC(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDTO>?> GetAllValidAsync()
        {
            try
            {
                var data = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDTO>>(apiHost + "/available");
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
                var data = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDTO>>(apiHost + $"/randomavailable?number={number}");
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
                var data = await _httpClient.GetFromJsonAsync<ProductDTO?>(apiHost + "/" + id);
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

        public async Task<IEnumerable<ProductDTO>?> GetAll()
        {
            try
            {
                var data = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDTO>>(apiHost);
                return data;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddProductAsync(ProductDTO product)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(apiHost, product);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateProductAsync(Guid id, ProductDTO product)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{apiHost}/{id}", product);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{apiHost}/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
