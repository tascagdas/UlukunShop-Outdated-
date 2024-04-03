using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UlukunShopAPI.Application.Features.Commands.AppUser.FacebookLoginUser;
using UlukunShopAPI.Application.Features.Commands.AppUser.GoogleLoginUser;
using UlukunShopAPI.Application.Features.Commands.AppUser.LoginUser;

namespace UlukunShopAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginUserCommandRequest googleLoginUserCommandRequest)
        {
            GoogleLoginUserCommandResponse response = await _mediator.Send(googleLoginUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginUserCommandRequest facebookLoginUserCommandRequest)
        {
            FacebookLoginUserCommandResponse response = await _mediator.Send(facebookLoginUserCommandRequest);
            return Ok(response);
        }
    }
}
