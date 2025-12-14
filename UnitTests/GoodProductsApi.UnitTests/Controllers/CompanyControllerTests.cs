using FluentAssertions;
using FluentResults;
using GoodProductsApi.BusinessLogic.DTOs;
using GoodProductsApi.BusinessLogic.Services.Interfaces;
using GoodProductsApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;

namespace GoodProductsApi.UnitTests.Controllers;

[ExcludeFromCodeCoverage]
internal sealed class CompanyControllerTests
{
    [Test]
    public async Task Get()
    {
        // ARRANGE
        var mocker = new AutoMocker();
        var mockCompaniesService = new Mock<ICompaniesService>();
        var expectedCompanies = new List<CompanyDto>()
        {
            new() { Id = 1, Name = "c1" },
            new() { Id = 2, Name = "c2" },
            new() { Id = 3, Name = "c3" },
        };
        mockCompaniesService
            .Setup(m => m.ReadAll(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok(expectedCompanies)));
        mocker.Use<ICompaniesService>(mockCompaniesService.Object);

        var controller = mocker.CreateInstance<CompanyController>();

        // ACT
        var result = await controller.Get();

        // ASSERT
        result.Should().BeOfType<ActionResult<List<CompanyDto>>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        (result.Result as OkObjectResult)!.Value.Should().BeOfType<List<CompanyDto>>();
        var actualCompanies = (result.Result as OkObjectResult)!.Value as List<CompanyDto>;

        actualCompanies.Should().BeEquivalentTo(expectedCompanies);
    }

    [Test]
    public async Task Get_ServiceReturnsFailure()
    {
        // ARRANGE
        var mocker = new AutoMocker();
        var mockCompaniesService = new Mock<ICompaniesService>();
        mockCompaniesService
            .Setup(m => m.ReadAll(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail<List<CompanyDto>>(string.Empty)));

        mocker.Use<ICompaniesService>(mockCompaniesService.Object);

        var controller = mocker.CreateInstance<CompanyController>();

        // ACT
        var result = await controller.Get();

        // ASSERT
        result.Should().BeOfType<ActionResult<List<CompanyDto>>>();
        result.Result.Should().BeOfType<ObjectResult>();
        (result.Result as ObjectResult)!.Value.Should().BeOfType<ProblemDetails>();
    }
}
