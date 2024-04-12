using Microsoft.Extensions.DependencyInjection;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.Abstractions.Services.Configurations;
using UlukunShopAPI.Application.Abstractions.Storage;
using UlukunShopAPI.Application.Abstractions.Token;
using UlukunShopAPI.Infrastructure.Enums;
using UlukunShopAPI.Infrastructure.Services;
using UlukunShopAPI.Infrastructure.Services.Configurations;
using UlukunShopAPI.Infrastructure.Services.Storage;
using UlukunShopAPI.Infrastructure.Services.Storage.Azure;
using UlukunShopAPI.Infrastructure.Services.Storage.Local;
using UlukunShopAPI.Infrastructure.Services.Token;

namespace UlukunShopAPI.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IStorageService, StorageService>();
        serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
        serviceCollection.AddScoped<IMailService, MailService>();
        serviceCollection.AddScoped<IApplicationService, ApplicationService>();
    }

    public static void AddStorage<T>(this IServiceCollection serviceCollection)where T:Storage,IStorage
    {
        serviceCollection.AddScoped<IStorage, T>();
    }
    public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
    {
        switch (storageType)
        {
            case StorageType.Local :
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
            
            case StorageType.Azure :
                serviceCollection.AddScoped<IStorage, AzureStorage>();
                break;
            
            case StorageType.AWS :
                
                break;
            
            default:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
        }
    }
}