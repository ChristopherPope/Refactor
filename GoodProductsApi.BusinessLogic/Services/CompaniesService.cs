using GoodProductsApi.DataAccess.Persistence.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoodProductsApi.BusinessLogic.Services;

internal sealed class CompaniesService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CompaniesService> _logger;

    public CompaniesService(IUnitOfWork unitOfWork, ILogger<CompaniesService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
}
