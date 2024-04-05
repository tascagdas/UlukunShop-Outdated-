using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UlukunShopAPI.Application.Features.Commands.Product.CreateProduct;
using UlukunShopAPI.Application.Features.Commands.Product.DeleteProduct;
using UlukunShopAPI.Application.Features.Commands.Product.UpdateProduct;
using UlukunShopAPI.Application.Features.Commands.ProductImageFile.DeleteProductImage;
using UlukunShopAPI.Application.Features.Commands.ProductImageFile.MakeProductImageThumbnail;
using UlukunShopAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using UlukunShopAPI.Application.Features.Queries.Product.GetAllProducts;
using UlukunShopAPI.Application.Features.Queries.Product.GetByIdProduct;
using UlukunShopAPI.Application.Features.Queries.ProductImageFile.GetProductImages;

namespace UlukunShopAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductsQueryRequest getAllProductsQueryRequest)
        {
            GetAllProductsQueryResponse response = await _mediator.Send(getAllProductsQueryRequest);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
        {
            DeleteProductCommandResponse response = await _mediator.Send(deleteProductCommandRequest);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            uploadProductImageCommandRequest.FormFileCollection = Request.Form.Files;
            UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            List<GetProductImagesQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest);
            return Ok(response);
        }

        [HttpDelete("[action]/{Id}")]  
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] DeleteProductImageCommandRequest deleteProductImageCommandRequest, [FromQuery] string imageId)
        {
            deleteProductImageCommandRequest.ImageId = imageId;
            DeleteProductImageCommandResponse response = await _mediator.Send(deleteProductImageCommandRequest);
            return Ok();
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> MakeProductImageThumbnail([FromQuery]MakeProductImageThumbnailCommandRequest makeProductImageThumbnailCommandRequest)
        {
            MakeProductImageThumbnailCommandResponse response =
                await _mediator.Send(makeProductImageThumbnailCommandRequest);
            return Ok(response);
        }
    }
}