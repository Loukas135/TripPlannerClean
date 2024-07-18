using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Services.Queries.GetServiceFromUserReservation;
using TripPlanner.Application.Users.Commands.FillWallet;
using TripPlanner.Application.Users.Commands.LoginUser;
using TripPlanner.Application.Users.Commands.Register;
using TripPlanner.Application.Users.Commands.RegisterAdmin;
using TripPlanner.Application.Users.Commands.RegisterUser;
using TripPlanner.Application.Users.Commands.TokenCheck;
using TripPlanner.Application.Users.Commands.Verify;
using TripPlanner.Application.Users.Queries;
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
                return NotFound("User not found");
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
        public async Task<ActionResult<IEnumerable<IdentityError>>> RegisterUser([FromForm] RegisterUserCommand command)
        {
            var result = await mediator.Send(command);
            if (result.Any())
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<ActionResult<IEnumerable<IdentityError>>> RegisterAdmin([FromBody]RegisterAdminCommand command)
        {
            var result = await mediator.Send(command);
            if (result.Any())
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var query = new GetCurrentUserQuery();
            var user = await mediator.Send(query);
            return Ok(user);
        }

        [HttpPost]
        [Route("Verify")]
        public async Task<ActionResult<IEnumerable<IdentityError>>> Verify([FromBody]VerifyCommand command)
        {
			var result = await mediator.Send(command);
			if (result)
			{
				return Ok("Verified");
			}
            return BadRequest("Invalid Token");
		}
        [HttpPost]
        [Route("FillWallet")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> FillWallet([FromBody] FillWalletCommand request)
        {
            var result = await mediator.Send(request);
            if (result)
            {
                return Ok();
            }
            return BadRequest("something went wrong");
        }

    }
}

//now to push