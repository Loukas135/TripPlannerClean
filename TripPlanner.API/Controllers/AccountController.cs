using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Users.Commands.LoginUser;
using TripPlanner.Application.Users.Commands.Register;
using TripPlanner.Application.Users.Commands.TokenCheck;
using TripPlanner.Domain.Entities.AuthEntity;

namespace TripPlanner.API.Controllers
{
    [ApiController]
    [Route ("/api/[controller]")]
    public class AccountController(IMediator mediator) :ControllerBase
    {
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterCommand request)
        {
            var record = await mediator.Send(request);
            return Ok(record);

        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult>Login(LoginUserCommand request)
        {
            var result= await mediator.Send(request);
            return Ok(result);
        }
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequestCommand request)
        {
            var response = await mediator.Send(request);
            if (response == null)
            {
                return Unauthorized();
            }
            return Ok(response);
        }
    }
}
