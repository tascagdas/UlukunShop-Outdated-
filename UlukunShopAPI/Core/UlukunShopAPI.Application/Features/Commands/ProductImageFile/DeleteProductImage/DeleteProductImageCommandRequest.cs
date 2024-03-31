using MediatR;

namespace UlukunShopAPI.Application.Features.Commands.ProductImageFile.DeleteProductImage;

public class DeleteProductImageCommandRequest:IRequest<DeleteProductImageCommandResponse>
{
    public string Id { get; set; }
    public string? ImageId { get; set; }
}