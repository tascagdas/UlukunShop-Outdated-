using MediatR;
using UlukunShopAPI.Application.Abstractions.Hubs;
using UlukunShopAPI.Application.Abstractions.Services;

namespace UlukunShopAPI.Application.Features.Commands.Order.CreateOrder;

public class CreateOrderCommandHandler:IRequestHandler<CreateOrderCommandRequest,CreateOrderCommandResponse>
{
    private readonly IOrderService _orderService;
    private readonly IOrderHubService _orderHubService;
    private readonly IShoppingCartService _shoppingCartService;

    public CreateOrderCommandHandler(IOrderService orderService, IOrderHubService orderHubService, IShoppingCartService shoppingCartService)
    {
        _orderService = orderService;
        _orderHubService = orderHubService;
        _shoppingCartService = shoppingCartService;
    }

    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
    {
        await _orderService.CreateOrderAsync(new()
        {
            Address = request.Address,
            Description = request.Description,
            BasketId = _shoppingCartService.GetUserActiveBasket?.Id.ToString()
        });

        await _orderHubService.OrderAddedMessageAsync("Oleey, yeni bir sipariş geldi! :)  (gelsin paracıklar) ");

        return new();
    }
}