using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Restaurant.Contexts;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;

namespace Restaurant.Repositories.Implements
{
    public class ProductRES(FFOEContext context) : IProductRES
    {
        public Product? Add(Product product)
        {
            using IDbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                var entry = context.Products.Add(product);
                var addedProduct = entry.Entity;
                context.SaveChanges();
                transaction.Commit();
                return addedProduct;
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
                var product = GetById(id);
                if (product == null)
                    return false;

                context.Products.Remove(product);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Products.Include(p => p.ComboDetails);
        }

        public IEnumerable<Product> GetAllAvailable()
        {
            var products = context.Products.Include(p => p.ComboDetails).Where(p => p.IsAvailable == true);
            var categoryIds = context.Categories.Where(c => c.IsAvailable == true).Select(c => c.Id);
            var availableProducts = products.Where(p => categoryIds.Contains(p.CategoryId));
            return availableProducts;
        }

        public Product? GetById(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            try
            {
                return context.Products
                    .Include(p => p.ComboDetails)
                    .FirstOrDefault(p => p.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public Product? Update(Product product, Guid id)
        {
            if (product == null)
                return null;
            var existingProduct = GetById(id);
            if (existingProduct is null)
                return null;

            try
            {
                existingProduct.Name = product.Name;
                existingProduct.UnitPrice = product.UnitPrice;
                existingProduct.PercentDiscount = product.PercentDiscount;
                existingProduct.HardDiscount = product.HardDiscount;
                existingProduct.Image = product.Image;
                existingProduct.IsAvailable = product.IsAvailable;
                existingProduct.Description = product.Description;
                existingProduct.IsAvailable = product.IsAvailable;
                existingProduct.CategoryId = product.CategoryId;

                existingProduct.ComboDetails = product.ComboDetails;

                context.SaveChanges();
                return existingProduct;
            }
            catch
            {
                return null;
            }
        }
    }
}
