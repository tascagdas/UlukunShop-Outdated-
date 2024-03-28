using Microsoft.Extensions.DependencyInjection;
using UlukunShopAPI.Application.Abstractions.Storage;
using UlukunShopAPI.Infrastructure.Enums;
using UlukunShopAPI.Infrastructure.Services.Storage;
using UlukunShopAPI.Infrastructure.Services.Storage.Local;

namespace UlukunShopAPI.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IStorageService, StorageService>();
    }

    public static void AddStorage<T>(this IServiceCollection serviceCollection)where T:class,IStorage
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
                
                break;
            case StorageType.AWS :
                
                break;
            default:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
        }
    }
}