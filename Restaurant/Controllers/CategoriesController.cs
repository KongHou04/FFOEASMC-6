using Microsoft.AspNetCore.Mvc;
using Restaurant.DTOs;
using Restaurant.Services.Interfaces;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategorySVC categorySVC) : ControllerBase
    {
        /// <summary>
        /// Render Id of Category
        /// </summary>
        /// <returns>A random ID.</returns>
        [HttpGet("randomGUID")]
        public string GetRandomGuid()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>All categories.</returns>
        [HttpGet]
        public IEnumerable<CategoryDTO> GetAll()
        {
            return categorySVC.GetAll();
        }
        /// <summary>
        /// Get all the categories that are available.
        /// </summary>
        /// <returns>All the categories are available.</returns>
        [HttpGet("available")]
        public IEnumerable<CategoryDTO> GetAllAvailable()
        {
            return categorySVC.GetAllAvailable();
        }
        /// <summary>
        /// Get a category by ID
        /// </summary>
        /// <returns>A category needs to be found.</returns>
        [HttpGet("{id}")]
        public CategoryDTO? GetById([FromRoute] Guid id)
        {
            return categorySVC.GetById(id);
        }
        /// <summary>
        /// Create a category
        /// </summary>
        /// <param name="categoryDTO">Category to be created.</param>
        /// <returns>The category created.</returns>
        [HttpPost]
        public CategoryDTO? Add([FromBody] CategoryDTO categoryDTO)
        {
            return categorySVC.Add(categoryDTO);
        }
        /// <summary>
        /// Update a category by ID
        /// </summary>
        /// <param name="categoryDTO">Category to be updated.</param>
        /// <param name="id">CategoryId to be updated.</param>
        /// <returns>The category updated.</returns>
        [HttpPut("{id}")]
        public CategoryDTO? Update([FromBody] CategoryDTO categoryDTO, [FromRoute] Guid id)
        {
            return categorySVC.Update(categoryDTO, id);
        }
        /// <summary>
        /// Delete a category by ID
        /// </summary>
        /// <param name="id">CategoryId to be deleted.</param>
        /// <returns>True of false.</returns>
        [HttpDelete("{id}")]
        public bool Delete([FromRoute] Guid id)
        {
            return categorySVC.Delete(id);
        }
    }
}
