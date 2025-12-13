using GoodProductsApi.BusinessLogic.DTOs;
using GoodProductsApi.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoodProductsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ProductsApiController
{
    private readonly ICompaniesService _companiesService;

    public CompanyController(ICompaniesService companiesService)
    {
        _companiesService = companiesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CompanyDto>>> Get(CancellationToken cancellationToken = default)
    {
        var result = await _companiesService.ReadAll(cancellationToken);

        return MakeResult(result);
    }
}
