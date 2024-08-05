using AutoMapper;
using QRCoder;
using Restaurant.DTOs;
using Restaurant.Helpers;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;
using Restaurant.Services.Interfaces;
using System.Drawing;

namespace Restaurant.Services.Implements
{
    public class OrderSVC
        (IMapper mapper, EmailSender emailSender, 
        IOrderRES orderRES, IProductSVC productSVC,
        ICouponTypeSVC couponTypeSVC, ICouponSVC couponSVC) : IOrderSVC
    {
        public async Task<OrderDTO?> Add(OrderDTO orderDTO, string? orderCheckUrl = null)
        {
            if (!ValidateOrder(orderDTO)) 
                return null;
            if (orderDTO.DeliveryStatus != 0)
                return null;
            if (orderDTO.PaymentStatus != 0)
                return null;
            if (orderDTO.IsCanceled != false)
                return null;
            var order = mapper.Map<Order>(orderDTO);
            var addedOrder = orderRES.Add(order);

            if (addedOrder != null && orderDTO.Email != null)
            {
                var subject = "Order Confirmation";
                var body = $@"
                    <h1>Your Order Has Been Successfully Placed!</h1>
                    <p>Thank you for your order. Your order Id is {addedOrder.Id}.</p>
                    <p>To check the status of your order, click <a href='{orderCheckUrl + $"/{addedOrder.Id}"}'>here</a> or scan the QR code below.</p>
                    <p>We will notify you once your order is processed.</p>";
                
                byte[] qrcode = GenerateQRCode(orderCheckUrl + $"/{addedOrder.Id}");


                await emailSender.SendEmailWithQRCodeAsync(orderDTO.Email, subject, body, qrcode);
            }

            return mapper.Map<OrderDTO>(addedOrder);
        }

        public async Task<bool> CancelOrder(Guid id)
        {
            var order = orderRES.GetById(id);
            if (order == null)
                return false;
            if (order.IsCanceled == true)
                return false;
            order.IsCanceled = true;
            var updatedOrder = orderRES.Update(order, order.Id);
            if (updatedOrder == null)
                return false;
            if (updatedOrder.Email != null)
            {
                var subject = "Order Cancellation Notice";
                var body = $@"
                <h1>Your Order Has Been Cancelled</h1>
                <p>We regret to inform you that your order with ID {updatedOrder.Id} has been cancelled.</p>
                <p>If you have any questions or concerns, please contact our support team.</p>
                <p>We apologize for any inconvenience this may have caused.</p>";

                await emailSender.SendEmailAsync(updatedOrder.Email, subject, body);
            }
            return true;
        }

        public bool Delete(Guid id)
        {
            var existingOrder = orderRES.GetById(id);
            if (existingOrder == null)
                return false;
            return orderRES.Delete(id);
        }

        public IEnumerable<OrderDTO> GetAll()
        {
            var orders = orderRES.GetAll();
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public IEnumerable<OrderDTO> GetAllFinished()
        {
            var orders = orderRES.GetAll();

            var availableOrders = orders.Where(o => o.IsCanceled == true || (o.PaymentStatus == 1 && o.DeliveryStatus == 2))
                .OrderByDescending(o => o.OrderTime);

            return mapper.Map<IEnumerable<OrderDTO>>(availableOrders);
        }

        public IEnumerable<OrderDTO> GetAllUnFinished()
        {
            var orders = orderRES.GetAll();
            var unFinishedOrders = orders.Where(o => (o.PaymentStatus != 1 && o.DeliveryStatus != 2 && o.IsCanceled == false));
            return mapper.Map<IEnumerable<OrderDTO>>(unFinishedOrders);
        }

        public OrderDTO? GetById(Guid id)
        {
            var existingOrder = orderRES.GetById(id);
            return mapper.Map<OrderDTO>(existingOrder);
        }

        public async Task<OrderDTO?> Update(OrderDTO orderDTO, Guid id, string? orderCheckUrl = null)
        {
            if (!ValidateOrder(orderDTO, false))
                return null;
            var order = mapper.Map<Order>(orderDTO);
            var updatedOrder = orderRES.Update(order, id);

            if (updatedOrder != null && orderDTO.Email != null)
            {
                var subject = "Order Update Notification";
                var body = $@"
                    <h1>Your Order Has Been Updated!</h1>
                    <p>Your order Id is {updatedOrder.Id} has been successfully updated.</p>
                    <p>To view the latest status and details of your order, click <a href='{orderCheckUrl}'>here</a>.</p>
                    <p>Thank you for your patience. We will keep you informed of any further updates.</p>";

                await emailSender.SendEmailAsync(orderDTO.Email, subject, body);
            }

            return mapper.Map<OrderDTO>(updatedOrder);
        }

        public bool UpdateDeliveryStatus(Guid id, int status)
        {
            var order = orderRES.GetById(id);
            if (order == null)
                return false;
            order.DeliveryStatus = status;
            var result = orderRES.Update(order, order.Id);
            if (result == null)
                return false;
            return true;
        }

        public async Task<bool> UpdatePaymentStatus(Guid id, string? orderCheckUrl = null)
        {
            var order = orderRES.GetById(id);
            if (order == null)
                return false;
            if (order.PaymentStatus == 1)
                return false;
            order.PaymentStatus = 1;
            if (order.CouponId != null && order.CouponId != Guid.Empty)
                if (!couponSVC.UseCoupon(order.CouponId.Value))
                    return false;
            var updatedOrder = orderRES.Update(order, order.Id);
            if (updatedOrder == null)
                return false;
            if (updatedOrder.CouponId != null && updatedOrder.CouponId != Guid.Empty)
                couponSVC.UseCoupon(updatedOrder.CouponId.Value);
            if (updatedOrder.Email != null)
            {
                var subject = "Payment Successful";
                var body = $@"
                    <h1>Your Payment Has Been Successfully Processed</h1>
                    <p>We are pleased to inform you that the payment for your order with ID {updatedOrder.Id} has been successfully processed.</p>
                    <p>You can check the status of your order by clicking <a href='{orderCheckUrl}'>here</a>.</p>
                    <p>If you have any questions or concerns, please contact our support team.</p>
                    <p>Thank you for your purchase!</p>";

                await emailSender.SendEmailAsync(updatedOrder.Email, subject, body);
            }
            return true;
        }

        private bool ValidateOrder(OrderDTO orderDTO, bool isAddingCheck = true)
        {
            if (orderDTO == null) 
                return false;

            Order? existingOrder = null;
            IEnumerable<OrderDetail> existingOrderDetails = [];
            IEnumerable<int> existingODIds = [];
            if (isAddingCheck == false)
            {
                existingOrder = orderRES.GetById(orderDTO.Id);
                if (existingOrder == null)
                    return false;
                existingOrderDetails = existingOrder.OrderDetails;
                existingODIds = existingOrderDetails.Select(x => x.Id);
            }
            
            if (orderDTO.CouponId != null && orderDTO.CouponId != Guid.Empty)
            {
                var couponType = couponTypeSVC.GetAvailableByCouponId(orderDTO.CouponId.Value);
                if (couponType == null) return false;

                var unFinishedProduct = GetAllUnFinished();
                if (unFinishedProduct != null)
                {
                    var usingCoupon = unFinishedProduct.Where(o => o.CouponId != null).Select(o => o.CouponId);
                    if (usingCoupon.Contains(orderDTO.CouponId))
                        return false;
                }

                if (orderDTO.SubTotal < couponType.MinOrderSubTotalCondition)
                    return false;
                if (orderDTO.Discount == 0)
                {
                    orderDTO.Discount = couponType.HardValue;
                }
                else 
                    if (orderDTO.Discount != couponType.HardValue)
                        return false;
            }

            if (orderDTO.Total + orderDTO.Discount != orderDTO.SubTotal) 
                return false;

            if (orderDTO.OrderDetails.Count != 0)
            {
                decimal subTotal = 0;
                foreach (var item in orderDTO.OrderDetails)
                {
                    if (item.ProductId != null && item.ProductId != Guid.Empty && !existingODIds.Contains(item.Id))
                    {
                        var sellingUnitPrice = productSVC.CaculteSellingUnitPrice(item.ProductId.Value);
                        if (sellingUnitPrice is null) 
                            return false;
                        if (sellingUnitPrice != item.UnitPrice) 
                            return false;
                    }
                    else
                    {
                        var sameExistingOrderDetail = existingOrderDetails.FirstOrDefault(od => od.Id == item.Id);
                        if (sameExistingOrderDetail!.UnitPrice != item.UnitPrice)
                            return false;
                    }
                    subTotal += item.UnitPrice * item.Quantity;
                }
                if (orderDTO.SubTotal != subTotal) 
                    return false;
            }

            

            if (orderDTO.Email is null && (orderDTO.CustomerId is null && orderDTO.CustomerId == Guid.Empty))
                return false;

            return true;
        }

        public byte[] GenerateQRCode(string? url)
        {
            if (url == null)
                return [];
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(5); // --> also read the readme
            return qrCodeAsPngByteArr;
        }
    }
}
