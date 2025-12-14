using FluentAssertions;
using GoodProductsApi.BusinessLogic.Mappers;
using GoodProductsApi.BusinessLogic.Mappers.Interfaces;
using GoodProductsApi.BusinessLogic.Services;
using GoodProductsApi.DataAccess.Entities;
using GoodProductsApi.DataAccess.Persistence.Interfaces;
using GoodProductsApi.DataAccess.Persistence.Repositories.Interfaces;
using Moq;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;

namespace GoodProductsApi.BusinessLogic.UnitTests.Services;

[ExcludeFromCodeCoverage]
internal sealed class CompaniesServiceTests
{
    [Test]
    public async Task ReadAll()
    {
        // ARRANGE
        var mapper = new CompanyMapper(new ProductMapper());
        var expectedEntities = new List<Company>()
        {
            new () { Id = 1, Name = "c1" },
            new () { Id = 2, Name = "c2" },
            new () { Id = 3, Name = "c3" },
        };
        var expectedDtos = mapper.FromEntities(expectedEntities);

        var mockCompanyRepo = new Mock<ICompanyRepository>();
        mockCompanyRepo
            .Setup(m => m.ReadAll(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(expectedEntities));

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork
            .SetupGet(m => m.Companies)
            .Returns(mockCompanyRepo.Object);

        var mocker = new AutoMocker();
        mocker.Use<IUnitOfWork>(mockUnitOfWork.Object);
        mocker.Use<ICompanyMapper>(mapper);

        var service = mocker.CreateInstance<CompaniesService>();

        // ACT
        var result = await service.ReadAll(new CancellationToken());

        // ASSERT
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expectedDtos);
    }

    [Test]
    public async Task ReadAll_HandlesRepoException()
    {
        // ARRANGE
        var mockCompanyRepo = new Mock<ICompanyRepository>();
        mockCompanyRepo
            .Setup(m => m.ReadAll(It.IsAny<CancellationToken>()))
            .Throws<Exception>();

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork
            .SetupGet(m => m.Companies)
            .Returns(mockCompanyRepo.Object);

        var mocker = new AutoMocker();
        mocker.Use<IUnitOfWork>(mockUnitOfWork.Object);

        var service = mocker.CreateInstance<CompaniesService>();

        // ACT
        var result = await service.ReadAll(new CancellationToken());

        // ASSERT
        result.IsFailed.Should().BeTrue();
    }
}
