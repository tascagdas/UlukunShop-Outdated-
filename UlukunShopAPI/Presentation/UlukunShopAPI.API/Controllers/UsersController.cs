using MediatR;
using Microsoft.AspNetCore.Mvc;
using UlukunShopAPI.Application.Abstractions.Services;
using UlukunShopAPI.Application.Features.Commands.AppUser.CreateUser;

namespace UlukunShopAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMailService _mailService;

        public UsersController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> MailTest()
        {
            await _mailService.SendMessageAsync("tascagdas@gmail.com", "Deneme Mail", "<h1>Deneme Maili.</h1>");
            return Ok();
        }

    }
}
