using GoodProductsApi.DataAccess.Entities;

namespace GoodProductsApi.DataAccess.Persistence.Repositories.Interfaces;

public interface ICompanyRepository : IRepository<Company>
{
    Task<List<Company>> ReadAll(CancellationToken cancellationToken);
}