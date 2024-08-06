namespace Customer.DTOs.Statistic
{
    public class OrderStatistics
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int TotalOrders { get; set; }
        public int DeliveriedOrders { get; set; }
        public int PaidOrders { get; set; }
        public int CanceledOrders { get; set; }
        public int CompletedOrders {  get; set; }
        public decimal SumSubTotal { get; set; }
        public decimal SumDiscount { get; set; }
        public decimal SumTotal { get; set; }
    }
}
