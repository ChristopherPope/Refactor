using GoodProductsApi.BusinessLogic.DTOs;
using GoodProductsApi.DataAccess.Entities;

namespace GoodProductsApi.BusinessLogic.Mappers.Interfaces;

internal interface ICompanyMapper
{
    List<CompanyDto> FromEntities(IEnumerable<Company> entities);
    CompanyDto FromEntity(Company entity);
}