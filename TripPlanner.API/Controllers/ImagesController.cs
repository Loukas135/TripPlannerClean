using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Services.Commands.Images;

namespace TripPlanner.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ImagesController(IMediator mediator):ControllerBase
    {
        [HttpPost]
        [Route("{serId}/Image/Add")]
        public async Task<IActionResult> AddServiceImage([FromRoute]int serId,AddImageCommand request)
        {
            request.ServiceId = serId;
            var result =await mediator.Send(request);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
