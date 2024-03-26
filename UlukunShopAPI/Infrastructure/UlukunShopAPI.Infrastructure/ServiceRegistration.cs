using Microsoft.Extensions.DependencyInjection;
using UlukunShopAPI.Application.Services;
using UlukunShopAPI.Infrastructure.Services;

namespace UlukunShopAPI.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IFileService, FileService>();
    }
}