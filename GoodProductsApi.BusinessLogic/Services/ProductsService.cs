using FluentResults;
using GoodProductsApi.BusinessLogic.DTOs;
using GoodProductsApi.BusinessLogic.Mappers.Interfaces;
using GoodProductsApi.BusinessLogic.Results;
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

    public async Task<Result> Update(ProductDto productDto, CancellationToken cancellationToken)
    {
        try
        {
            var productEntity = await _unitOfWork.Products.ReadById(productDto.Id, cancellationToken);
            if (productEntity is null)
            {
                return new EntityNotFoundError("Product", productDto.Id);
            }

            _productMapper.CopyToEntity(productDto, productEntity);
            await _unitOfWork.SaveChanges(cancellationToken);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to update the Product (ID: {Id}", productDto.Id);

            return Result.Fail($"Unable to update the Product (ID: {productDto.Id}");
        }
    }

    public async Task<Result<ProductDto>> Create(ProductDto productDto, CancellationToken cancellationToken)
    {
        try
        {
            var company = await _unitOfWork.Companies.ReadById(productDto.CompanyId, cancellationToken);
            if (company is null)
            {
                return new EntityNotFoundError("Company", productDto.CompanyId);
            }

            var productEntity = _productMapper.ToEntity(productDto);
            if (productEntity.Price <= 0)
            {
                productEntity.Price = 123;
            }

            var newProduct = await _unitOfWork.Products.Create(productEntity, cancellationToken);
            await _unitOfWork.SaveChanges(cancellationToken);

            return _productMapper.FromEntity(newProduct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to create the new Product");

            return Result.Fail("Unable to create the new Product");
        }
    }

    public async Task<Result<ProductDto?>> ReadById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _unitOfWork.Products.ReadById(id, cancellationToken);
            if (product is null)
            {
                return new EntityNotFoundError("Product", id);
            }

            return _productMapper.FromEntity(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to read Product ID {Id}", id);

            return Result.Fail($"Unable to read Product ID {id}");
        }
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
