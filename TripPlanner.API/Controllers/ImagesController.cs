using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Images.Query;
using TripPlanner.Application.Services.Commands.Images;

namespace TripPlanner.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ImagesController(IMediator mediator):ControllerBase
    {
        [HttpPost]
        [Route("{serId}/Image/Add")]
        public async Task<IActionResult> AddServiceImage([FromRoute]int serId,IFormFile serImage)
        {
            AddImageCommand request = new AddImageCommand();
            request.ServiceImage = serImage;
            request.ServiceId = serId;
            var result =await mediator.Send(request);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("images/{path}")]
        public async Task<ActionResult>GetImageAsIs([FromRoute]string path)
        {
            var image = System.IO.File.OpenRead(path);
            return File(image, "image/png");
        }
        [HttpGet]
        [Route("images/{path}/asBytes")]
        public async Task<ActionResult>GetImageAsBytes([FromRoute]string path)
        {
            var byteImage = await mediator.Send(new GetImageAsByteArray(path));
            return Ok(byteImage);
        }
    }
}
