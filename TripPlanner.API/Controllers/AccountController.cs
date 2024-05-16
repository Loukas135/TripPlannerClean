using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Users.Commands.LoginUser;
using TripPlanner.Application.Users.Commands.Register;
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
    }
}

//b4723cb3-d420-41f1-b9df-1d9b0e9a5489

/*
 * {
  "name": "sheraton",
  "address": "damascus umawyeen",
  "description": "very nice hotel",
  "contactNumber": "+96395555888",
  "contactEmail": "sheraton@hotmail.com",
  "governorateId": 0,
  "serviceTypeId": 0,
  "ownerId": "b4723cb3-d420-41f1-b9df-1d9b0e9a5489"
}
*/
