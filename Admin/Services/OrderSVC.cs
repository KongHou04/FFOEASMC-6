using Admin.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace Admin.Services
{
    public class OrderSVC
    {
        private readonly HttpClient httpClient;
        private readonly string apiHost = "https://localhost:7032/api/orders";

        public OrderSVC(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<OrderDTO?> AddAsync(OrderDTO orderDTO)
        {


            try
            {
                orderDTO.OrderTime = DateTime.Now;
                foreach (var od in orderDTO.OrderDetails)
                {
                    od.Product = null;
                }
                var response = await httpClient.PostAsJsonAsync(apiHost + "?orderdetailcheckurl=https://localhost:7169/orderdetails", orderDTO);
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
            return statusNumber switch
            {
                0 => "In Queue",
                1 => "In Transmit",
                _ => "Delivered"
            };
        }

        public string GetPaymentStatus(int statusNumber)
        {
            return statusNumber switch
            {
                0 => "Unpaid",
                _ => "Paid"
            };
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrder()
        {
            try
            {
                var data = await httpClient.GetFromJsonAsync<IEnumerable<OrderDTO>>(apiHost + "/getallorder");
                return data;
            }
            catch
            {
                return Enumerable.Empty<OrderDTO>();
            }
        }

        public async Task<bool> UpdateFullStatus(Guid id, int paymentStatus, int deliveryStatus)
        {
            try
            {

                var paymentResult = await UpdatePaymentStatus(id, paymentStatus);


                var deliveryResult = await UpdateDeliveryStatus(id, deliveryStatus);

                return paymentResult && deliveryResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdatePaymentStatus(Guid id, int paymentStatus)
        {
            try
            {
                var response = await httpClient.PutAsync($"https://localhost:7032/api/orders/pay/{id}",
                    new StringContent(paymentStatus.ToString(), Encoding.UTF8, "application/json"));
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateDeliveryStatus(Guid id, int deliveryStatus)
        {
            try
            {
                var response = await httpClient.PutAsync($"https://localhost:7032/api/orders/updatedeliverystatus/{id}?status={deliveryStatus}",
                    new StringContent(string.Empty, Encoding.UTF8, "application/json"));
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

    }
}
