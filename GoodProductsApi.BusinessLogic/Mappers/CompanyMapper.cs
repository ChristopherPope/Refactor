using GoodProductsApi.BusinessLogic.DTOs;
using GoodProductsApi.BusinessLogic.Mappers.Interfaces;
using GoodProductsApi.DataAccess.Entities;

namespace GoodProductsApi.BusinessLogic.Mappers;

internal sealed class CompanyMapper : ICompanyMapper
{
    private readonly IProductMapper _productMapper;

    public CompanyMapper(IProductMapper productMapper)
    {
        _productMapper = productMapper;
    }

    public CompanyDto FromEntity(Company entity)
    {
        return new CompanyDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Products = _productMapper.FromEntities(entity.Products)
        };
    }

    public List<CompanyDto> FromEntities(IEnumerable<Company> entities)
    {
        return [.. entities.Select(FromEntity)];
    }
}
