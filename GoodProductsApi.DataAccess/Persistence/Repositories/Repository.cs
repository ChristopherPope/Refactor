using GoodProductsApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodProductsApi.DataAccess.Persistence.Repositories;

internal abstract class Repository<T> where T : class
{
    protected ProductsDbContext DbContext { get; init; }
    protected DbSet<T> Entities { get; init; }

    public Repository(ProductsDbContext context)
    {
        DbContext = context;
        Entities = DbContext.Set<T>();
    }
}
