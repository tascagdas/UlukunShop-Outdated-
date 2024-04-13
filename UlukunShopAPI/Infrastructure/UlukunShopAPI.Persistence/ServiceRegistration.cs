using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.Abstractions.Services.Authentications;
using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Application.Repositories.CompletedOrder;
using UlukunShopAPI.Application.Repositories.Endpoint;
using UlukunShopAPI.Application.Repositories.InvoiceFile;
using UlukunShopAPI.Application.Repositories.Menu;
using UlukunShopAPI.Application.Repositories.ProductImageFile;
using UlukunShopAPI.Application.Repositories.ShoppingCart;
using UlukunShopAPI.Application.Repositories.ShoppingCartItem;
using UlukunShopAPI.Domain.Entities.Identity;
using UlukunShopAPI.Persistence.Contexts;
using UlukunShopAPI.Persistence.Repositories;
using UlukunShopAPI.Persistence.Repositories.CompletedOrder;
using UlukunShopAPI.Persistence.Repositories.Endpoint;
using UlukunShopAPI.Persistence.Repositories.File;
using UlukunShopAPI.Persistence.Repositories.InvoiceFile;
using UlukunShopAPI.Persistence.Repositories.Menu;
using UlukunShopAPI.Persistence.Repositories.ProductImageFile;
using UlukunShopAPI.Persistence.Repositories.ShoppingCart;
using UlukunShopAPI.Persistence.Repositories.ShoppingCartItem;
using UlukunShopAPI.Persistence.Services;

namespace UlukunShopAPI.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        
        //Sqlite için alttaki.
        services.AddDbContext<UlukunAPIDbContext>(options =>
            options
                .UseSqlite(services
                    .BuildServiceProvider()
                    .GetRequiredService<IConfiguration>()
                    .GetConnectionString("SqliteConnection")));
        
        
        
        //Postgre için aşağıdaki...
        // services.AddDbContext<UlukunAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString),
        //     ServiceLifetime.Scoped);
        
        
        
        
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
        } ).AddEntityFrameworkStores<UlukunAPIDbContext>().AddDefaultTokenProviders();
        
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
        services.AddScoped<ICompletedOrderReadRepository, CompletedOrderReadRepository>();
        services.AddScoped<ICompletedOrderWriteRepository, CompletedOrderWriteRepository>();
        services.AddScoped<IEndpointReadRepository, EndpointReadRepository>();
        services.AddScoped<IEndpointWriteRepository, EndpointWriteRepository>();
        services.AddScoped<IMenuReadRepository, MenuReadRepository>();
        services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();


        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IExternalAuthentication, AuthService>();
        services.AddScoped<IInternalAuthentication, AuthService>();
        services.AddScoped<IShoppingCartService, ShoppingCartService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAuthorizationEndpointService, AuthorizationEndpointService>();
        

    }
}