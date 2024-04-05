using MediatR;
using Microsoft.EntityFrameworkCore;
using UlukunShopAPI.Application.Repositories.ProductImageFile;

namespace UlukunShopAPI.Application.Features.Commands.ProductImageFile.MakeProductImageThumbnail;

public class MakeProductImageThumbnailCommandHandler : IRequestHandler<MakeProductImageThumbnailCommandRequest,
    MakeProductImageThumbnailCommandResponse>
{
    private readonly IProductImageFileWriteRepository _imageFileWrite;

    public MakeProductImageThumbnailCommandHandler(IProductImageFileWriteRepository imageFileWrite)
    {
        _imageFileWrite = imageFileWrite;
    }

    public async Task<MakeProductImageThumbnailCommandResponse> Handle(MakeProductImageThumbnailCommandRequest request,
        CancellationToken cancellationToken)
    {
        var query = _imageFileWrite.Table
            .Include(p => p.Products)
            .SelectMany(p => p.Products, (file, product) => new
            {
                file,
                product
            });
        
        var data =await query.FirstOrDefaultAsync(p => p.product.Id == Guid.Parse(request.ProductId) && p.file.isThumbnail);

        if (data != null) data.file.isThumbnail = false;

        var image = await query.FirstOrDefaultAsync(p => p.file.Id == Guid.Parse(request.ImageId));
        if (image != null) image.file.isThumbnail = true;

        await _imageFileWrite.SaveAsync();
        return new();
    }
}