using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Persistence.Contexts;
using UlukunShopAPI.Persistence.Repositories;

namespace UlukunShopAPI.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<UlukunAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString),
            ServiceLifetime.Singleton);
        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRespository>();
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        services.AddScoped<IProductReadRespository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
    }
}