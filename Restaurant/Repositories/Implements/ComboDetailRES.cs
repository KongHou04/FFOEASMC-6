using Microsoft.EntityFrameworkCore.Storage;
using Restaurant.Contexts;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;

namespace Restaurant.Repositories.Implements
{
    public class ComboDetailRES(FFOEContext context) : IComboDetailRES
    {
        public ComboDetail? Add(ComboDetail comboDetail)
        {
            using IDbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                var entry = context.ComboDetails.Add(comboDetail);
                var addedComboDetail = entry.Entity;
                context.SaveChanges();
                transaction.Commit();
                return addedComboDetail;
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
                var ComboDetail = GetById(id);
                if (ComboDetail == null)
                    return false;

                context.ComboDetails.Remove(ComboDetail);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ComboDetail? GetById(int id)
        {
            try
            {
                return context.ComboDetails.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<ComboDetail> GetByCombo(Guid comboId)
        {
            return context.ComboDetails.Where(od => od.ComboId == comboId);
        }

        public ComboDetail? Update(ComboDetail comboDetail, int id)
        {
            if (comboDetail == null)
                return null;
            var existingComboDetail = GetById(id);
            if (existingComboDetail is null)
                return null;

            try
            {
                existingComboDetail.Quantity = comboDetail.Quantity;
                existingComboDetail.ProductId = comboDetail.ProductId;

                context.SaveChanges();
                return existingComboDetail;
            }
            catch
            {
                return null;
            }
        }
    }
}
