using MediatR;

namespace UlukunShopAPI.Application.Features.Commands.ProductImageFile.MakeProductImageThumbnail;

public class MakeProductImageThumbnailCommandRequest:IRequest<MakeProductImageThumbnailCommandResponse>
{
    public string ImageId { get; set; }
    public string ProductId { get; set; }
}