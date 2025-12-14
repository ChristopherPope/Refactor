using FluentAssertions;
using GoodProductsApi.DataAccess.Entities;
using GoodProductsApi.DataAccess.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;

namespace GoodProductsApi.DataAccess.UnitTests.Persistence.Repositories;

[ExcludeFromCodeCoverage]
internal sealed class CompanyRepositoryTests : BaseRepositoryTests
{
    [Test]
    public async Task ReadById()
    {
        // ARRANGE
        var autoMock = new AutoMocker();
        using var context = new ProductsDbContext(_dbContextOptions);
        autoMock.Use<ProductsDbContext>(context);
        var repo = autoMock.CreateInstance<CompanyRepository>();

        var expectedCompany = _companies.First();

        // ACT
        var actualCompany = await repo.ReadById(expectedCompany.Id, new CancellationToken());

        // ASSERT
        actualCompany.Should().BeEquivalentTo(expectedCompany, options => options
            .Excluding(c => c.Products));
    }


    [Test]
    public async Task ReadAll()
    {
        // ARRANGE
        var autoMock = new AutoMocker();
        using var context = new ProductsDbContext(_dbContextOptions);
        autoMock.Use<ProductsDbContext>(context);
        var repo = autoMock.CreateInstance<CompanyRepository>();


        // ACT
        var actualCompanies = await repo.ReadAll(new CancellationToken());

        // ASSERT
        actualCompanies.Should().BeEquivalentTo(_companies, options => options
            .Excluding(c => c.Products));
    }
}
