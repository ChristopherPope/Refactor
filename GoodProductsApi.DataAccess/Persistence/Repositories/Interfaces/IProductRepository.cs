using GoodProductsApi.DataAccess.Entities;

namespace GoodProductsApi.DataAccess.Persistence.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> ReadAll(CancellationToken cancellationToken);
    Task<Product?> ReadById(int productId, CancellationToken cancellationToken);
}