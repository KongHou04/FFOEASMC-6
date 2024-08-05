using AutoMapper;
using Restaurant.DTOs;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;
using Restaurant.Services.Interfaces;

namespace Restaurant.Services.Implements
{
    public class CategorySVC(IMapper mapper, ICategoryRES categoryRES) : ICategorySVC
    {
        public CategoryDTO? Add(CategoryDTO categoryDTO)
        {
            var category = mapper.Map<Category>(categoryDTO);
            var addedCategory = categoryRES.Add(category);
            return mapper.Map<CategoryDTO>(addedCategory);
        }

        public bool Delete(Guid id)
        {
            return categoryRES.Delete(id);
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            var categories = categoryRES.GetAll();
            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public IEnumerable<CategoryDTO> GetAllAvailable()
        {
            var categories = categoryRES.GetAllAvailable();
            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public CategoryDTO? GetById(Guid id)
        {
            var existingCategory = categoryRES.GetById(id);
            return mapper.Map<CategoryDTO>(existingCategory);
        }

        public CategoryDTO? Update(CategoryDTO categoryDTO, Guid id)
        {
            var category = mapper.Map<Category>(categoryDTO);
            var updatedCategory = categoryRES.Update(category, id);
            return mapper.Map<CategoryDTO>(updatedCategory);
        }
    }
}
