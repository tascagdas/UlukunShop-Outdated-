using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UlukunShopAPI.Application.Consts;
using UlukunShopAPI.Application.CustomAttributes;
using UlukunShopAPI.Application.Enums;
using UlukunShopAPI.Application.Features.Commands.ShoppingCart.AddItemToCart;
using UlukunShopAPI.Application.Features.Commands.ShoppingCart.RemoveItemFromCart;
using UlukunShopAPI.Application.Features.Commands.ShoppingCart.UpdateQuantityOfCartItem;
using UlukunShopAPI.Application.Features.Queries.ShoppingCart.GetCartItems;

namespace UlukunShopAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.ShoppingCarts, ActionType = ActionType.Reading, Definition = "Get ShoppingCart Items")]
        public async Task<IActionResult> GetShoppingCartItems([FromQuery]GetCartItemsQueryRequest getCartItemsQueryRequest)
        {
            List<GetCartItemsQueryResponse> response= await _mediator.Send(getCartItemsQueryRequest);
            return Ok(response);
        }
        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.ShoppingCarts, ActionType = ActionType.Writing, Definition = "Add Item To ShoppingCart")]
        public async Task<IActionResult> AddItemToCart(AddItemToCartCommandRequest addItemToCartCommandRequest)
        {
            AddItemToCartCommandResponse response= await _mediator.Send(addItemToCartCommandRequest);
            return Ok(response);
        }
        [HttpPut]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.ShoppingCarts, ActionType = ActionType.Updating, Definition = "Update Quantity")]
        public async Task<IActionResult> ChangeCartItemQuantity(UpdateQuantityOfCartItemCommandRequest updateQuantityOfCartItemCommandRequest)
        {
            UpdateQuantityOfCartItemCommandResponse  response= await _mediator.Send(updateQuantityOfCartItemCommandRequest);
            return Ok(response);
        }
        [HttpDelete("{ShoppingCartItemId}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.ShoppingCarts, ActionType = ActionType.Deleting, Definition = "Remove ShoppingCart Item")]
        public async Task<IActionResult> RemoveCartItem([FromRoute]RemoveItemFromCartCommandRequest removeItemFromCartCommandRequest)
        {
            RemoveItemFromCartCommandResponse response= await _mediator.Send(removeItemFromCartCommandRequest);
            return Ok(response);
        }
        
    }
}
