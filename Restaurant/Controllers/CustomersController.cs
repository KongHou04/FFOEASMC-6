using Microsoft.AspNetCore.Mvc;
using Restaurant.DTOs;
using Restaurant.Services.Interfaces;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(ICustomerSVC CustomerSVC) : ControllerBase
    {
        [HttpGet("{id}")]
        public CustomerDTO GetById([FromRoute] Guid id)
        {
            return CustomerSVC.GetById(id);
        }

        [HttpPost]
        public bool Add([FromBody] CustomerDTO CustomerDTO)
        {
            return CustomerSVC.Add(CustomerDTO);
        }

        [HttpPut("{id}")]
        public bool Update([FromBody] CustomerDTO CustomerDTO, Guid id)
        {
            return CustomerSVC.Update(CustomerDTO, id);
        }

        [HttpDelete("{id}")]
        public bool Delete([FromRoute] Guid id)
        {
            return CustomerSVC.Delete(id);
        }
    }
}
