using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UlukunShopAPI.Application.Abstractions.Storage;
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
        
        
        public ProductsController(
            IProductReadRespository productReadRepository,
            IProductWriteRepository productWriteRepository,
            IFileWriteRepository fileWriteRepository,
            IFileReadRepository fileReadRepository,
            IProductImageFileReadRepository imageRead,
            IProductImageFileWriteRepository imageWrite,
            IInvoiceFileReadRepository invoiceFileRead,
            IInvoiceFileWriteRepository invoiceFileWrite,
            IStorageService storageService)
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
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            //tracking devre disi daha verimli calisma icin.
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).ToList();
            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            //tracking devre disi daha verimli calisma icin.
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }


        [HttpPost]
        public async Task<IActionResult> Post(ProductCreateViewModel model)
        {

            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });
            await _productWriteRepository.SaveAsync();
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
        public async Task<IActionResult> Upload()
        {

            var datas=await _storageService.UploadAsync("files", Request.Form.Files);
             await _imageWrite.AddRangeAsync(datas.Select(d => new ProductImageFile()
             {
                 FileName = d.fileName,
                 Path = d.pathOrContainer,
                 Storage = _storageService.storageName
             }).ToList());
              await _imageWrite.SaveAsync();
            
            
            // var datas= await _fileService.UploadAsync("resource/product-images", Request.Form.Files);
            // await _imageWrite.AddRangeAsync(datas.Select(d => new ProductImageFile()
            // {
            //     FileName = d.fileName,
            //     Path = d.path
            // }).ToList());
            // await _imageWrite.SaveAsync();
            
            // var datas= await _fileService.UploadAsync("resource/invoices", Request.Form.Files);
            // await _invoiceFileWrite.AddRangeAsync(datas.Select(d => new InvoiceFile()
            // {
            //     FileName = d.fileName,
            //     Path = d.path,
            //     Price = new Random().Next()
            // }).ToList());
            // await _invoiceFileWrite.SaveAsync();
            
            // var datas= await _fileService.UploadAsync("resource/files", Request.Form.Files);
            // await _fileWriteRepository.AddRangeAsync(datas.Select(d => new File()
            // {
            //     FileName = d.fileName,
            //     Path = d.path
            // }).ToList());
            // await _invoiceFileWrite.SaveAsync();
            
            
            // var data= _fileReadRepository.GetAll(false);
            // var data2= _invoiceFileRead.GetAll(false);
            // var data3= _imageRead.GetAll(false);
            
            return Ok();
        }
    }
}