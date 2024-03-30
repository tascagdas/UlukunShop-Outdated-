using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UlukunShopAPI.Application.Abstractions.Storage;
using UlukunShopAPI.Application.Features.Commands.CreateProduct;
using UlukunShopAPI.Application.Features.Queries.GetAllProducts;
using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Application.Repositories.InvoiceFile;
using UlukunShopAPI.Application.Repositories.ProductImageFile;
using UlukunShopAPI.Application.RequestParameters;
using UlukunShopAPI.Application.ViewModels.Products;
using UlukunShopAPI.Domain.Entities;
using File = UlukunShopAPI.Domain.Entities.File;


namespace UlukunShopAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRespository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IProductImageFileReadRepository _imageRead;
        private readonly IProductImageFileWriteRepository _imageWrite;
        private readonly IInvoiceFileReadRepository _invoiceFileRead;
        private readonly IInvoiceFileWriteRepository _invoiceFileWrite;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _configuration;

        private readonly IMediator _mediator;
        
        
        
        
        public ProductsController(
            IProductReadRespository productReadRepository,
            IProductWriteRepository productWriteRepository,
            IFileWriteRepository fileWriteRepository,
            IFileReadRepository fileReadRepository,
            IProductImageFileReadRepository imageRead,
            IProductImageFileWriteRepository imageWrite,
            IInvoiceFileReadRepository invoiceFileRead,
            IInvoiceFileWriteRepository invoiceFileWrite,
            IStorageService storageService, IConfiguration configuration, IMediator mediator)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _imageRead = imageRead;
            _imageWrite = imageWrite;
            _invoiceFileRead = invoiceFileRead;
            _invoiceFileWrite = invoiceFileWrite;
            _storageService = storageService;
            this._configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllProductsQueryRequest getAllProductsQueryRequest)
        {
          GetAllProductsQueryResponse response= await _mediator.Send(getAllProductsQueryRequest);
          return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            //tracking devre disi daha verimli calisma icin.
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response= await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ProductUpdateViewModel model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Name = model.Name;
            product.Stock = model.Stock;
            product.Price = model.Price;
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
            List<(string fileName,string pathOrContainerName)> result = await _storageService.UploadAsync("product-images", Request.Form.Files);

            Product product= await _productReadRepository.GetByIdAsync(id);


            // foreach (var r in result)
            // {
            //     product.ProductImageFiles.Add(new()
            //     {
            //         FileName = r.fileName,
            //         Path = r.pathOrContainerName,
            //         Storage = _storageService.storageName,
            //         Products = new List<Product>(){product}
            //     });
            // }
            
            
            
            await _imageWrite.AddRangeAsync(result.Select(x => new ProductImageFile
            {
                FileName = x.fileName,
                Path = x.pathOrContainerName,
                Storage = _storageService.storageName,
                Products = new List<Product>(){product}
            }).ToList());

            await _imageWrite.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages(string id)
        {
           Product? product= await _productReadRepository.Table.Include(p => p.ProductImageFiles)
               .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
           return Ok(product.ProductImageFiles.Select(p => new
           {
                Path=$"{_configuration["BaseStorageUrl"]}/{p.Path}",
                p.FileName,
                p.Id
           }));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id, string imageId)
        {
            Product? product= await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));
            product.ProductImageFiles.Remove(productImageFile);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}