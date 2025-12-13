using GoodProductsApi.DataAccess.Entities;
using GoodProductsApi.DataAccess.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoodProductsApi.DataAccess.Persistence.Repositories;

internal sealed class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ProductsDbContext context)
        : base(context)
    {
    }

    public async Task<List<Product>> ReadAll(CancellationToken cancellationToken)
    {
        return await Entities
            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> ReadById(int productId, CancellationToken cancellationToken)
    {
        return await Entities
            .Where(p => p.Id == productId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
