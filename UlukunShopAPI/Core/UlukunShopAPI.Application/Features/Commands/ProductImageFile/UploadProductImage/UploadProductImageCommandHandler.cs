using MediatR;
using UlukunShopAPI.Application.Abstractions.Storage;
using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Application.Repositories.ProductImageFile;

namespace UlukunShopAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;

public class UploadProductImageCommandHandler:IRequestHandler<UploadProductImageCommandRequest,UploadProductImageCommandResponse>
{
    private readonly IStorageService _storageService;
    private readonly IProductImageFileWriteRepository _imageFileWrite;
    private readonly IProductReadRespository _productReadRespository;

    public UploadProductImageCommandHandler(IProductImageFileWriteRepository imageFileWrite, IProductReadRespository productReadRespository, IStorageService storageService)
    {
        _imageFileWrite = imageFileWrite;
        _productReadRespository = productReadRespository;
        _storageService = storageService;
    }

    public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        List<(string fileName,string pathOrContainerName)> result = await _storageService.UploadAsync("product-images", request.FormFileCollection);

        Domain.Entities.Product product= await _productReadRespository.GetByIdAsync(request.Id);
        await _imageFileWrite.AddRangeAsync(result.Select(x => new Domain.Entities.ProductImageFile
        {
            FileName = x.fileName,
            Path = x.pathOrContainerName,
            Storage = _storageService.storageName,
            Products = new List<Domain.Entities.Product>(){product}
        }).ToList());

        await _imageFileWrite.SaveAsync();
        return new();
    }
}