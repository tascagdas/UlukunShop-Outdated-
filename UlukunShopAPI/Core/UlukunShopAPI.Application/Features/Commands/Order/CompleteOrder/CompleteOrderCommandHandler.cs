using MediatR;
using UlukunShopAPI.Application.Abstractions.Services;

namespace UlukunShopAPI.Application.Features.Commands.Order.CompleteOrder;

public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
{
    readonly IOrderService _orderService;

    public CompleteOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
    {
        await _orderService.CompleteOrderAsync(request.Id);
        return new();
    }
}