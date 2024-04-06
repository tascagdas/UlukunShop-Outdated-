using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.Abstractions.Services.Authentications;
using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Application.Repositories.InvoiceFile;
using UlukunShopAPI.Application.Repositories.ProductImageFile;
using UlukunShopAPI.Application.Repositories.ShoppingCart;
using UlukunShopAPI.Application.Repositories.ShoppingCartItem;
using UlukunShopAPI.Domain.Entities.Identity;
using UlukunShopAPI.Persistence.Contexts;
using UlukunShopAPI.Persistence.Repositories;
using UlukunShopAPI.Persistence.Repositories.File;
using UlukunShopAPI.Persistence.Repositories.InvoiceFile;
using UlukunShopAPI.Persistence.Repositories.ProductImageFile;
using UlukunShopAPI.Persistence.Repositories.ShoppingCart;
using UlukunShopAPI.Persistence.Repositories.ShoppingCartItem;
using UlukunShopAPI.Persistence.Services;

namespace UlukunShopAPI.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<UlukunAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString),
            ServiceLifetime.Singleton);
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
        } ).AddEntityFrameworkStores<UlukunAPIDbContext>();
        
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
        services.AddScoped<IShoppingCartWriteRepository, ShoppingCartWriteRepository>();
        services.AddScoped<IShoppingCartReadRepository, ShoppingCartReadRepository>();
        services.AddScoped<IShoppingCartItemWriteRepository, ShoppingCartItemWriteRepository>();
        services.AddScoped<IShoppingCartItemReadRepository, ShoppingCartItemReadRepository>();


        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IExternalAuthentication, AuthService>();
        services.AddScoped<IInternalAuthentication, AuthService>();
        services.AddScoped<IShoppingCartService, ShoppingCartService>();
        

    }
}