using Customer.DTOs;
using System.Net.Http.Json;

namespace Customer.Services
{
    public class CategorySVC(HttpClient httpClient)
    {
        public IEnumerable<CategoryDTO>? Categorys { get; set; } = [];

        private readonly string apiHost = "https://localhost:7032/api/categories";

        public async Task<IEnumerable<CategoryDTO>?> GetAllValidAsync()
        {
            try
            {
                var data = await httpClient.GetFromJsonAsync<IEnumerable<CategoryDTO>>(apiHost+"/available");
                return data;
            }
            catch
            {
                return null;
            }   
        }

        public async Task<CategoryDTO?> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await httpClient.GetFromJsonAsync<CategoryDTO?>(apiHost + "/" + id);
                return data;
            }
            catch
            {
                return null;
            }
        }

    }
}
