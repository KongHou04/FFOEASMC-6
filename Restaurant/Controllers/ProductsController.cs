using Microsoft.AspNetCore.Mvc;
using Restaurant.DTOs;
using Restaurant.Services.Interfaces;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductSVC productSVC) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ProductDTO> GetAll()
        {
            return productSVC.GetAll();
        }

        [HttpGet("available")]
        public IEnumerable<ProductDTO> GetAllAvailable()
        {
            return productSVC.GetAllAvailable();
        }

        [HttpGet("{id}")]
        public ProductDTO? GetById([FromRoute] Guid id)
        {
            return productSVC.GetById(id);
        }

        [HttpPost]
        public ProductDTO? Add([FromForm] ProductDTO productDTO)
        {
            return productSVC.Add(productDTO);
        }

        [HttpPut("{id}")]
        public ProductDTO? Update([FromForm] ProductDTO productDTO, [FromRoute] Guid id)
        {
            return productSVC.Update(productDTO, id);
        }

        [HttpDelete("{id}")]
        public bool Delete([FromRoute] Guid id)
        {
            return productSVC.Delete(id);
        }

        [HttpGet("randomavailable")]
        public IEnumerable<ProductDTO> GetRandomAvailable([FromQuery]int number = 6)
        {
            return productSVC.GetRandomAvailable(number);
        }
    }
}
