using Restaurant.DTOs;

namespace Restaurant.Services.Interfaces
{
    public interface ICategorySVC
    {
        public IEnumerable<CategoryDTO> GetAll();

        public IEnumerable<CategoryDTO> GetAllAvailable();

        public CategoryDTO? GetById(Guid id);

        public CategoryDTO? Add(CategoryDTO categoryDTO);

        public CategoryDTO? Update(CategoryDTO categoryDTO, Guid id);

        public bool Delete(Guid id);
    }
}
