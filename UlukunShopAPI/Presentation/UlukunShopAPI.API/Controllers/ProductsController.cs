using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UlukunShopAPI.Application.Abstractions.Storage;
using UlukunShopAPI.Application.Features.Commands.Product.CreateProduct;
using UlukunShopAPI.Application.Features.Commands.Product.DeleteProduct;
using UlukunShopAPI.Application.Features.Commands.Product.UpdateProduct;
using UlukunShopAPI.Application.Features.Commands.ProductImageFile.DeleteProductImage;
using UlukunShopAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using UlukunShopAPI.Application.Features.Queries.Product.GetAllProducts;
using UlukunShopAPI.Application.Features.Queries.Product.GetByIdProduct;
using UlukunShopAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
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
        public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response= await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute]DeleteProductCommandRequest deleteProductCommandRequest)
        {
            DeleteProductCommandResponse response = await _mediator.Send(deleteProductCommandRequest);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery]UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            uploadProductImageCommandRequest.FormFileCollection = Request.Form.Files;
            UploadProductImageCommandResponse response= await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages([FromRoute]GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            List<GetProductImagesQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest);
            return Ok(response);
        }

        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute]DeleteProductImageCommandRequest deleteProductImageCommandRequest,[FromQuery]string imageId)
        {
            deleteProductImageCommandRequest.ImageId = imageId;
            DeleteProductImageCommandResponse response = await _mediator.Send(deleteProductImageCommandRequest);
            return Ok();
        }
    }
}