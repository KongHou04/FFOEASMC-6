using Customer.Handlers;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Customer.Services
{
    public class VnPayService
    {
        private const string TmnCode = "Z64B4JPJ";
        private const string HashSecret = "U8N6XZRZTURQIUCX75PAHKNPO4Y2VOAV";
        private const string BaseUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        private const string Version = "2.1.0";
        private const string Locale = "vn";
        private const string Command = "pay";
        private const string CurrCode = "USD";
        private const string ReturnUrl = "http://localhost:5272/PaymentReturn";

        private readonly HttpClient _httpClient;

        public VnPayService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string CreatePaymentUrl(VnPaymentRequestModel model, string ipAddress)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", Version);
            vnpay.AddRequestData("vnp_Command", Command);
            vnpay.AddRequestData("vnp_TmnCode", TmnCode);
            vnpay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", model.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", CurrCode);
            vnpay.AddRequestData("vnp_IpAddr", ipAddress);
            vnpay.AddRequestData("vnp_Locale", Locale);
            vnpay.AddRequestData("vnp_OrderInfo", "Payment for Order: " + model.OrderID);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", model.OrderID.ToString());

            // Tạo URL thanh toán
            var paymentUrl = vnpay.CreateRequestUrl(BaseUrl, HashSecret);
            return paymentUrl;
        }

        public static class Utils
        {
            public static string HmacSHA512(string key, string inputData)
            {
                var keyBytes = Encoding.UTF8.GetBytes(key);
                var inputBytes = Encoding.UTF8.GetBytes(inputData);
                using var hmac = new HMACSHA512(keyBytes);
                var hashValue = hmac.ComputeHash(inputBytes);
                return string.Concat(hashValue.Select(b => b.ToString("x2")));
            }
        }
    }

    public class VnPaymentRequestModel
    {
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid OrderID { get; set; }
    }

    public class VnPaymentResponseModel
    {
        public bool Success { get; set; }
        public string PaymentMethod { get; set; }
        public Guid OrderId { get; set; }
        public string OrderDescription { get; set; }
        public string TransactionId { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
    }
}
