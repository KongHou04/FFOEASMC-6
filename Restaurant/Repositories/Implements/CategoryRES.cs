using Microsoft.EntityFrameworkCore.Storage;
using Restaurant.Contexts;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Restaurant.Repositories.Implements
{
    public class CategoryRES(FFOEContext context) : ICategoryRES
    {
        public Category? Add(Category category)
        {
            using IDbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                var entry = context.Categories.Add(category);
                var addedCategory = entry.Entity;
                context.SaveChanges();
                transaction.Commit();
                return addedCategory;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return null;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var category = GetById(id);
                if (category == null)
                    return false;

                context.Categories.Remove(category);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories;
        }

        public IEnumerable<Category> GetAllAvailable()
        {
            return context.Categories.Where(c => c.IsAvailable == true);
        }

        public Category? GetById(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            try
            {
                return context.Categories.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public Category? Update(Category category, Guid id)
        {
            if (category == null)
                return null;
            var existingCategory = GetById(id);
            if (existingCategory is null)
                return null;

            try
            {
                existingCategory.Name = category.Name;
                existingCategory.Description = category.Description;
                existingCategory.IsAvailable = category.IsAvailable;

                context.SaveChanges();
                return existingCategory;
            }
            catch
            {
                return null;
            }
        }
    }
}
