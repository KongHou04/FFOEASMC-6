using Restaurant.DTOs;
using Restaurant.DTOs.Statistic;

namespace Restaurant.Services.Interfaces
{
    public interface IStatisticSVC
    {
        public OrderStatistics GetMonthlyOrderStatistics(int year, int month);
        public IEnumerable<ProductStatistics> GetMonthlyProductStatistics(int year, int month);
        public CustomerStatistics GetMonthlyCustomerStatistics(int year, int month);
        public IEnumerable<CategoryStatistics> GetMonthlyCategoryStatistics(int year, int month);
    }
}
