using Restaurant.Models.Db;

namespace Restaurant.Repositories.Interfaces
{
    public interface IOrderDetailRES
    {
        public IEnumerable<OrderDetail> GetAll();

        public IEnumerable<OrderDetail> GetByOrder(Guid orderId);

        public OrderDetail? GetById(int id);

        public OrderDetail? Add(OrderDetail orderDetail);

        public OrderDetail? Update(OrderDetail orderDetail, int id);

        public bool Delete(int id);
    }
}
