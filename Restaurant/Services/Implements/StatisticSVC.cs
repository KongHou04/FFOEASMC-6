using Restaurant.DTOs.Statistic;
using Restaurant.Services.Interfaces;

namespace Restaurant.Services.Implements
{
    public class StatisticSVC(IProductSVC productSVC, IOrderSVC orderSVC, IOrderDetailSVC orderDetailSVC) : IStatisticSVC
    {

        public IEnumerable<CategoryStatistics> GetMonthlyCategoryStatistics(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var orders = orderSVC.GetAll()
                                 .Where(o => o.OrderTime >= startDate && o.OrderTime <= endDate)
                                 .ToList();

            var orderIds = orders.Select(o => o.Id);

            var orderDetails = orderDetailSVC.GetAll()
                                               .Where(od => orderIds.Contains(od.OrderId))
                                               .ToList();

            var productStats = orderDetails.GroupBy(od => od.ProductId)
                                           .Select(g => new
                                           {
                                               ProductId = g.Key,
                                               UnitsSold = g.Sum(od => od.Quantity),
                                               RevenueGenerated = g.Sum(od => od.Quantity * od.UnitPrice),
                                               CategoryId = g.Key == null? null : productSVC.GetById(g.Key.Value)?.CategoryId,
                                           })
                                           .ToList();

            var categoryStats = productStats.GroupBy(ps => ps.CategoryId)
                                            .Select(g => new CategoryStatistics
                                            {
                                                CategoryId = g.Key,
                                                UnitsSold = g.Sum(ps => ps.UnitsSold),
                                                RevenueGenerated = g.Sum(ps => ps.RevenueGenerated)
                                            });

            return categoryStats;
        }

        public CustomerStatistics GetMonthlyCustomerStatistics(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var allOrders = orderSVC.GetAll();

            var orders = allOrders.Where(o => o.OrderTime >= startDate && o.OrderTime <= endDate)
                                 .ToList();

            var Emails = orders.Select(o => o.Email).Distinct().ToList();

            var previousOrders = allOrders.Where(o => o.OrderTime < startDate)
                                     .ToList();

            var previousCustomerIds = previousOrders.Select(o => o.Email).Distinct().ToList();

            var newCustomers = Emails.Except(previousCustomerIds).Count();
            var returningCustomers = Emails.Intersect(previousCustomerIds).Count();
            var totalCustomers = Emails.Count();

            return new CustomerStatistics
            {
                Year = year,
                Month = month,
                NewCustomerCount = newCustomers,
                ReturningCustomerCount = returningCustomers,
                TotalCustomerCount = totalCustomers
            };
        }

        public OrderStatistics GetMonthlyOrderStatistics(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var orders = orderSVC.GetAll()
                                     .Where(o => o.OrderTime >= startDate && o.OrderTime <= endDate)
                                     .ToList();

            var orderStats = new OrderStatistics()
            {
                Month = month,
                Year = year,
                TotalOrders = orders.Count(),
                DeliveriedOrders = orders.Where(o => o.DeliveryStatus == 2).Count(),
                PaidOrders = orders.Where(o => o.PaymentStatus == 1).Count(),
                CanceledOrders = orders.Where(o => o.IsCanceled == true).Count(),
                CompletedOrders = orders.Where(o => (o.IsCanceled = true || !(o.PaymentStatus == 1 && o.DeliveryStatus == 2 && o.IsCanceled == false))).Count(),
                SumSubTotal = orders.Sum(o => o.SubTotal),
                SumDiscount = orders.Sum(o => o.Discount),
                SumTotal = orders.Sum(o => o.Total)
            };

            return orderStats;
        }

        public IEnumerable<ProductStatistics> GetMonthlyProductStatistics(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var orders = orderSVC.GetAll()
                                 .Where(o => o.OrderTime >= startDate && o.OrderTime <= endDate)
                                 .ToList();

            var orderIds = orders.Select(o => o.Id);

            var orderDetails = orderDetailSVC.GetAll()
                                               .Where(od => orderIds.Contains(od.OrderId))
                                               .ToList();

            var productStats = orderDetails.GroupBy(od => od.ProductId)
                                           .Select(g => new ProductStatistics
                                           {
                                               Month = month,
                                               Year = year,
                                               ProductId = g.Key,
                                               UnitsSold = g.Sum(od => od.Quantity),
                                               RevenueGenerated = g.Sum(od => od.Quantity * od.UnitPrice),
                                           });

            return productStats;
        }
    }
}
