using Microsoft.EntityFrameworkCore.Storage;
using Restaurant.Contexts;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;

namespace Restaurant.Repositories.Implements
{
    public class CouponTypeRES(FFOEContext context) : ICouponTypeRES
    {
        public CouponType? Add(CouponType couponType)
        {
            using IDbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                var entry = context.CouponTypes.Add(couponType);
                var addedCouponType = entry.Entity;
                context.SaveChanges();
                transaction.Commit();
                return addedCouponType;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return null;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var CouponType = GetById(id);
                if (CouponType == null)
                    return false;

                context.CouponTypes.Remove(CouponType);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public CouponType? GetById(int id)
        {
            try
            {
                return context.CouponTypes.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public CouponType? Update(CouponType couponType, int id)
        {
            if (couponType == null)
                return null;
            var existingCouponType = GetById(id);
            if (existingCouponType is null)
                return null;

            try
            {
                existingCouponType.HardValue = couponType.HardValue;
                existingCouponType.PercentValue = couponType.PercentValue;
                existingCouponType.StartTime = couponType.EndTime;
                existingCouponType.EndTime = couponType.EndTime;

                context.SaveChanges();
                return existingCouponType;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<CouponType> GetAll()
        {
            return context.CouponTypes;
        }
    }
}
