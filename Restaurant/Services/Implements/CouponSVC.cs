using AutoMapper;
using Restaurant.DTOs;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;
using Restaurant.Services.Interfaces;

namespace Restaurant.Services.Implements
{
    public class CouponSVC(IMapper mapper, ICouponRES couponRES) : ICouponSVC
    {
        public bool Add(int CouponDTOTypeId, int number = 100)
        {
            return couponRES.Add(CouponDTOTypeId, number);
        }

        public bool Delete(Guid id)
        {
            return couponRES.Delete(id);
        }

        public IEnumerable<CouponDTO> GetByCustomer(Guid customerId)
        {
            var coupons = couponRES.GetByCustomer(customerId);
            return mapper.Map<IEnumerable<CouponDTO>>(coupons);
        }

        public CouponDTO? GetById(Guid id)
        {
            var existingCoupon = couponRES.GetById(id);
            return mapper.Map<CouponDTO>(existingCoupon);
        }

        public bool Update(CouponDTO couponDTO, Guid id)
        {
            var existingCoupon = mapper.Map<Coupon>(couponDTO);
            return couponRES.Update(existingCoupon, id);
        }

        public bool UseCoupon(Guid id)
        {
            var existingCoupon = couponRES.GetById(id);
            if (existingCoupon == null) 
                return false;
            if (existingCoupon.IsUsed == true)
                return false;
            existingCoupon.IsUsed = true;
            return couponRES.Update(existingCoupon, id);
        }
    }
}
