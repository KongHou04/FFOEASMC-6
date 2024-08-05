using Restaurant.DTOs;

namespace Restaurant.Services.Interfaces
{
    public interface ICouponTypeSVC
    {
        public IEnumerable<CouponTypeDTO> GetAll();

        public CouponTypeDTO GetById(int id);

        public CouponTypeDTO? GetAvailableByCouponId(Guid id);

        public CouponTypeDTO? Add(CouponTypeDTO couponTypeDTO);

        public CouponTypeDTO? Update(CouponTypeDTO couponTypeDTO, int id);

        public bool Delete(int id);
    }
}
