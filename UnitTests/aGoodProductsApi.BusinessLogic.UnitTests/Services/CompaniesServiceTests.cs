using System.Diagnostics.CodeAnalysis;

namespace GoodProductsApi.BusinessLogic.UnitTests.Services;

[ExcludeFromCodeCoverage]
internal sealed class CompaniesServiceTests
{
    [Test]
    public async Task ReadAll()
    {
        // ARRANGE
        //var mapper = new CompanyMapper(new ProductMapper());
        //var expectedEntities = new List<Company>()
        //{
        //    new () { Id = 1, Name = "c1" },
        //    new () { Id = 2, Name = "c2" },
        //    new () { Id = 3, Name = "c3" },
        //};
        //var expectedDtos = mapper.FromEntities(expectedEntities);

        //var mockCompanyRepo = new Mock<ICompanyRepository>();
        //mockCompanyRepo
        //    .Setup(m => m.ReadAll(It.IsAny<CancellationToken>()))
        //    .Returns(Task.FromResult(expectedEntities));

        //var mockUnitOfWork = new Mock<IUnitOfWork>();
        //mockUnitOfWork
        //    .SetupGet(m => m.Companies)
        //    .Returns(mockCompanyRepo.Object);

        //var mocker = new AutoMocker();
        //mocker.Use<IUnitOfWork>(mockUnitOfWork.Object);
        //mocker.Use<ICompanyMapper>(mapper);

        //var service = mocker.CreateInstance<CompaniesService>();

        //// ACT
        //var result = await service.ReadAll(new CancellationToken());

        //// ASSERT
        //result.IsSuccess.Should().BeTrue();
        //result.Value.Should().BeEquivalentTo(expectedDtos);
    }
}
