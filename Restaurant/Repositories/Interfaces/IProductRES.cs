using Restaurant.Models.Db;

namespace Restaurant.Repositories.Interfaces
{
    public interface IProductRES
    {
        public IEnumerable<Product> GetAll();

        public IEnumerable<Product> GetAllAvailable();

        public Product? GetById(Guid id);

        public Product? Add(Product product);

        public Product? Update(Product product, Guid id);

        public bool Delete(Guid id);
    }
}
