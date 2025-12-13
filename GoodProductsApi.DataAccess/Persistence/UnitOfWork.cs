using GoodProductsApi.DataAccess.Entities;
using GoodProductsApi.DataAccess.Persistence.Interfaces;

namespace GoodProductsApi.DataAccess.Persistence;

internal class UnitOfWork : IDisposable, IUnitOfWork
{
    private readonly ProductsDbContext _dbContext;

    public UnitOfWork(ProductsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChanges(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
