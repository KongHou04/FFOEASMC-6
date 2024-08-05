using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Restaurant.Contexts;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;

namespace Restaurant.Repositories.Implements
{
    public class OrderRES(FFOEContext context) : IOrderRES
    {
        public Order? Add(Order order)
        {
            using IDbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                var entry = context.Orders.Add(order);
                var addedOrder = entry.Entity;
                context.SaveChanges();
                transaction.Commit();
                return addedOrder;
            }
            catch
            {
                transaction.Rollback();
                return null;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var Order = GetById(id);
                if (Order == null)
                    return false;

                context.Orders.Remove(Order);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Order> GetAll()
        {
            return context.Orders.Include(o => o.OrderDetails);
        }

        public Order? GetById(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            try
            {
                return context.Orders
                    .Include(p => p.OrderDetails)
                    .FirstOrDefault(p => p.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public Order? Update(Order order, Guid id)
        {
            if (order == null)
                return null;
            var existingOrder = GetById(id);
            if (existingOrder is null)
                return null;

            try
            {
                existingOrder.OrderTime = order.OrderTime;
                existingOrder.Address = order.Address;
                existingOrder.SubTotal = order.SubTotal;
                existingOrder.Discount = order.Total;
                existingOrder.Note = order.Note;
                existingOrder.DeliveryStatus = order.DeliveryStatus;
                existingOrder.PaymentStatus = order.PaymentStatus;
                existingOrder.IsCanceled = order.IsCanceled;
                existingOrder.CouponId = order.CouponId;

                existingOrder.OrderDetails = order.OrderDetails;

                context.SaveChanges();
                return existingOrder;
            }
            catch
            {
                return null;
            }
        }
    }
}
