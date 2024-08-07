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
        /// <summary>
        /// Get OrderStatistic by year, month.
        /// </summary>
        /// <param name="month">Month</param>
        /// <param name="year">Year</param>
        ///<returns>
        /// List the orderstatistic.
        ///</returns>
        [HttpGet("monthlyorder")]
        public OrderStatistics GetMonthlyOrderStatistics([FromQuery] int year, [FromQuery] int month)
        {
            return statisticSVC.GetMonthlyOrderStatistics(year, month);
        }
        /// <summary>
        /// Get ProductStatistic by year, month.
        /// </summary>
        /// <param name="month">Month</param>
        /// <param name="year">Year</param>
        ///<returns>
        /// List the Productstatistic.
        ///</returns>
        [HttpGet("monthlyproduct")]
        public IEnumerable<ProductStatistics> GetMonthlyProductStatistics([FromQuery] int year, [FromQuery] int month)
        {
            return statisticSVC.GetMonthlyProductStatistics(year, month);
        }
        /// <summary>
        /// Get CategoryStatistics by year, month.
        /// </summary>
        /// <param name="month">Month</param>
        /// <param name="year">Year</param>
        ///<returns>
        /// List the CategoryStatistics.
        ///</returns>
        [HttpGet("monthlycategory")]
        public IEnumerable<CategoryStatistics> GetMonthlyCategoryStatistics([FromQuery] int year, [FromQuery] int month)
        {
            return statisticSVC.GetMonthlyCategoryStatistics(year, month);
        }
        /// <summary>
        /// Get CustomerStatistics by year, month.
        /// </summary>
        /// <param name="month">Month</param>
        /// <param name="year">Year</param>
        ///<returns>
        /// List the CustomerStatistics.
        ///</returns>
        [HttpGet("monthlycustomer")]
        public CustomerStatistics GetMonthlyCustomerStatistics([FromQuery] int year, [FromQuery] int month)
        {
            return statisticSVC.GetMonthlyCustomerStatistics(year, month);
        }
    }
}
