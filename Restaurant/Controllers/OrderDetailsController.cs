using Microsoft.AspNetCore.Mvc;
using Restaurant.DTOs;
using Restaurant.Services.Interfaces;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController(IOrderDetailSVC orderDetailSVC) : ControllerBase
    {
        [HttpGet("byorder/{orderId}")]
        public IEnumerable<OrderDetailDTO> GetByOrder([FromRoute] Guid orderId)
        {
            return orderDetailSVC.GetByOrder(orderId);
        }

        [HttpGet("{id}")]
        public OrderDetailDTO? GetById([FromRoute] int id)
        {
            return orderDetailSVC.GetById(id);
        }

        [HttpPost]
        public OrderDetailDTO? Add([FromBody] OrderDetailDTO orderDetailDTO)
        {
            return orderDetailSVC.Add(orderDetailDTO);
        }

        [HttpPut("{id}")]
        public OrderDetailDTO? Update([FromBody] OrderDetailDTO orderDetailDTO, [FromRoute] int id)
        {
            return orderDetailSVC.Update(orderDetailDTO, id);
        }

        [HttpDelete("{id}")]
        public bool Delete([FromRoute] int id)
        {
            return orderDetailSVC.Delete(id);
        }
    }
}
