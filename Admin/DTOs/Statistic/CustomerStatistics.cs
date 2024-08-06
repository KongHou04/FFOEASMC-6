namespace Customer.DTOs.Statistic
{
    public class CustomerStatistics
    {
        public int Year { set; get; }
        public int Month { set; get; }
        public int NewCustomerCount {  get; set; }
        public int ReturningCustomerCount { get; set; }
        public int TotalCustomerCount { get; set; }
    }
}
