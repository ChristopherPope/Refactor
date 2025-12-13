using GoodProductsApi.DataAccess.Entities;
using GoodProductsApi.DataAccess.Persistence.Interfaces;
using GoodProductsApi.DataAccess.Persistence.Repositories;
using GoodProductsApi.DataAccess.Persistence.Repositories.Interfaces;

namespace GoodProductsApi.DataAccess.Persistence;

internal class UnitOfWork : IDisposable, IUnitOfWork
{
    private readonly ProductsDbContext _dbContext;
    private readonly Lazy<ICompanyRepository> _companies;
    private readonly Lazy<IProductRepository> _products;

    public ICompanyRepository Companies => _companies.Value;
    public IProductRepository Products => _products.Value;

    public UnitOfWork(ProductsDbContext dbContext)
    {
        _dbContext = dbContext;

        _companies = new Lazy<ICompanyRepository>(() => new CompanyRepository(dbContext));
        _products = new Lazy<IProductRepository>(() => new ProductRepository(dbContext));
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
