using GoodProductsApi.DataAccess.Entities;
using GoodProductsApi.DataAccess.Persistence;
using GoodProductsApi.DataAccess.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GoodProductsApi.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGoodProductsApiDataAccess(this IServiceCollection services, string dbConnectionString)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<ProductsDbContext>(options =>
        {
            options
                .UseSqlServer(dbConnectionString)
                .UseLazyLoadingProxies();
        });

        return services;
    }
}
