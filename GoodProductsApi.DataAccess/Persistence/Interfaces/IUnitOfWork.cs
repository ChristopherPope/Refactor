using GoodProductsApi.DataAccess.Persistence.Repositories.Interfaces;

namespace GoodProductsApi.DataAccess.Persistence.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChanges(CancellationToken cancellationToken = default);

    ICompanyRepository Companies { get; }
}