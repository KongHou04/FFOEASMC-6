namespace Admin.DTOs.Statistic
{
    public class CategoryStatistics
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public Guid? CategoryId { get; set; }
        public int UnitsSold { get; set; }
        public decimal RevenueGenerated { get; set; }
    }
}
