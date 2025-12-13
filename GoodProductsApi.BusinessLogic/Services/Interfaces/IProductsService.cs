using FluentResults;
using GoodProductsApi.BusinessLogic.DTOs;

namespace GoodProductsApi.BusinessLogic.Services.Interfaces;

public interface IProductsService
{
    Task<Result<List<CompanyDto>>> ReadAll(CancellationToken cancellationToken);
}