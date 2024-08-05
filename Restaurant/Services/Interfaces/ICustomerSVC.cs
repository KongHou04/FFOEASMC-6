using Restaurant.DTOs;

namespace Restaurant.Services.Interfaces
{
    public interface ICustomerSVC
    {
        public CustomerDTO GetById(Guid id);

        public bool Add(CustomerDTO customerDTO);

        public bool Update(CustomerDTO customerDTO, Guid id);

        public bool Delete(Guid id);
    }
}
