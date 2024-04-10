using MediatR;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.DTOs.Order;

namespace UlukunShopAPI.Application.Features.Commands.Order.CompleteOrder;

public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
{
    readonly IOrderService _orderService;
    readonly IMailService _mailService;

    public CompleteOrderCommandHandler(IOrderService orderService, IMailService mailService)
    {
        _orderService = orderService;
        _mailService = mailService;
    }

    public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
    {
        (bool succeeded, CompletedOrder_DTO dto) = await _orderService.CompleteOrderAsync(request.Id);
        if (succeeded)
            await _mailService.SendCompletedOrderMailAsync(dto.EMail, dto.OrderCode, dto.OrderDate, dto.Username);
        return new();
    }
}