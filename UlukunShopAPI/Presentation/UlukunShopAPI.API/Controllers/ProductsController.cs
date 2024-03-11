using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Domain.Entities;


namespace UlukunShopAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductReadRespository _productReadRespository;
        readonly private IProductWriteRepository _productWriteRepository;

        public ProductsController(IProductReadRespository productReadRespository,
            IProductWriteRepository productWriteRepository)
        {
            _productReadRespository = productReadRespository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            // await _productWriteRepository.AddRangeAsync(new()
            // {
            //     new() { Id = Guid.NewGuid(), Name = "Ürün-1", Price = 110, CreatedDate = DateTime.UtcNow, Stock = 10 },
            //     new() { Id = Guid.NewGuid(), Name = "Ürün-2", Price = 201, CreatedDate = DateTime.UtcNow, Stock = 42 },
            //     new() { Id = Guid.NewGuid(), Name = "Ürün-3", Price = 338, CreatedDate = DateTime.UtcNow, Stock = 64 },
            //     new() { Id = Guid.NewGuid(), Name = "Ürün-4", Price = 499, CreatedDate = DateTime.UtcNow, Stock = 14 }
            // });
            // await _productWriteRepository.SaveAsync();
            Product p = await _productReadRespository.GetByIdAsync("0b5f9f01-c099-4b6d-bee5-78f2bdb71757",false);
            p.Name = "selen";
            await _productWriteRepository.SaveAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product=await _productReadRespository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}