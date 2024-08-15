using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Services.Queries.GetServiceFromUserReservation;
using TripPlanner.Application.Users.Commands.DeleteAccount;
using TripPlanner.Application.Users.Commands.FillWallet;
using TripPlanner.Application.Users.Commands.LoginUser;
using TripPlanner.Application.Users.Commands.Register;
using TripPlanner.Application.Users.Commands.RegisterAdmin;
using TripPlanner.Application.Users.Commands.RegisterUser;
using TripPlanner.Application.Users.Commands.TokenCheck;
using TripPlanner.Application.Users.Commands.UpdateImage;
using TripPlanner.Application.Users.Commands.Verify;
using TripPlanner.Application.Users.Queries;
using TripPlanner.Application.Users.Queries.GetAllUsersPerType;
using TripPlanner.Application.Users.Queries.GetNewUsers;
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
            var result = new { newOwnerId = ownerId };
            return this.StatusCode(201,result);
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
            var baseVerifyUrl = Request.Scheme + "://" + Request.Host + "/api" + "/Account" + "/Verify" + "?token=";
            command.BaseUrl = baseVerifyUrl;
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

        [HttpGet]
        [Route("Verify")]
        public async Task<ActionResult<IEnumerable<IdentityError>>> Verify([FromQuery]string token)
        {
			var result = await mediator.Send(new VerifyCommand(token));
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

        [HttpGet]
        [Route("report/NumOfUsers")]
        public async Task<ActionResult> GetNumberOfUsersPerRole()
        {
            var request = new GetAllUsersPerTypeQuery();
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        [Route("report/NumOfUsers/{month}/{year}")]
        public async Task<ActionResult> GetNumberOfUsersPerRole(int month,int year)
        {
            var request = new GetNewUsersQuery(month,year);
            var result = await mediator.Send(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("Images/UpdateUserImage")]
        [Authorize]
        public async Task<ActionResult> UpdateUserImage([FromForm] UpdateUserImageCommand request)
        {
            await mediator.Send(request);
            return NoContent();
        }
        [HttpDelete]
        [Route("deleteAccount")]
        [Authorize]
        public async Task<ActionResult> DeleteUserAccount([FromBody] DeleteAccountCommand request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}