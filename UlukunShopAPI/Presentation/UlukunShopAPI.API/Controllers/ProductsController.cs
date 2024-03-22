using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Application.ViewModels.Products;
using UlukunShopAPI.Domain.Entities;


namespace UlukunShopAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductReadRespository _productReadRespository;
        readonly private IProductWriteRepository _productWriteRepository;


        public ProductsController(
            IProductReadRespository productReadRespository,
            IProductWriteRepository productWriteRepository)
        {
            _productReadRespository = productReadRespository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //tracking devre disi daha verimli calisma icin.
            return Ok(_productReadRespository.GetAll(false));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            //tracking devre disi daha verimli calisma icin.
            return Ok(await _productReadRespository.GetByIdAsync(id, false));
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
            Product product = await _productReadRespository.GetByIdAsync(model.Id);
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
            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}