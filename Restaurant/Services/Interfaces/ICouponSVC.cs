using Restaurant.DTOs;
using Restaurant.Models.Db;

namespace Restaurant.Services.Interfaces
{
    public interface ICouponSVC
    {
        public IEnumerable<CouponDTO> GetByCustomer(Guid customerId);

        public CouponDTO? GetById(Guid id);

        public bool UseCoupon(Guid id);

        public bool Add(int CouponDTOTypeId, int number = 100);

        public bool Update(CouponDTO CouponDTO, Guid id);

        public bool Delete(Guid id);
    }
}
