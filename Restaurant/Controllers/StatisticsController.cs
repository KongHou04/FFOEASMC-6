using Microsoft.AspNetCore.Mvc;
using Restaurant.DTOs;
using Restaurant.DTOs.Statistic;
using Restaurant.Services.Interfaces;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController(IStatisticSVC statisticSVC) : ControllerBase
    {
        [HttpGet("monthlyorder")]
        public OrderStatistics GetMonthlyOrderStatistics([FromQuery] int year, [FromQuery] int month)
        {
            return statisticSVC.GetMonthlyOrderStatistics(year, month);
        }

        [HttpGet("monthlyproduct")]
        public IEnumerable<ProductStatistics> GetMonthlyProductStatistics([FromQuery] int year, [FromQuery] int month)
        {
            return statisticSVC.GetMonthlyProductStatistics(year, month);
        }

        [HttpGet("monthlycategory")]
        public IEnumerable<CategoryStatistics> GetMonthlyCategoryStatistics([FromQuery] int year, [FromQuery] int month)
        {
            return statisticSVC.GetMonthlyCategoryStatistics(year, month);
        }

        [HttpGet("monthlycustomer")]
        public CustomerStatistics GetMonthlyCustomerStatistics([FromQuery] int year, [FromQuery] int month)
        {
            return statisticSVC.GetMonthlyCustomerStatistics(year, month);
        }
    }
}
