using Admin.DTOs;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.IO;
using System.Net.Http.Headers;

namespace Admin.Services
{
    public class ProductSVC
    {
        private readonly HttpClient _httpClient;
        private readonly int maxFileSize = 1024 * 1024 * 5;
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

        public async Task<bool> AddProductAsync(ProductDTO product, IBrowserFile? selectedFile)
        {
            using var content = new MultipartFormDataContent();
            if (selectedFile != null)
            {
                byte[] bytes;
                var stream = selectedFile.OpenReadStream(maxAllowedSize: maxFileSize);
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    bytes  = memoryStream.ToArray();
                }
                content.Add(new StreamContent(new MemoryStream(bytes)), "ImgFile", selectedFile.Name);
            }
            content.Add(new StringContent(product.Name), "Name");
            content.Add(new StringContent(product.UnitPrice.ToString()), "UnitPrice");
            content.Add(new StringContent(product.PercentDiscount.ToString()), "PercentDiscount");
            content.Add(new StringContent(product.HardDiscount.ToString()), "HardDiscount");
            content.Add(new StringContent(product.IsAvailable.ToString()), "IsAvailable");
            content.Add(new StringContent(product.CategoryId.ToString()), "CategoryId");

            content.Add(new StringContent("conghau"), "msg");
            try
            {
                var response = await _httpClient.PostAsync(apiHost, content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var msg = $"Error: {response.StatusCode}, Details: {errorContent}";
                    return false;
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateProductAsync(ProductDTO product, IBrowserFile? selectedFile)
        {
            using var content = new MultipartFormDataContent();
            if (selectedFile != null)
            {
                byte[] bytes;
                var stream = selectedFile.OpenReadStream(maxAllowedSize: maxFileSize);
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    bytes = memoryStream.ToArray();
                }
                content.Add(new StreamContent(new MemoryStream(bytes)), "ImgFile", selectedFile.Name);
            }
            content.Add(new StringContent(product.Name), "Name");
            content.Add(new StringContent(product.UnitPrice.ToString()), "UnitPrice");
            content.Add(new StringContent(product.PercentDiscount.ToString()), "PercentDiscount");
            content.Add(new StringContent(product.HardDiscount.ToString()), "HardDiscount");
            content.Add(new StringContent(product.IsAvailable.ToString()), "IsAvailable");
            content.Add(new StringContent(product.CategoryId.ToString()), "CategoryId");

            content.Add(new StringContent("conghau"), "msg");
            try
            {
                var response = await _httpClient.PutAsync(apiHost+$"/{product.Id}", content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var msg = $"Error: {response.StatusCode}, Details: {errorContent}";
                    return false;
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
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
