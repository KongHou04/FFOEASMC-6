using AutoMapper;
using Restaurant.DTOs;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;
using Restaurant.Services.Interfaces;
using System.Linq;

namespace Restaurant.Services.Implements
{
    public class CouponTypeSVC(IMapper mapper, ICouponTypeRES couponTypeRES, ICouponSVC couponSVC) : ICouponTypeSVC
    {
        public CouponTypeDTO? Add(CouponTypeDTO couponTypeDTO)
        {
            var couponType = mapper.Map<CouponType>(couponTypeDTO);
            var addedCouponType = couponTypeRES.Add(couponType);
            return mapper.Map<CouponTypeDTO>(addedCouponType);
        }

        public bool Delete(int id)
        {
            return couponTypeRES.Delete(id);
        }

        public IEnumerable<CouponTypeDTO> GetAll()
        {
            var couponTypes = couponTypeRES.GetAll();
            return mapper.Map<IEnumerable<CouponTypeDTO>>(couponTypes);
        }

        public CouponTypeDTO? GetAvailableByCouponId(Guid id)
        {
            var existingCoupon = couponSVC.GetById(id);
            if (existingCoupon == null) 
                return null;
            if (existingCoupon.IsUsed == true)
                return null;
            var existingCouponType = couponTypeRES.GetById(existingCoupon.CouponTypeId);
            return mapper.Map<CouponTypeDTO>(existingCouponType);
        }

        public CouponTypeDTO GetById(int id)
        {
            var existingCouponType = couponTypeRES.GetById(id);
            return mapper.Map<CouponTypeDTO>(existingCouponType);
        }

        public CouponTypeDTO? Update(CouponTypeDTO couponTypeDTO, int id)
        {
            var couponType = mapper.Map<CouponType>(couponTypeDTO);
            var updatedCouponType = couponTypeRES.Update(couponType, id);
            return mapper.Map<CouponTypeDTO>(updatedCouponType);
        }

        private bool ValidateCouponType(CouponTypeDTO couponTypeDTO)
        {
            if (couponTypeDTO == null) return false;
            // Does not supported
            if (couponTypeDTO.PercentValue != 0) 
                return false;
            if (couponTypeDTO.StartTime >= couponTypeDTO.EndTime)
                return false;
            if (couponTypeDTO.HardValue > couponTypeDTO.MinOrderSubTotalCondition)
                return false;
            
            return true;
        }
    }
}
