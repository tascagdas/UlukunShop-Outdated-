using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UlukunShopAPI.Application.Features.Commands.AppUser.CreateUser;
using UlukunShopAPI.Application.Features.Commands.AppUser.FacebookLoginUser;
using UlukunShopAPI.Application.Features.Commands.AppUser.GoogleLoginUser;
using UlukunShopAPI.Application.Features.Commands.AppUser.LoginUser;

namespace UlukunShopAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }


    }
}