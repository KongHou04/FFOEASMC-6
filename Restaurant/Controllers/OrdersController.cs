using Microsoft.AspNetCore.Mvc;
using Restaurant.DTOs;
using Restaurant.Services.Interfaces;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderSVC orderSVC) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<OrderDTO> GetAll()
        {
            return orderSVC.GetAll();
        }

        [HttpGet("unfinished")]
        public IEnumerable<OrderDTO> GetAllUnFinished()
        {
            return orderSVC.GetAllUnFinished();
        }

        [HttpGet("finished")]
        public IEnumerable<OrderDTO> GetAllFinished()
        {
            return orderSVC.GetAllFinished();
        }

        [HttpGet("customer")]
        public IEnumerable<OrderDTO> GetAllByCustomer(string email)
        {
            return orderSVC.GetByCustomer(email);
        }

        [HttpGet("{id}")]
        public OrderDTO? GetById(Guid id)
        {
            return orderSVC.GetById(id);
        }

        [HttpPost]
        public async Task<OrderDTO?> Add([FromBody] OrderDTO orderDTO, [FromQuery] string? orderDetailCheckUrl = null)
        {
            return await orderSVC.Add(orderDTO, orderDetailCheckUrl);
        }

        [HttpPut("{id}")]
        public async Task<OrderDTO?> Update([FromBody] OrderDTO orderDTO, [FromRoute] Guid id, [FromQuery] string? orderDetailCheckUrl = null)
        {
            return await orderSVC.Update(orderDTO, id, orderDetailCheckUrl);
        }

        [HttpDelete("{id}")]
        public bool Delete([FromRoute] Guid id)
        {
            return orderSVC.Delete(id);
        }

        [HttpPut("updatedeliverystatus/{id}")]
        public bool UpdateDeliveryStatus([FromRoute] Guid id, [FromQuery] int status)
        {
            return orderSVC.UpdateDeliveryStatus(id, status);
        }

        [HttpPut("cancel/{id}")]
        public async Task<bool> CancelOrder([FromRoute] Guid id)
        {
            return await orderSVC.CancelOrder(id);
        }

        [HttpPut("pay/{id}")]
        public async Task<bool> PayOrder([FromRoute] Guid id)
        {
            return await orderSVC.UpdatePaymentStatus(id);
        }
    }
}
