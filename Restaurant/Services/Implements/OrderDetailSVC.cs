using AutoMapper;
using Restaurant.DTOs;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;
using Restaurant.Services.Interfaces;

namespace Restaurant.Services.Implements
{
    public class OrderDetailSVC(IMapper mapper, IOrderDetailRES orderDetailRES, IProductSVC productSVC) : IOrderDetailSVC
    {
        public OrderDetailDTO? Add(OrderDetailDTO orderDetailDTO)
        {
            if (!ValidateOrderDetail(orderDetailDTO))
                return null;
            var orderDetail = mapper.Map<OrderDetail>(orderDetailDTO);
            var addedOD = orderDetailRES.Add(orderDetail);
            return mapper.Map<OrderDetailDTO>(addedOD);
        }

        public bool Delete(int id)
        {
            return orderDetailRES.Delete(id);
        }

        public IEnumerable<OrderDetailDTO> GetAll()
        {
            var orderDetails = orderDetailRES.GetAll();
            return mapper.Map<IEnumerable<OrderDetailDTO>>(orderDetails);
        }

        public OrderDetailDTO? GetById(int id)
        {
            var existingOD = orderDetailRES.GetById(id);
            return mapper.Map <OrderDetailDTO>(existingOD);
        }

        public IEnumerable<OrderDetailDTO> GetByOrder(Guid orderId)
        {
            var orderDetails = orderDetailRES.GetByOrder(orderId);
            return mapper.Map<IEnumerable<OrderDetailDTO>>(orderDetails);
        }

        public OrderDetailDTO? Update(OrderDetailDTO orderDetailDTO, int id)
        {
            if (!ValidateOrderDetail(orderDetailDTO))
                return null;
            var orderDetail = mapper.Map<OrderDetail>(orderDetailDTO);
            var updatedOD = orderDetailRES.Add(orderDetail);
            return mapper.Map<OrderDetailDTO>(updatedOD);
        }

        private bool ValidateOrderDetail(OrderDetailDTO orderDetailDTO)
        {
            if (orderDetailDTO == null) 
                return false;

            if (orderDetailDTO.ProductId != null && orderDetailDTO.ProductId != Guid.Empty)
            {
                var sellingUnitPrice = productSVC.CaculteSellingUnitPrice(orderDetailDTO.ProductId.Value);
                if (sellingUnitPrice is null)
                    return false;
                if (sellingUnitPrice != orderDetailDTO.UnitPrice)
                    return false;
            }

            return true;
        }
    }
}
