using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Moq;
using Restaurant.DTOs;
using Restaurant.Helpers;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;
using Restaurant.Services.Implements;

namespace ApiTest;

public class ProductSVCTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<ImgHelper> _imgHelperMock;
    private readonly Mock<IProductRES> _productRESMock;
    private readonly ProductSVC _productSVC;
    private readonly string _productImgPath = "testPath";
    private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;
    public ProductSVCTests()
    {
        _mapperMock = new Mock<IMapper>();
        _configurationMock = new Mock<IConfiguration>();
        _productRESMock = new Mock<IProductRES>();
        _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();

        _configurationMock.Setup(c => c["FolderPath:Product"]).Returns(_productImgPath);
        _mockWebHostEnvironment.Setup(wb => wb.WebRootPath).Returns("wwwroot");

        _imgHelperMock = new Mock<ImgHelper>(Mock.Of<IWebHostEnvironment>());

        _productSVC = new ProductSVC(
            _mapperMock.Object,
            _configurationMock.Object,
            _imgHelperMock.Object,
            _productRESMock.Object);
    }

    [Fact]
    public void Add_ValidProduct_ReturnsProductDTO()
    {
        // Arrange
        var productDTO = new ProductDTO
        {
            Id = new Guid(),
            Name = "Test Product",
            UnitPrice = 5,
            HardDiscount = 1,
            PercentDiscount = 10,
            CategoryId = new Guid("303CF809-C86B-41A5-9D2E-03C93B19F0D2"),
        };
        var product = new Product { Id = productDTO.Id, Name = productDTO.Name };
        _mapperMock.Setup(m => m.Map<Product>(productDTO)).Returns(product);
        _productRESMock.Setup(r => r.Add(product)).Returns(product);
        _mapperMock.Setup(m => m.Map<ProductDTO>(product)).Returns(productDTO);

        // Act
        var result = _productSVC.Add(productDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productDTO.Id, result?.Id);
        Assert.Equal(productDTO.Name, result?.Name);
        Assert.Equal(productDTO.UnitPrice, result?.UnitPrice);
        Assert.Equal(productDTO.CategoryId, result?.CategoryId);
        Assert.Equal(productDTO.HardDiscount, result?.HardDiscount);
        Assert.Equal(productDTO.PercentDiscount, result?.PercentDiscount);
    }

    [Fact]
    public void CaculteSellingUnitPrice_ValidProductId_ReturnsCalculatedPrice()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product { Id = productId, UnitPrice = 100m, PercentDiscount = 10, HardDiscount = 5m };
        _productRESMock.Setup(r => r.GetById(productId)).Returns(product);

        // Act
        var result = _productSVC.CaculteSellingUnitPrice(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(85m, result);
    }

    [Fact]
    public void Delete_ExistingProductId_ReturnsTrue()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product { Id = productId };
        _productRESMock.Setup(r => r.GetById(productId)).Returns(product);
        _productRESMock.Setup(r => r.Delete(productId)).Returns(true);

        // Act
        var result = _productSVC.Delete(productId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetAll_ReturnsListOfProductDTOs()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Product 1" },
            new Product { Id = Guid.NewGuid(), Name = "Product 2" }
        };
        var productDTOs = new List<ProductDTO>
        {
            new ProductDTO { Id = products[0].Id, Name = "Product 1" },
            new ProductDTO { Id = products[1].Id, Name = "Product 2" }
        };
        _productRESMock.Setup(r => r.GetAll()).Returns(products);
        _mapperMock.Setup(m => m.Map<IEnumerable<ProductDTO>>(products)).Returns(productDTOs);

        // Act
        var result = _productSVC.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void GetById_ExistingProductId_ReturnsProductDTO()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product { Id = productId, Name = "Test Product" };
        var productDTO = new ProductDTO { Id = productId, Name = "Test Product" };
        _productRESMock.Setup(r => r.GetById(productId)).Returns(product);
        _mapperMock.Setup(m => m.Map<ProductDTO>(product)).Returns(productDTO);

        // Act
        var result = _productSVC.GetById(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productId, result?.Id);
        Assert.Equal("Test Product", result?.Name);
    }

    [Fact]
    public void Update_ValidProduct_ReturnsUpdatedProductDTO()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var productDTO = new ProductDTO { Id = productId, Name = "Updated Product" };
        var product = new Product { Id = productId, Name = "Updated Product" };
        _mapperMock.Setup(m => m.Map<Product>(productDTO)).Returns(product);
        _productRESMock.Setup(r => r.Update(product, productId)).Returns(product);
        _mapperMock.Setup(m => m.Map<ProductDTO>(product)).Returns(productDTO);

        // Act
        var result = _productSVC.Update(productDTO, productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productId, result?.Id);
        Assert.Equal("Updated Product", result?.Name);
    }

    [Fact]
    public void CalculateSellingUnitPrice_ShouldReturnCorrectPrice()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product
        {
            Id = productId,
            UnitPrice = 100m,
            PercentDiscount = 10,
            HardDiscount = 5m
        };

        _productRESMock.Setup(repo => repo.GetById(productId)).Returns(product);

        // Act
        var result = _productSVC.CaculteSellingUnitPrice(productId);

        // Assert
        decimal expectedPrice = ((100 - product.PercentDiscount) / 100m * product.UnitPrice) - product.HardDiscount;
        if (expectedPrice < 0)
            expectedPrice = 0;

        Assert.Equal(expectedPrice, result);
    }
}