using AutoMapper;
using Restaurant.DTOs;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;
using Restaurant.Services.Interfaces;

namespace Restaurant.Services.Implements
{
    public class CustomerSVC(IMapper mapper, ICustomerRES customerRES) : ICustomerSVC
    {
        public bool Add(CustomerDTO customerDTO)
        {
            var customer = mapper.Map<Customer>(customerDTO);
            var result = customerRES.Add(customer);
            return result;
        }

        public bool Delete(Guid id)
        {
            return customerRES.Delete(id);
        }

        public CustomerDTO GetById(Guid id)
        {
            var existingCustomer = customerRES.GetById(id);
            return mapper.Map<CustomerDTO>(existingCustomer);
        }

        public bool Update(CustomerDTO customerDTO, Guid id)
        {
            var customer = mapper.Map<Customer>(customerDTO);
            var result = customerRES.Update(customer, id);
            return result;
        }
    }
}
