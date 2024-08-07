using Microsoft.AspNetCore.Mvc;
using Restaurant.DTOs;
using Restaurant.Services.Interfaces;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController(IOrderDetailSVC orderDetailSVC) : ControllerBase
    {
        /// <summary>
        /// Get a orderdetail by OrderID.
        /// </summary>
        /// <param name="orderId">Id of OrderDetail</param>
        ///<returns>
        /// A orderdetail.
        ///</returns>
        [HttpGet("byorder/{orderId}")]
        public IEnumerable<OrderDetailDTO> GetByOrder([FromRoute] Guid orderId)
        {
            return orderDetailSVC.GetByOrder(orderId);
        }
        /// <summary>
        /// Get a orderdetail by ID.
        /// </summary>
        /// <param name="id">Id of OrderDetail</param>
        ///<returns>
        /// A orderdetail.
        ///</returns>
        [HttpGet("{id}")]
        public OrderDetailDTO? GetById([FromRoute] int id)
        {
            return orderDetailSVC.GetById(id);
        }
        /// <summary>
        /// Create a orderdetail.
        /// </summary>
        /// <param name="orderDetailDTO">OrderDetailDTO to be created</param>
        ///<returns>
        /// A OrderDetailDTO created.
        ///</returns>
        [HttpPost]
        public OrderDetailDTO? Add([FromBody] OrderDetailDTO orderDetailDTO)
        {
            return orderDetailSVC.Add(orderDetailDTO);
        }
        /// <summary>
        /// Update a orderdetail.
        /// </summary>
        /// <param name="orderDetailDTO">OrderDetailDTO to be updated</param>
        /// <param name="id">ID of OrderDetailDTO.</param>
        ///<returns>
        /// A OrderDetailDTO updated.
        ///</returns>
        [HttpPut("{id}")]
        public OrderDetailDTO? Update([FromBody] OrderDetailDTO orderDetailDTO, [FromRoute] int id)
        {
            return orderDetailSVC.Update(orderDetailDTO, id);
        }
        /// <summary>
        /// Delete a orderdetail.
        /// </summary>
        /// <param name="id">ID of OrderDetailDTO.</param>
        ///<returns>
        /// True or false.
        ///</returns>
        [HttpDelete("{id}")]
        public bool Delete([FromRoute] int id)
        {
            return orderDetailSVC.Delete(id);
        }
    }
}
