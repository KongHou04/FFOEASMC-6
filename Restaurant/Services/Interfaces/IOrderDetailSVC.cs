using Restaurant.DTOs;

namespace Restaurant.Services.Interfaces
{
    public interface IOrderDetailSVC
    {
        public IEnumerable<OrderDetailDTO> GetAll();

        public IEnumerable<OrderDetailDTO> GetByOrder(Guid orderId);

        public OrderDetailDTO? GetById(int id);
        
        public OrderDetailDTO? Add(OrderDetailDTO orderDetailDTO);

        public OrderDetailDTO? Update(OrderDetailDTO orderDetailDTO, int id);

        public bool Delete(int id);
    }
}
