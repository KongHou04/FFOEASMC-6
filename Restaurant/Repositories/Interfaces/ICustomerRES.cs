using Restaurant.Models.Db;

namespace Restaurant.Repositories.Interfaces
{
    public interface ICustomerRES
    {
        public Customer? GetById(Guid id);
        public bool Add(Customer customer);
        public bool Update(Customer customer, Guid id);
        public bool Delete(Guid id);

    }
}
