using AutoMapper;
using Moq;
using Restaurant.DTOs;
using Restaurant.Models.Db;
using Restaurant.Repositories.Interfaces;
using Restaurant.Services.Implements;

public class CategorySVCTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ICategoryRES> _categoryRESMock;
    private readonly CategorySVC _categorySVC;

    public CategorySVCTests()
    {
        _mapperMock = new Mock<IMapper>();
        _categoryRESMock = new Mock<ICategoryRES>();
        _categorySVC = new CategorySVC(_mapperMock.Object, _categoryRESMock.Object);
    }

    [Fact]
    public void Add_ValidCategory_ReturnsCategoryDTO()
    {
        // Arrange
        var categoryDTO = new CategoryDTO { Id = Guid.NewGuid(), Name = "Test Category" };
        var category = new Category { Id = categoryDTO.Id, Name = categoryDTO.Name };
        _mapperMock.Setup(m => m.Map<Category>(categoryDTO)).Returns(category);
        _categoryRESMock.Setup(r => r.Add(category)).Returns(category);
        _mapperMock.Setup(m => m.Map<CategoryDTO>(category)).Returns(categoryDTO);

        // Act
        var result = _categorySVC.Add(categoryDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryDTO.Id, result?.Id);
        Assert.Equal(categoryDTO.Name, result?.Name);
    }

    [Fact]
    public void Delete_ExistingCategoryId_ReturnsTrue()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        _categoryRESMock.Setup(r => r.Delete(categoryId)).Returns(true);

        // Act
        var result = _categorySVC.Delete(categoryId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetAll_ReturnsListOfCategoryDTOs()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category { Id = Guid.NewGuid(), Name = "Category 1" },
            new Category { Id = Guid.NewGuid(), Name = "Category 2" }
        };
        var categoryDTOs = new List<CategoryDTO>
        {
            new CategoryDTO { Id = categories[0].Id, Name = "Category 1" },
            new CategoryDTO { Id = categories[1].Id, Name = "Category 2" }
        };
        _categoryRESMock.Setup(r => r.GetAll()).Returns(categories);
        _mapperMock.Setup(m => m.Map<IEnumerable<CategoryDTO>>(categories)).Returns(categoryDTOs);

        // Act
        var result = _categorySVC.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result?.Count());
    }

    [Fact]
    public void GetAllAvailable_ReturnsListOfCategoryDTOs()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category { Id = Guid.NewGuid(), Name = "Category 1" },
            new Category { Id = Guid.NewGuid(), Name = "Category 2" }
        };
        var categoryDTOs = new List<CategoryDTO>
        {
            new CategoryDTO { Id = categories[0].Id, Name = "Category 1" },
            new CategoryDTO { Id = categories[1].Id, Name = "Category 2" }
        };
        _categoryRESMock.Setup(r => r.GetAllAvailable()).Returns(categories);
        _mapperMock.Setup(m => m.Map<IEnumerable<CategoryDTO>>(categories)).Returns(categoryDTOs);

        // Act
        var result = _categorySVC.GetAllAvailable();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result?.Count());
    }

    [Fact]
    public void GetById_ExistingCategoryId_ReturnsCategoryDTO()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var category = new Category { Id = categoryId, Name = "Test Category" };
        var categoryDTO = new CategoryDTO { Id = categoryId, Name = "Test Category" };
        _categoryRESMock.Setup(r => r.GetById(categoryId)).Returns(category);
        _mapperMock.Setup(m => m.Map<CategoryDTO>(category)).Returns(categoryDTO);

        // Act
        var result = _categorySVC.GetById(categoryId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryId, result?.Id);
        Assert.Equal("Test Category", result?.Name);
    }

    [Fact]
    public void Update_ValidCategory_ReturnsUpdatedCategoryDTO()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var categoryDTO = new CategoryDTO { Id = categoryId, Name = "Updated Category" };
        var category = new Category { Id = categoryId, Name = "Updated Category" };
        _mapperMock.Setup(m => m.Map<Category>(categoryDTO)).Returns(category);
        _categoryRESMock.Setup(r => r.Update(category, categoryId)).Returns(category);
        _mapperMock.Setup(m => m.Map<CategoryDTO>(category)).Returns(categoryDTO);

        // Act
        var result = _categorySVC.Update(categoryDTO, categoryId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryId, result?.Id);
        Assert.Equal("Updated Category", result?.Name);
    }
}