using FluentResults;
using GoodProductsApi.BusinessLogic.DTOs;
using GoodProductsApi.BusinessLogic.Mappers.Interfaces;
using GoodProductsApi.BusinessLogic.Services.Interfaces;
using GoodProductsApi.DataAccess.Persistence.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoodProductsApi.BusinessLogic.Services;

internal sealed class CompaniesService : ICompaniesService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CompaniesService> _logger;
    private readonly ICompanyMapper _companyMapper;

    public CompaniesService(IUnitOfWork unitOfWork,
        ILogger<CompaniesService> logger,
        ICompanyMapper companyMapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _companyMapper = companyMapper;
    }

    public async Task<Result<List<CompanyDto>>> ReadAll(CancellationToken cancellationToken)
    {
        try
        {
            var companyEntities = await _unitOfWork.Companies.ReadAll(cancellationToken);

            return _companyMapper.FromEntities(companyEntities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to read Companies");

            return Result.Fail("Unable to read Companies");
        }
    }
}
