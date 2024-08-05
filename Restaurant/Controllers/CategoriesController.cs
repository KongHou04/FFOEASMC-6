using Microsoft.AspNetCore.Mvc;
using Restaurant.DTOs;
using Restaurant.Services.Interfaces;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategorySVC categorySVC) : ControllerBase
    {
        [HttpGet("randomGUID")]
        public string GetRandomGuid()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }

        [HttpGet]
        public IEnumerable<CategoryDTO> GetAll()
        {
            return categorySVC.GetAll();
        }

        [HttpGet("available")]
        public IEnumerable<CategoryDTO> GetAllAvailable()
        {
            return categorySVC.GetAllAvailable();
        }

        [HttpGet("{id}")]
        public CategoryDTO? GetById([FromRoute] Guid id)
        {
            return categorySVC.GetById(id);
        }

        [HttpPost]
        public CategoryDTO? Add([FromBody] CategoryDTO categoryDTO)
        {
            return categorySVC.Add(categoryDTO);
        }

        [HttpPut("{id}")]
        public CategoryDTO? Update([FromBody] CategoryDTO categoryDTO, [FromRoute] Guid id)
        {
            return categorySVC.Update(categoryDTO, id);
        }

        [HttpDelete("{id}")]
        public bool Delete([FromRoute] Guid id)
        {
            return categorySVC.Delete(id);
        }
    }
}
