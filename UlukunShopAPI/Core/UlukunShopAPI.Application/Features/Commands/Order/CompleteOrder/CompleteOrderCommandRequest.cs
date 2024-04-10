using MediatR;

namespace UlukunShopAPI.Application.Features.Commands.Order.CompleteOrder;

public class CompleteOrderCommandRequest : IRequest<CompleteOrderCommandResponse>
{
    public string Id { get; set; }
}