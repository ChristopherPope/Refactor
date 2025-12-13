using GoodProductsApi.DataAccess.Entities;
using GoodProductsApi.DataAccess.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoodProductsApi.DataAccess.Persistence.Repositories;

internal sealed class CompanyRepository : Repository<Company>, ICompanyRepository
{
    public CompanyRepository(ProductsDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<Company>> ReadAll(CancellationToken cancellationToken)
    {
        return await Entities
            .ToListAsync(cancellationToken);
    }
}
