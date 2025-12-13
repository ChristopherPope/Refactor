using FluentResults;
using GoodProductsApi.BusinessLogic.DTOs;
using GoodProductsApi.BusinessLogic.Mappers.Interfaces;
using GoodProductsApi.BusinessLogic.Services.Interfaces;
using GoodProductsApi.DataAccess.Persistence.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoodProductsApi.BusinessLogic.Services;

internal sealed class ProductsService : IProductsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductsService> _logger;
    private readonly IProductMapper _productMapper;

    public ProductsService(IUnitOfWork unitOfWork,
        IProductMapper productMapper,
        ILogger<ProductsService> logger)
    {
        _unitOfWork = unitOfWork;
        _productMapper = productMapper;
        _logger = logger;
    }

    public async Task<Result<List<ProductDto>>> ReadAll(CancellationToken cancellationToken)
    {
        try
        {
            var productEntities = await _unitOfWork.Products.ReadAll(cancellationToken);

            return _productMapper.FromEntities(productEntities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to read Products");

            return Result.Fail("Unable to read Products");
        }
    }
}
