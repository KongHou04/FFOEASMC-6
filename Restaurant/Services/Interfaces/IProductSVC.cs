using Restaurant.DTOs;

namespace Restaurant.Services.Interfaces
{
    public interface IProductSVC
    {
        public IEnumerable<ProductDTO> GetAll();

        public IEnumerable<ProductDTO> GetAllAvailable();

        public IEnumerable<ProductDTO> GetRandomAvailable(int number);

        public ProductDTO? GetById(Guid id);

        public ProductDTO? Add(ProductDTO productDTO);

        public ProductDTO? Update(ProductDTO productDTO, Guid id);

        public bool Delete(Guid id);

        public decimal? CaculteSellingUnitPrice(Guid id);
    }
}
