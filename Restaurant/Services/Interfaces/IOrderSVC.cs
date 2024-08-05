using Restaurant.DTOs;

namespace Restaurant.Services.Interfaces
{
    public interface IOrderSVC
    {
        public IEnumerable<OrderDTO> GetAll();

        public IEnumerable<OrderDTO> GetAllUnFinished();

        public IEnumerable<OrderDTO> GetAllFinished();

        public OrderDTO? GetById(Guid id);

        public Task<OrderDTO?> Add(OrderDTO orderDTO, string? OrderCheckUrl = null);

        public Task<OrderDTO?> Update(OrderDTO orderDTO, Guid id, string? OrderCheckUrl = null);

        public bool UpdateDeliveryStatus(Guid id, int status);

        public Task<bool> UpdatePaymentStatus(Guid id, string? OrderCheckUrl = null);

        public Task<bool> CancelOrder(Guid id);

        public bool Delete(Guid id);
    }
}
