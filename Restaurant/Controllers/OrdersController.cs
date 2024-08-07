using Microsoft.AspNetCore.Mvc;
using Restaurant.DTOs;
using Restaurant.Services.Interfaces;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderSVC orderSVC) : ControllerBase
    {
        /// <summary>
        /// Get all order current.
        /// </summary>
        ///<returns>
        /// All orders.
        ///</returns>
        [HttpGet]
        public IEnumerable<OrderDTO> GetAll()
        {
            return orderSVC.GetAll();
        }
        /// <summary>
        /// Get all orders in ListOrder that are unfinished.
        /// </summary>
        /// <returns>All orders are unfinished.</returns>
        [HttpGet("unfinished")]
        public IEnumerable<OrderDTO> GetAllUnFinished()
        {
            return orderSVC.GetAllUnFinished();
        }
        /// <summary>
        /// Get all orders in ListOrder that are finished.
        /// </summary>
        /// <returns>All orders are finished.</returns>
        [HttpGet("finished")]
        public IEnumerable<OrderDTO> GetAllFinished()
        {
            return orderSVC.GetAllFinished();
        }
        /// <summary>
        /// Get a order in ListOrder by ID
        /// </summary>
        /// <param name="id">Student needs create</param>
        /// <returns>A student by ID</returns>
        [HttpGet("{id}")]
        public OrderDTO? GetById(Guid id)
        {
            return orderSVC.GetById(id);
        }
        /// <summary>
        /// Create a order into ListStudent
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/orders
        ///     {
        ///        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///        "orderTime": "2024-08-07T00:21:15.838Z",
        ///        "address": "string",
        ///        "email": "user@example.com",
        ///        "subTotal": 1000000,
        ///        "discount": 1000000,
        ///        "total": 1000000,
        ///        "note": "string",
        ///        "deliveryStatus": 2,
        ///        "paymentStatus": 1,
        ///        "isCanceled": true,
        ///        "orderDetails": null
        ///        "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///        "couponId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///    }
        /// </remarks>
        /// <param name="orderDTO">Order needs create</param>
        /// <returns>Order created</returns>
        [HttpPost]
        public async Task<OrderDTO?> Add([FromBody] OrderDTO orderDTO, [FromQuery] string? orderDetailCheckUrl = null)
        {
            return await orderSVC.Add(orderDTO, orderDetailCheckUrl);
        }
        /// <summary>
        /// Update a order by ID
        /// </summary>
        /// <param name="orderDTO">Order needs update</param>
        /// <param name="id">Order ID needs update</param>
        /// <returns>Order updated</returns>
        [HttpPut("{id}")]
        public async Task<OrderDTO?> Update([FromBody] OrderDTO orderDTO, [FromRoute] Guid id, [FromQuery] string? orderDetailCheckUrl = null)
        {
            return await orderSVC.Update(orderDTO, id, orderDetailCheckUrl);
        }
        /// <summary>
        /// Delete a order by ID
        /// </summary>
        /// <param name="id">Order ID needs delete</param>
        /// <returns>The result of deleting is true or false.</returns>
        [HttpDelete("{id}")]
        public bool Delete([FromRoute] Guid id)
        {
            return orderSVC.Delete(id);
        }
        /// <summary>
        /// Update delivery status of a order by ID
        /// </summary>
        /// <param name="id">Order ID needs update.</param>
        /// <param name="status">Status of the Order.</param>
        /// <returns>The result of updating is true or false.</returns>
        [HttpPut("updatedeliverystatus/{id}")]
        public bool UpdateDeliveryStatus([FromRoute] Guid id, [FromQuery] int status)
        {
            return orderSVC.UpdateDeliveryStatus(id, status);
        }
        /// <summary>
        /// Cancel a order by ID
        /// </summary>
        /// <param name="id">Order ID needs update.</param>
        /// <returns>The result of updating is true or false.</returns>
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
        /// <summary>
        /// Update payment status of a order by ID
        /// </summary>
        /// <param name="id">Order ID needs update.</param>
        /// <returns>The result of updating is true or false.</returns>
        [HttpPut("payment/{id}")]
        public async Task<IActionResult> PaymentlOrder([FromRoute] Guid id)
        {
            bool result = await orderSVC.Payment(id);
            return result ? Ok() : BadRequest();
        }
    }
}
