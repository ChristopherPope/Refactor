using GoodProductsApi.BusinessLogic.DTOs;
using GoodProductsApi.DataAccess.Entities;

namespace GoodProductsApi.BusinessLogic.Mappers.Interfaces;

internal interface IProductMapper
{
    List<ProductDto> FromEntities(IEnumerable<Product> entities);
    ProductDto FromEntity(Product entity);
    Product ToEntity(ProductDto dto);
}