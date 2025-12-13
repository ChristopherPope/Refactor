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

    [HttpPut]
    public async Task<IActionResult> Update(ProductDto bobo, CancellationToken cancellationToken)
    {
        var result = await _productsService.Update(bobo, cancellationToken);

        return MakeResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDto product, CancellationToken cancellationToken)
    {
        var result = await _productsService.Create(product, cancellationToken);

        return MakeCreateResult(result, "api/product");
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> Get(CancellationToken cancellationToken = default)
    {
        var result = await _productsService.ReadAll(cancellationToken);

        return MakeResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<ProductDto>>> Get(int id, CancellationToken cancellationToken = default)
    {
        var result = await _productsService.ReadById(id, cancellationToken);

        return MakeResult(result);
    }
}
