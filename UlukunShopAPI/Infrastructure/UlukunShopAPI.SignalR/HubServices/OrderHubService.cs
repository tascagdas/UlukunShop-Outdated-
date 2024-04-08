using Microsoft.AspNetCore.SignalR;
using UlukunShopAPI.Application.Abstractions.Hubs;
using UlukunShopAPI.SignalR.Hubs;

namespace UlukunShopAPI.SignalR.HubServices;

public class OrderHubService:IOrderHubService
{
    private readonly IHubContext<OrderHub> _hubContext;

    public OrderHubService(IHubContext<OrderHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task OrderAddedMessageAsync(string message)
    => await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.OrderAddedMessage,message);
    
}