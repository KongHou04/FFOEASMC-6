using Restaurant.Models.Db;

namespace Restaurant.Repositories.Interfaces
{
    public interface IOrderRES
    {
        public IEnumerable<Order> GetAll();

        public Order? GetById(Guid id);

        public Order? Add(Order order);

        public Order? Update(Order order, Guid id);

        public bool Delete(Guid id);
    }
}
