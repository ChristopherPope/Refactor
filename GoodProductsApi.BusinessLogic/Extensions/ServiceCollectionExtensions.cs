using GoodProductsApi.BusinessLogic.Mappers;
using GoodProductsApi.BusinessLogic.Mappers.Interfaces;
using GoodProductsApi.BusinessLogic.Services;
using GoodProductsApi.BusinessLogic.Services.Interfaces;
using GoodProductsApi.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GoodProductsApi.BusinessLogic.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGoodProductsApiBusinessLogic(this IServiceCollection services, string dbConnectionString)
    {
        return services
            .AddMappers()
            .AddServices()
            .AddGoodProductsApiDataAccess(dbConnectionString);
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        return services
            .AddScoped<IProductMapper, ProductMapper>()
            .AddScoped<ICompanyMapper, CompanyMapper>();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IProductsService, ProductsService>()
            .AddScoped<ICompaniesService, CompaniesService>();
    }
}
