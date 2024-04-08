using Microsoft.Extensions.DependencyInjection;
using UlukunShopAPI.Application.Abstractions.Hubs;
using UlukunShopAPI.SignalR.HubServices;

namespace UlukunShopAPI.SignalR;

public static class ServiceRegistration
{
    public static void AddsignalRServices(this IServiceCollection collection)
    {
        collection.AddTransient<IProductHubService, ProductHubService>();
        collection.AddTransient<IOrderHubService, OrderHubService>();
        collection.AddSignalR();
    }
}