using Restaurant.Models.Db;

namespace Restaurant.Repositories.Interfaces
{
    public interface ICouponTypeRES
    {
        public IEnumerable<CouponType> GetAll();

        public CouponType? GetById(int id);

        public CouponType? Add(CouponType CouponType);

        public CouponType? Update(CouponType CouponType, int id);

        public bool Delete(int id);
    }
}
