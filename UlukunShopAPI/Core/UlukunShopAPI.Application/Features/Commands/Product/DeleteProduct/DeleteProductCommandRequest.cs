using MediatR;

namespace UlukunShopAPI.Application.Features.Commands.Product.DeleteProduct;

public class DeleteProductCommandRequest:IRequest<DeleteProductCommandResponse>
{
    public string Id { get; set; }
}