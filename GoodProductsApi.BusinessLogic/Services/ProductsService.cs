using GoodProductsApi.DataAccess.Persistence.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoodProductsApi.BusinessLogic.Services;

internal sealed class ProductsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductsService> _logger;

    public ProductsService(IUnitOfWork unitOfWork, ILogger<ProductsService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
}
