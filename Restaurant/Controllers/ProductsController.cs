using Microsoft.AspNetCore.Mvc;
using Restaurant.DTOs;
using Restaurant.Services.Interfaces;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductSVC productSVC) : ControllerBase
    {
        /// <summary>
        /// Get all products current.
        /// </summary>
        ///<returns>
        /// All products.
        ///</returns>
        [HttpGet]
        public IEnumerable<ProductDTO> GetAll()
        {
            return productSVC.GetAll();
        }
        /// <summary>
        /// Get all products current that are available.
        /// </summary>
        ///<returns>
        /// All products that are available.
        ///</returns>
        [HttpGet("available")]
        public IEnumerable<ProductDTO> GetAllAvailable()
        {
            return productSVC.GetAllAvailable();
        }
        /// <summary>
        /// Get a product by ID
        /// </summary>
        ///<returns>
        /// A product by ID
        ///</returns>
        [HttpGet("{id}")]
        public ProductDTO? GetById([FromRoute] Guid id)
        {
            return productSVC.GetById(id);
        }
        /// <summary>
        /// Create a product
        /// </summary>
        ///<returns>
        /// A product created
        ///</returns>
        [HttpPost]
        public ProductDTO? Add([FromForm] ProductDTO productDTO)
        {
            return productSVC.Add(productDTO);
        }
        /// <summary>
        /// Update a product by ID
        /// </summary>
        ///<returns>
        /// A product updated
        ///</returns>
        [HttpPut("{id}")]
        public ProductDTO? Update([FromForm] ProductDTO productDTO, [FromRoute] Guid id)
        {
            return productSVC.Update(productDTO, id);
        }
        /// <summary>
        /// Delete a product by ID
        /// </summary>
        ///<returns>
        /// A product deleted
        ///</returns>
        [HttpDelete("{id}")]
        public bool Delete([FromRoute] Guid id)
        {
            return productSVC.Delete(id);
        }
        /// <summary>
        /// Get the products randomly.
        /// </summary>
        ///<returns>
        /// The products randomly.
        ///</returns>
        [HttpGet("randomavailable")]
        public IEnumerable<ProductDTO> GetRandomAvailable([FromQuery]int number = 6)
        {
            return productSVC.GetRandomAvailable(number);
        }
    }
}
