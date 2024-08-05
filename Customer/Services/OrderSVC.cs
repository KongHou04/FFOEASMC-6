using Customer.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace Customer.Services
{
    public class OrderSVC(HttpClient httpClient)
    {
        private readonly string apiHost = "https://localhost:7032/api/orders";

        public async Task<OrderDTO?> AddAsync(OrderDTO orderDTO)
        {
            try
            {
                orderDTO.OrderTime = DateTime.Now;
                foreach (var od in orderDTO.OrderDetails)
                {
                    od.Product = null;
                }
                var response = await httpClient.PostAsJsonAsync(apiHost+"?orderdetailcheckurl=https://localhost:7169/orderdetails", orderDTO);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<OrderDTO?>();
                    return data;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}, {error}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<OrderDTO?> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await httpClient.GetFromJsonAsync<OrderDTO?>(apiHost + $"/{id}");
                return data;
            }
            catch
            {
                return null;
            }
        }

        public string GetDeliveryStatus(int statusNumber)
        {
            if (statusNumber == 0)
                return "In Queue";
            if (statusNumber == 1)
                return "In Transmit";
            return "Delivered";
        }

        public string GetPaymentStatus(int statusNumber)
        {
            if (statusNumber == 0)
                return "Unpaid";
            return "Paid";
        }
    }
}
