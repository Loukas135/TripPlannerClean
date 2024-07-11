using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Rating.Commands.AddRating;
using TripPlanner.Application.Rating.Commands.DeleteRating;
using TripPlanner.Application.Services.Queries.GetServiceById;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.API.Controllers
{
    [ApiController]
    [Route("/api/{serviceId}/[controller]")]
    [Authorize(Roles = "User")]
    public class RatingController(IMediator mediator):ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult>AddRating([FromRoute]int serviceId,[FromBody]AddRatingCommand request)
        {
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
