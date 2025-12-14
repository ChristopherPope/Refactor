using GoodProductsApi.BusinessLogic.DTOs;
using GoodProductsApi.DataAccess.Entities;

namespace GoodProductsApi.BusinessLogic.Mappers.Interfaces;

public interface ICompanyMapper
{
    List<CompanyDto> FromEntities(IEnumerable<Company> entities);
    CompanyDto FromEntity(Company entity);
}