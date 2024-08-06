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
        public async Task<IEnumerable<CategoryDTO>?> GetAll()
        {
            try
            {
                var data = await httpClient.GetFromJsonAsync<IEnumerable<CategoryDTO>>(apiHost);
                return data;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                bool data = await httpClient.DeleteFromJsonAsync<bool>(apiHost+"/"+id);
                return data;
            }
            catch
            {
                return false;
            }
        }
        public async Task<CategoryDTO?> Create(CategoryDTO category)
        {
            try
            {
                category.Id = Guid.NewGuid();
                var response = await httpClient.PostAsJsonAsync(apiHost, category);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CategoryDTO>();
                }
                else
                {
                    Console.WriteLine($"HTTP Error: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

    }
}
