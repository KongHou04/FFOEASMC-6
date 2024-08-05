using AutoMapper;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Restaurant.DTOs;
using Restaurant.Helpers;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;
using Restaurant.Services.Interfaces;

namespace Restaurant.Services.Implements
{
    public class ProductSVC(IMapper mapper, IConfiguration configuration, ImgHelper imgHelper, IProductRES productRES) : IProductSVC
    {
        string ProductImgPath = configuration["FolderPath:Product"]!;
        public ProductDTO? Add(ProductDTO productDTO)
        {
            string? imageFileName = null;
            if (productDTO.ImgFile != null)
            {
                imageFileName = imgHelper.SaveImage(productDTO.ImgFile, ProductImgPath);
                productDTO.Image = imageFileName;
            }
            var product = mapper.Map<Product>(productDTO);
            var addedProduct = productRES.Add(product);

            if (addedProduct == null && imageFileName != null)
                imgHelper.DeleteImage(imageFileName, ProductImgPath);

            return mapper.Map<ProductDTO>(addedProduct);
        }

        public decimal? CaculteSellingUnitPrice(Guid id)
        {
            var product = productRES.GetById(id);
            if (product == null) return null;
            var percent = (100 - product.PercentDiscount) / 100m;
            decimal sellingUnitPrice = (percent * product.UnitPrice) - product.HardDiscount;
            return sellingUnitPrice < 0? 0 : sellingUnitPrice;
        }

        public bool Delete(Guid id)
        {
            Product? existingProduct = null;
            string? existingImgFileName = null;
            existingProduct = productRES.GetById(id);
            existingImgFileName = existingProduct?.Image;
            var result = productRES.Delete(id);
            if (result == true && existingImgFileName != null)
            {
                imgHelper.DeleteImage(existingImgFileName, ProductImgPath);
            }
            return result;
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            var products = productRES.GetAll();
            return mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public IEnumerable<ProductDTO> GetAllAvailable()
        {
            var availableProducts = productRES.GetAllAvailable();
            return mapper.Map<IEnumerable<ProductDTO>>(availableProducts);
        }

        public ProductDTO? GetById(Guid id)
        {
            var existingProduct = productRES.GetById(id);
            return mapper.Map<ProductDTO>(existingProduct);
        }

        public IEnumerable<ProductDTO> GetRandomAvailable(int number)
        {
            var availableProducts = productRES.GetAllAvailable();
            var randomProducts = availableProducts.OrderBy(x => Guid.NewGuid()).Take(number);
            return mapper.Map<IEnumerable<ProductDTO>>(randomProducts);
        }

        public ProductDTO? Update(ProductDTO productDTO, Guid id)
        {
            string? newImageFileName = null;
            Product? existingProduct = null;
            string? existingImgFileName = null;
            existingProduct = productRES.GetById(id);
            existingImgFileName = existingProduct?.Image;
            productDTO.Image = existingImgFileName;

            if (productDTO.ImgFile != null)
            {
                
                newImageFileName = imgHelper.SaveImage(productDTO.ImgFile, ProductImgPath);
                productDTO.Image = newImageFileName;
            }

            var product = mapper.Map<Product>(productDTO);
            var updatedProduct = productRES.Update(product, id);

            if (updatedProduct == null && newImageFileName != null)
                imgHelper.DeleteImage(newImageFileName, ProductImgPath);
            if (updatedProduct != null && existingImgFileName != null)
                imgHelper.DeleteImage(existingImgFileName, ProductImgPath);

            return mapper.Map<ProductDTO>(updatedProduct);
        }
    }
}
