using GoodProductsApi.BusinessLogic.DTOs;
using GoodProductsApi.BusinessLogic.Mappers.Interfaces;
using GoodProductsApi.DataAccess.Entities;

namespace GoodProductsApi.BusinessLogic.Mappers;

internal sealed class ProductMapper : IProductMapper
{
    public void CopyToEntity(ProductDto dto, Product entity)
    {
        entity.Name = dto.Name;
        entity.Price = dto.Price;
    }

    public Product ToEntity(ProductDto dto)
    {
        return new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            CompanyId = dto.CompanyId
        };
    }

    public ProductDto FromEntity(Product entity)
    {
        return new ProductDto
        {
            Id = entity.Id,
            CompanyId = entity.CompanyId,
            Name = entity.Name,
            Price = entity.Price,
        };
    }

    public List<ProductDto> FromEntities(IEnumerable<Product> entities)
    {
        return [.. entities.Select(FromEntity)];
    }
}
