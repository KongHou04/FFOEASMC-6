using Restaurant.Models.Db;

namespace Restaurant.Repositories.Interfaces
{
    public interface ICouponTypeRES
    {
        public IEnumerable<CouponType> GetAll();

        public CouponType? GetById(int id);

        public CouponType? Add(CouponType couponType);

        public CouponType? Update(CouponType couponType, int id);

        public bool Delete(int id);
    }
}
