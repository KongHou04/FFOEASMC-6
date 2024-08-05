using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Restaurant.Contexts;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;

namespace Restaurant.Repositories.Implements
{
    public class CouponRES(FFOEContext context) : ICouponRES
    {
        public bool Add(int couponTypeId, int number = 100)
        {
            using IDbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                for (int i = 0; i < number; i++)
                {
                    var coupon = new Coupon()
                    {
                        IsUsed = false,
                        CustomerId = null,
                        CouponTypeId = couponTypeId,
                    };
                    context.Coupons.Add(coupon);
                }
                context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var Coupon = GetById(id);
                if (Coupon == null)
                    return false;

                context.Coupons.Remove(Coupon);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Coupon> GetByCustomer(Guid customerId)
        {
            return context.Coupons.Where(c => c.CustomerId == customerId);
        }

        public Coupon? GetById(Guid id)
        {
            try
            {
                return context.Coupons.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public bool Update(Coupon coupon, Guid id)
        {
            if (coupon == null)
                return false;
            var existingCoupon = GetById(id);
            if (existingCoupon is null)
                return false;

            try
            {
                existingCoupon.CustomerId = coupon.CustomerId;
                existingCoupon.IsUsed = coupon.IsUsed;

                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
