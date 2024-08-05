using Restaurant.Models.Db;

namespace Restaurant.Repositories.Interfaces
{
    public interface ICategoryRES
    {
        public IEnumerable<Category> GetAll();

        public IEnumerable<Category> GetAllAvailable();

        public Category? GetById(Guid id);

        public Category? Add(Category category);

        public Category? Update(Category category, Guid id);

        public bool Delete(Guid id);
    }
}
