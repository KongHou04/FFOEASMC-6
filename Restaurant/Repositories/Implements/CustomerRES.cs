using Microsoft.EntityFrameworkCore.Storage;
using Restaurant.Contexts;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;

namespace Restaurant.Repositories.Implements
{
    public class CustomerRES(FFOEContext context) : ICustomerRES
    {
        public bool Add(Customer customer)
        {
            using IDbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                var entry = context.Customers.Add(customer);
                var addedCustomer = entry.Entity;
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
                var Customer = GetById(id);
                if (Customer == null)
                    return false;

                context.Customers.Remove(Customer);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Customer? GetById(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            try
            {
                return context.Customers.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public bool Update(Customer customer, Guid id)
        {
            if (customer == null)
                return false;
            var existingCustomer = GetById(id);
            if (existingCustomer is null)
                return false;

            try
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Address = customer.Address;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;

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
