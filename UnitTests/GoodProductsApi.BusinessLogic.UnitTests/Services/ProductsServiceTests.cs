using FluentAssertions;
using GoodProductsApi.BusinessLogic.DTOs;
using GoodProductsApi.BusinessLogic.Mappers;
using GoodProductsApi.BusinessLogic.Mappers.Interfaces;
using GoodProductsApi.BusinessLogic.Results;
using GoodProductsApi.BusinessLogic.Services;
using GoodProductsApi.DataAccess.Entities;
using GoodProductsApi.DataAccess.Persistence.Interfaces;
using GoodProductsApi.DataAccess.Persistence.Repositories.Interfaces;
using Moq;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;

namespace GoodProductsApi.BusinessLogic.UnitTests.Services;

[ExcludeFromCodeCoverage]
internal sealed class ProductsServiceTests
{
    [Test]
    public async Task Update()
    {
        // ARRANGE
        var autoMock = new AutoMocker();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockProductRepo = new Mock<IProductRepository>();
        mockProductRepo
            .Setup(x => x.ReadById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Product());
        mockUnitOfWork
            .SetupGet(x => x.Products)
            .Returns(mockProductRepo.Object);

        autoMock.Use<IProductMapper>(new ProductMapper());
        autoMock.Use<IUnitOfWork>(mockUnitOfWork.Object);
        var expectedProductDto = new ProductDto
        {
            Id = 1,
            Name = "Updated Product",
            Price = 100.09m,
        };

        var service = autoMock.CreateInstance<ProductsService>();

        // ACT
        var result = await service.Update(expectedProductDto, CancellationToken.None);

        // ASSERT
        result.IsSuccess.Should().BeTrue();
    }

    [Test]
    public async Task Update_ProductNotFound()
    {
        // ARRANGE
        var autoMock = new AutoMocker();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockProductRepo = new Mock<IProductRepository>();
        mockProductRepo
            .Setup(x => x.ReadById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);
        mockUnitOfWork
            .SetupGet(x => x.Products)
            .Returns(mockProductRepo.Object);

        autoMock.Use<IProductMapper>(new ProductMapper());
        autoMock.Use<IUnitOfWork>(mockUnitOfWork.Object);

        var service = autoMock.CreateInstance<ProductsService>();

        // ACT
        var result = await service.Update(new ProductDto(), CancellationToken.None);

        // ASSERT
        result.IsFailed.Should().BeTrue();
        result.HasError<EntityNotFoundError>().Should().BeTrue();
    }

    [Test]
    public async Task Update_HandlesProductRepoException()
    {
        // ARRANGE
        var autoMock = new AutoMocker();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockProductRepo = new Mock<IProductRepository>();
        mockProductRepo
            .Setup(x => x.ReadById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .Throws<Exception>();
        mockUnitOfWork
            .SetupGet(x => x.Products)
            .Returns(mockProductRepo.Object);

        autoMock.Use<IProductMapper>(new ProductMapper());
        autoMock.Use<IUnitOfWork>(mockUnitOfWork.Object);

        var service = autoMock.CreateInstance<ProductsService>();

        // ACT
        var result = await service.Update(new ProductDto(), CancellationToken.None);

        // ASSERT
        result.IsFailed.Should().BeTrue();
    }
}