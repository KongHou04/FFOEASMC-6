namespace Admin.DTOs.Statistic
{
    public class ProductStatistics
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public Guid? ProductId { get; set; }
        public int UnitsSold { get; set; }
        public decimal RevenueGenerated { get; set; }
    }
}
