using Microsoft.EntityFrameworkCore.Storage;
using Restaurant.Contexts;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;

namespace Restaurant.Repositories.Implements
{
    public class OrderDetailRES(FFOEContext context) : IOrderDetailRES
    {
        public OrderDetail? Add(OrderDetail orderDetail)
        {
            using IDbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                var entry = context.OrderDetails.Add(orderDetail);
                var addedOrderDetail = entry.Entity;
                context.SaveChanges();
                transaction.Commit();
                return addedOrderDetail;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return null;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var OrderDetail = GetById(id);
                if (OrderDetail == null)
                    return false;

                context.OrderDetails.Remove(OrderDetail);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return context.OrderDetails;
        }

        public OrderDetail? GetById(int id)
        {
            try
            {
                return context.OrderDetails.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<OrderDetail> GetByOrder(Guid orderId)
        {
            return context.OrderDetails.Where(od => od.OrderId == orderId);
        }

        public OrderDetail? Update(OrderDetail orderDetail, int id)
        {
            if (orderDetail == null)
                return null;
            var existingOrderDetail = GetById(id);
            if (existingOrderDetail is null)
                return null;

            try
            {
                existingOrderDetail.Quantity = orderDetail.Quantity;
                existingOrderDetail.Note = orderDetail.Note;
                existingOrderDetail.ProductName = orderDetail.ProductName;
                existingOrderDetail.UnitPrice = orderDetail.UnitPrice;
                

                context.SaveChanges();
                return existingOrderDetail;
            }
            catch
            {
                return null;
            }
        }
    }
}
