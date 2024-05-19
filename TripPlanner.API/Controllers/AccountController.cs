using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Users.Commands.LoginUser;
using TripPlanner.Application.Users.Commands.Register;
using TripPlanner.Application.Users.Commands.RegisterUser;
using TripPlanner.Application.Users.Commands.TokenCheck;
using TripPlanner.Domain.Entities.AuthEntity;

namespace TripPlanner.API.Controllers
{
    [ApiController]
    [Route ("/api/[controller]")]
    public class AccountController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("RegisterServiceOwner")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> RegisterServiceOwner(RegisterServiceOwnerCommand request)
        {
            var ownerId = await mediator.Send(request);
            return Ok(ownerId);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserCommand request)
        {
            var result = await mediator.Send(request);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("RefreshToken")]
        [Authorize]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequestCommand request)
        {
            var response = await mediator.Send(request);
            if (response == null)
            {
                return Unauthorized();
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<IEnumerable<IdentityError>>> RegisterUser([FromBody] RegisterUserCommand command)
        {
            var result = await mediator.Send(command);
            if (result.Any())
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}

