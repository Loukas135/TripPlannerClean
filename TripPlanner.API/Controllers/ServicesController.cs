using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Services.Commands.CreateService;
using TripPlanner.Application.Services.Commands.DeleteService;
using TripPlanner.Application.Services.Dtos;
using TripPlanner.Application.Services.Queries.GetAllServices;
using TripPlanner.Application.Services.Queries.GetServiceById;

namespace TripPlanner.API.Controllers
{
<<<<<<< Updated upstream
	[ApiController]
=======
    [ApiController]
>>>>>>> Stashed changes
	[Route("api/governorates/{govId}/services")]
	public class ServicesController(IMediator mediator) : ControllerBase
	{
		//private static List<string> AllowedRoles = ["User","HotelOwner", "CarRental", "TourismOffice", "Restaurant"];
		[HttpPost]
		[Route("servicetype/{stId}")]
<<<<<<< Updated upstream
        [Authorize(Roles ="HotelOwner")]
		[Authorize(Roles="CarRental")]
		
=======
		[Authorize(Roles = "Administrator")]
>>>>>>> Stashed changes
		public async Task<IActionResult> AddService(int govId, int stId, [FromBody] CreateServiceCommand command)
		{
			command.GovernorateId = govId;
			command.ServiceTypeId = stId;
			int serId = await mediator.Send(command);
			return CreatedAtAction(nameof(GetServiceById), new { govId, serId }, null);
			//I shouldn't forget to add rate to services table
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAllServicesFromGovernorate(int govId)
		{
			var services = await mediator.Send(new GetAllServicesQuery(govId));
			return Ok(services);
		}

		[HttpDelete]
		[Route("{serId}")]
		public async Task<IActionResult> DeleteService(int govId, int serId)
		{
			await mediator.Send(new DeleteServiceCommand(govId, serId));
			return NoContent();
		}

		[HttpGet]
		[Route("{serId}")]
		public async Task<ActionResult<ServiceDto>> GetServiceById(int govId, int serId)
		{
			var service = await mediator.Send(new GetServiceByIdQuery(govId, serId));
			return Ok(service);
		}
	}
}
