using GoodProductsApi.BusinessLogic.DTOs;
using GoodProductsApi.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoodProductsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ProductsApiController
{
    private readonly IProductsService _productsService;

    public ProductController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> Get(CancellationToken cancellationToken = default)
    {
        var r = await _productsService.ReadAll(cancellationToken);

        return ReturnResult(r);
    }
}
