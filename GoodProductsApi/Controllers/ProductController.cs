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
        var result = await _productsService.ReadAll(cancellationToken);

        return MakeActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<ProductDto>>> Get(int id, CancellationToken cancellationToken = default)
    {
        var result = await _productsService.ReadById(id, cancellationToken);

        return MakeActionResult(result);
    }
}
