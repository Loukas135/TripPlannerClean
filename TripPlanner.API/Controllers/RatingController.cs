using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Rating.Commands.AddRating;
using TripPlanner.Application.Rating.Commands.DeleteRating;
using TripPlanner.Application.Services.Queries.GetServiceById;
using TripPlanner.Application.Users;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.API.Controllers
{
    [ApiController]
    [Route("/api/{serviceId}/[controller]")]
    [Authorize]
    public class RatingController(IMediator mediator, IUserContext userContext) :ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult>AddRating([FromRoute]int serviceId,[FromBody]AddRatingCommand request)
        {
            var userId = userContext.GetCurrentUser().Id;
            request.UserId = userId; //you forgot it
            request.ServiceId = serviceId;
           var rate_id= await mediator.Send(request);
            return Created();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRating([FromRoute]int serviceId)
        {
            var deleteCommand = new DeleteRatingCommand(serviceId);
            await mediator.Send(deleteCommand);
            return NoContent();
        }
    }
}
