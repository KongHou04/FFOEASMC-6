using Restaurant.Models.Db;

namespace Restaurant.Repositories.Interfaces
{
    public interface ICouponRES
    {
        public IEnumerable<Coupon> GetByCustomer(Guid customerId);

        public Coupon? GetById(Guid id);

        public bool Add(int couponTypeId, int number = 100);

        public bool Update(Coupon coupon, Guid id);

        public bool Delete(Guid id);
    }
}
