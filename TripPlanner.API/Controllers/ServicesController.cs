using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Services.Commands.CreateService;
using TripPlanner.Application.Services.Commands.DeleteService;
using TripPlanner.Application.Services.Queries.GetAllServices;
using TripPlanner.Application.Services.Queries.GetServiceById;

namespace TripPlanner.API.Controllers
{
	[ApiController]
	[Route("api/{controller}")]
	public class ServicesController(IMediator mediator) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> AddService([FromBody] CreateServiceCommand command)
		{
			int id = await mediator.Send(command);
			return Ok(id);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllServices()
		{
			var services = await mediator.Send(new GetAllServicesQuery());
			return Ok(services);
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> DeleteService([FromRoute] int id)
		{
			bool isDeleted = await mediator.Send(new DeleteServiceCommand(id));
			if(isDeleted)
			{
				return NoContent();
			}
			return NotFound();
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetServiceById([FromRoute] int id)
		{
			var service = await mediator.Send(new GetServiceByIdQuery(id));
			if(service == null)
			{
				return NotFound();
			}
			return Ok(service);
		}
	}
}
