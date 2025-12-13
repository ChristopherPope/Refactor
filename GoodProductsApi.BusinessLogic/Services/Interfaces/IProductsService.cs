using FluentResults;
using GoodProductsApi.BusinessLogic.DTOs;

namespace GoodProductsApi.BusinessLogic.Services.Interfaces;

public interface IProductsService
{
    Task<Result<List<ProductDto>>> ReadAll(CancellationToken cancellationToken);
    Task<Result<ProductDto?>> ReadById(int id, CancellationToken cancellationToken);
    Task<Result<ProductDto>> Create(ProductDto productDto, CancellationToken cancellationToken);
}