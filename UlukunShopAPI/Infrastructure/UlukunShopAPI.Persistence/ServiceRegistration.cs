using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Application.Repositories.InvoiceFile;
using UlukunShopAPI.Application.Repositories.ProductImageFile;
using UlukunShopAPI.Domain.Entities.Identity;
using UlukunShopAPI.Persistence.Contexts;
using UlukunShopAPI.Persistence.Repositories;
using UlukunShopAPI.Persistence.Repositories.File;
using UlukunShopAPI.Persistence.Repositories.InvoiceFile;
using UlukunShopAPI.Persistence.Repositories.ProductImageFile;

namespace UlukunShopAPI.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<UlukunAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString),
            ServiceLifetime.Singleton);
        services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<UlukunAPIDbContext>();
        
        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRespository>();
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        services.AddScoped<IProductReadRespository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        services.AddScoped<IFileReadRepository, FileReadRepository>();
        services.AddScoped<IFileWriteRepository, FileWriteRepository>();
        services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
        services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
        services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
        services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();

    }
}