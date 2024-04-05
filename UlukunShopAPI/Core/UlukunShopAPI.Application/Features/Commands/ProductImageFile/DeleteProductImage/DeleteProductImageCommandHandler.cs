using MediatR;
using Microsoft.EntityFrameworkCore;
using UlukunShopAPI.Application.Repositories;

namespace UlukunShopAPI.Application.Features.Commands.ProductImageFile.DeleteProductImage;

public class DeleteProductImageCommandHandler:IRequestHandler<DeleteProductImageCommandRequest,DeleteProductImageCommandResponse>
{
    private readonly IProductReadRespository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public DeleteProductImageCommandHandler(IProductReadRespository productReadRepository, IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    public async Task<DeleteProductImageCommandResponse> Handle(DeleteProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Product? product= await _productReadRepository.Table.Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
        Domain.Entities.ProductImageFile? productImageFile = product?.ProductImages.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));
        if (productImageFile!=null)
        {
            product?.ProductImages.Remove(productImageFile);

        }
        await _productWriteRepository.SaveAsync();
        return new();
    }
}